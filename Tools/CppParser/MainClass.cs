using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

static class MainClass
{
    const string PATH = @"d:\MyGames\Urho3D_Fork\Urho3D\Source\Urho3D\";

    static string[] игнорируемыеПапки =
    {
        "AngelScript",
        "LuaScript",
        "DotNet"
    };

    static string[] игнорируемыеФайлы =
    {
        "Main.h",
        "DbConnection.h",
        "DbResult.h",
        "GraphicsImpl.h",
        "ShaderProgram.h",
        "VertexDeclaration.h",
        "MacFileWatcher.h",
        "Str.h",
    };

    // Возвращает список папок из "/Source/Urho3D/", отбрасывая лишние.
    static string[] НужныеПапки(string путь)
    {
        string[] папки = Directory.GetDirectories(путь);
        List<string> результат = new List<string>(папки.Length);
        foreach (string папка in папки)
        {
            if (!игнорируемыеПапки.Contains(Утилиты.ИмяПапки(папка)))
                результат.Add(папка);
        }
        return результат.ToArray();
    }

    // Возвращает список заголовочных файлов из "Source/Urho3D/ДИРЕКТОРИЯ/", отбрасывая лишние.
    static string[] НужныеФайлы(string путь)
    {
        string[] файлы = Directory.GetFiles(путь, "*.h");
        List<string> результат = new List<string>(файлы.Length);
        foreach (string файл in файлы)
        {
            if (!игнорируемыеФайлы.Contains(Path.GetFileName(файл)))
                результат.Add(файл);
        }
        return результат.ToArray();
    }

    static void Main(string[] аргументы)
    {
        
        foreach (string папка in НужныеПапки(PATH))
        {
            // Создаем папку в текущей.
            string имяПапки = Утилиты.ИмяПапки(папка);
            if (!Directory.Exists("DotNet/" + имяПапки))
                Directory.CreateDirectory("DotNet/" + имяПапки);
            foreach (string файл in НужныеФайлы(папка))
            {
                Console.WriteLine(файл);

                string оригинальныйИсходник = File.ReadAllText(файл);
                string преобразованныйИсходник = ЗаголовочныйФайл.Преобразовать(оригинальныйИсходник);

                string имяФайла = Path.GetFileName(файл);
                имяПапки = Утилиты.ИмяПапки(файл);
                File.WriteAllText("DotNet/" + имяПапки + "/" + имяФайла, преобразованныйИсходник);
            }
        }
    }
}
