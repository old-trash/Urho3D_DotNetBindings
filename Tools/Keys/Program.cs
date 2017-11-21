// Generate Keys enum.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

static class MainClass
{
    const string PATH = @"d:\MyGames\Urho3D_Fork\Urho3D\Source\";

    const string UrhoFilePath = PATH + @"Urho3D\Input\InputEvents.h";
    const string SdlScancodeFilePath = PATH + @"ThirdParty\SDL\include\SDL_scancode.h";
    const string SdlKeycodeFilePath = PATH + @"ThirdParty\SDL\include\SDL_keycode.h";

    static Dictionary<string, string> SdlScancodes = new Dictionary<string, string>();
    static Dictionary<string, string> SdlKeycodes = new Dictionary<string, string>();

    static string Clear(string source)
    {
        source = source.Replace("\r\n", "\n");

        // Remove comments.
        string lineComment = @"//.*?\n";
        string blockComment = @"/\*.*?\*/";
        string pattern = lineComment + "|" + blockComment;
        source = Regex.Replace(source, pattern, "\n", RegexOptions.Singleline);

        return source;
    }

    // SDL_SCANCODE_UNKNOWN = 0,
    // SDL_SCANCODE_A = 4,
    // ...
    static void LoadSdlScancodes()
    {
        string source = File.ReadAllText(SdlScancodeFilePath);
        source = Clear(source);

        string pattern = @"(SDL_SCANCODE_\w+)\s*=\s*(\d+)";
        MatchCollection matches = Regex.Matches(source, pattern, RegexOptions.Singleline);

        for (int i = 0; i < matches.Count; i++)
            SdlScancodes.Add(matches[i].Result("$1"), matches[i].Result("$2"));
    }

    const int SDLK_SCANCODE_MASK = (1 << 30);

    static int SDL_SCANCODE_TO_KEYCODE(int X)
    {
        return (X | SDLK_SCANCODE_MASK);
    }

    // Should be call after LoadSdlScancodes();
    static void LoadSdlKeycodes()
    {
        string source = File.ReadAllText(SdlKeycodeFilePath);
        source = Clear(source);

        // Add ',' to last enum member to simplify regexp.
        source = source.Replace("\n};", ",\n};");

        // Remove \n after = to make every enum member singleline.
        string pattern = @"([^\n]*)=\s*\n([^\n]*),";
        source = Regex.Replace(source, pattern, "$1=$2,", RegexOptions.Singleline);

        pattern = @"(SDLK_\w+)\s*=\s*(\S+|' ')\s*,\n";
        MatchCollection matches = Regex.Matches(source, pattern, RegexOptions.Singleline);

        for (int i = 0; i < matches.Count; i++)
        {
            string name = matches[i].Result("$1");
            string value = matches[i].Result("$2");

            // If value is define.
            pattern = @"SDL_SCANCODE_TO_KEYCODE\((\w+)\)";
            Match match = Regex.Match(value, pattern);
            if (match.Success)
            {
                value = match.Result("$1");
                int number = int.Parse(SdlScancodes[value]);
                number = SDL_SCANCODE_TO_KEYCODE(number);
                value = number.ToString();
            }

            SdlKeycodes.Add(name, value);
        }
    }

    // Make lowercase all letters except first.
    static string WithCapital(string str)
    {
        return str.First().ToString().ToUpper() + str.Substring(1).ToLower();
    }

    static void Main()
    {
        LoadSdlScancodes();
        LoadSdlKeycodes();

        // Parsing Urho3D header file.
        string source = File.ReadAllText(UrhoFilePath);
        source = Clear(source);

        string result = "";

        // Keys.
        // static const int KEY_BACKQUOTE = SDLK_BACKQUOTE;
        result += "public enum Keys\r\n{\r\n";
        string pattern = @"static const int KEY_(\w+)\s*=\s*(SDLK_\w+);";
        MatchCollection matches = Regex.Matches(source, pattern, RegexOptions.Singleline);
        for (int i = 0; i < matches.Count; i++)
        {
            string name = matches[i].Result("$1");
            result += "    ";
            if (char.IsDigit(name[0]))
                result += "D";
            //result += WithCapital(name);
            result += name;

            result += " = ";

            string sdlKey = matches[i].Result("$2");
            result += SdlKeycodes[sdlKey];
            result += ",\r\n";
        }
        result += "}\r\n\r\n";

        // Scancodes.
        // static const int SCANCODE_Z = SDL_SCANCODE_Z;
        result += "public enum Scancodes\r\n{\r\n";
        pattern = @"static const int SCANCODE_(\w+)\s*=\s*(SDL_SCANCODE_\w+);";
        matches = Regex.Matches(source, pattern, RegexOptions.Singleline);
        for (int i = 0; i < matches.Count; i++)
        {
            string name = matches[i].Result("$1");
            result += "    ";
            if (char.IsDigit(name[0]))
                result += "D";
            //result += WithCapital(name);
            result += name;

            result += " = ";

            string sdlScancode = matches[i].Result("$2");
            result += SdlScancodes[sdlScancode];
            result += ",\r\n";
        }
        result += "}\r\n\r\n";
        
        // Hacks.
        result = result.Replace(@"'\033'", "27");
        result = result.Replace(@"'\177'", "127");

        File.WriteAllText("InputEvents.cs_result", result);
    }
}
