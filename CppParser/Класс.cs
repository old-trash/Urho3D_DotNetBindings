using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

class Класс
{
    public string Имя { get; private set; }
    public string Тело { get; private set; }

    public Класс(string исходник)
    {
        // Разделяем заголовок и тело класса.
        string шаблон = @"(class\s+URHO3D_API\s+[^{}]*)"
                      + @"{(.+)};";
        Match соответствие = Regex.Match(исходник, шаблон, RegexOptions.Singleline);
        if (!соответствие.Success)
            throw new Exception();
        string заголовок = соответствие.Result("$1");
        Тело = соответствие.Result("$2");

        // Парсим заголовок класса.
        шаблон = @"class\s+URHO3D_API\s+(\w+)\s*(?::|$)";
        соответствие = Regex.Match(заголовок, шаблон, RegexOptions.Singleline);
        if (!соответствие.Success)
            throw new Exception();
        Имя = соответствие.Result("$1").Trim();
    }

    public string[] НайтиФункции()
    {
        string шаблон_ФункцияСТелом = @"[^:;(){}]+\([^;(){}]*\)\s*(?:const)?\s*(?:override)?\s*{.*?}";
        string шаблон_ФункцияБезТела = @"[^:;(){}]+\([^;(){}]*\)\s*(?:const)?\s*(?:override)?\s*;";
        string шаблон = шаблон_ФункцияСТелом + "|" + шаблон_ФункцияБезТела;
        MatchCollection соответствия = Regex.Matches(Тело, шаблон, RegexOptions.Singleline);
        string[] результат = new string[соответствия.Count];
        for (int i = 0; i < результат.Length; i++)
            результат[i] = соответствия[i].Value.Trim();
        return результат;
    }

    public string ПреобразоватьФункцию(string исходник)
    {
        исходник = исходник.Replace("\n", " ");
        if (исходник.StartsWith("URHO3D_OBJECT"))
            return исходник;
        if (исходник.Contains("operator"))
            return исходник;
        if (исходник.Contains("~"))
            return исходник;

        string результат = "// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n";
        результат += "// " + исходник + "\n";

        // Возможно это функция с телом.
        string шаблон = @"([^:;(){}]+)\(([^;(){}]*)\)\s*(?:const)?\s*(?:override)?\s*{.*?}";
        Match соответствие = Regex.Match(исходник, шаблон, RegexOptions.Singleline);
        if (!соответствие.Success)
        {
            // Значит функция без тела.
            шаблон = @"([^:;(){}]+)\(([^;(){}]*)\)\s*(?:const)?\s*(?:override)?\s*;";
            соответствие = Regex.Match(исходник, шаблон, RegexOptions.Singleline);
        }
        string передСкобками = соответствие.Result("$1"); // Возвращаемый тип + имя функции.
        string параметры = соответствие.Result("$2"); // Параметры в скобках.

        шаблон = @"(.*?)\s*(~?\w+)\s*$";
        соответствие = Regex.Match(передСкобками, шаблон, RegexOptions.Singleline);
        string возвращаемыйТип = соответствие.Result("$1");
        string имяФункции = соответствие.Result("$2");

        string имяКласса = Имя;

        // Для отладки.
        результат += "// КЛАСС = " + имяКласса + ", ВОЗВРАЩАЕМЫЙ ТИП = " + возвращаемыйТип + ", ИМЯ = " + имяФункции + ", ПАРАМЕТРЫ = " + параметры + "\n";

        результат += "// C++\n";
        результат += "URHO3D_API ";

        // Это вставим в результат позже, но формируем сразу.
        string заголовок_CSharp = "private static extern ";

        // Возвращаемый тип.
        bool конструктор = false; // Функция является конструктором.
        if (имяКласса == имяФункции)
            конструктор = true;
        if (конструктор)
        {
            результат += имяКласса + "* ";
            заголовок_CSharp += "IntPtr ";
        }
        else
        {
            результат += возвращаемыйТип + " ";
            заголовок_CSharp += возвращаемыйТип + " ";
        }

        // Имя обертки.
        результат += Имя + "_" + имяФункции;
        результат += "(";
        заголовок_CSharp += Имя + "_" + имяФункции;
        заголовок_CSharp += "(";

        // Параметры обертки.
        параметры = Утилиты.УдалитьПовтороныеПробелы(параметры).Trim();
        параметры = параметры.Replace("Context* context", "Context* nativeContext");
        параметры = параметры.Replace("String::EMPTY", "\"\"");
        параметры = параметры.Replace("const String&", "const char*");
        if (конструктор)
        {
            результат += параметры + ")\n";
            заголовок_CSharp += параметры + ");\n";
        }
        else
        {
            if (параметры == "")
            {
                результат += Имя + "* nativeInstance);\n";
                заголовок_CSharp += "IntPtr nativeInstance);\n";
            }
            else
            {
                результат += Имя + "* nativeInstance, " + параметры + ")\n";
                заголовок_CSharp += "IntPtr nativeInstance, " + параметры + ");\n";
            }
        }

        // Тело обертки.
        результат += "{\n";
        if (конструктор)
        {
            результат += "    return new " + имяКласса + "(" + параметры + ");\n";
        }
        else
        {
            if (возвращаемыйТип.Contains("void"))
                результат += "    ";
            else
                результат += "    return ";
            результат += "nativeInstance->" + имяФункции + "(" + УбратьТипыПараметров(параметры) + ");\n";
        }
        результат += "}\n";

        // C# код.
        результат += "// C#\n";
        результат += "[DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]\n";
        заголовок_CSharp = заголовок_CSharp.Replace("const char*", "string");
        результат += УказателиВIntPtr(заголовок_CSharp);

        return результат + "\n\n";
    }

    // На вход передаются параметры функции (содержимое скобок), разделенные запятой.
    // От них оставляются только названия.
    private string УбратьТипыПараметров(string исходник)
    {
        if (исходник.Trim() == "")
            return "";
        string[] параметры = исходник.Split(',');
        string результат = "";
        for (int i = 0; i < параметры.Length; i++)
        {
            string параметр = параметры[i];
            // Убираем значение по умолчанию, если есть.
            string шаблон = @"(.*)=.*";
            Match соответствие = Regex.Match(параметр, шаблон, RegexOptions.Singleline);
            if (соответствие.Success)
                параметр = соответствие.Result("$1");
            // Оставляем только имя.
            шаблон = @".*?\s*(\w+)\s*$";
            соответствие = Regex.Match(параметр, шаблон, RegexOptions.Singleline);
            if (!соответствие.Success)
            {
                результат += "!ОШИБКА!"; // Некоторое недоработки править вручную.
                continue;
            }
            параметр = соответствие.Result("$1");

            результат += параметр;
            if (i != параметры.Length - 1) // Если не последний, добавляем запятую.
                результат += ", ";
        }
        return результат;
    }

    // В C# вместо указателей должны быть IntPtr.
    private string УказателиВIntPtr(string исходник)
    {
        string шаблон = @"\b\w+?\s*?\*"; 
        return Regex.Replace(исходник, шаблон, "IntPtr", RegexOptions.Singleline);
    }
}
