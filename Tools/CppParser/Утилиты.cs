using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

static class Утилиты
{
    /*
    public static void Напечатать(IList<string> строки, string разделитель = null)
    {
        for (int i = 0; i < строки.Count; i++)
        {
            Console.WriteLine(строки[i]);
            if (разделитель != null && i != строки.Count - 1)
                Console.WriteLine(разделитель);
        }
    }

    public static string[] ВМассивСтрок(MatchCollection соответствия)
    {
        string[] результат = new string[соответствия.Count];
        for (int i = 0; i < результат.Length; i++)
            результат[i] = соответствия[i].Value;
        return результат;
    }

    public static string[] Исключить(IList<string> список, IList<string> исключения)
    {
        List<string> результат = new List<string>(список.Count);
        foreach (string строка in список)
        {
            if (!исключения.Contains(строка))
                результат.Add(строка);
        }
        return результат.ToArray();
    }
    */

    // Если подряд идут несколько пробелов, они заменяются на одинарный пробел.
    public static string УдалитьПовтороныеПробелы(string строка)
    {
        while (строка.IndexOf("  ") != -1)
            строка = строка.Replace("  ", " ");

        return строка;
    }

    public static string ИмяПапки(string путь)
    {
        if (File.Exists(путь))
        {
            // Убираем название файла из пути.
            путь = Path.GetDirectoryName(путь);
        }

        return new DirectoryInfo(путь).Name;
    }
}
