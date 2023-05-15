/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* Строки и коллекции              *
* 20.03.2023                      *
**********************************/

using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace lab {
  public class OhWords {
    static Dictionary<string, string> WrongWords = new Dictionary<string, string>() {
      ["пирвет-првиет-приевт"] = "привет",
      ["кратошка-катрошка-каротшка"] = "картошка"
    };
    public static void FixFile(string Path) {
      string FileBody = File.ReadAllText(Path);
      FileBody = ChangeWrongWords(FileBody);
      FileBody = ChangePhoneNumbers(FileBody);
      File.WriteAllText(Path, FileBody);
    }
    static string ChangeWrongWords(string FileBody) { 
      foreach (var Word in WrongWords) {
        string[] EveryKey = Word.Key.Split('-');
        foreach (var Key in EveryKey) {
          FileBody = FileBody.Replace(Key, Word.Value);
        }
      }
      return FileBody;
    }

    static string ChangePhoneNumbers(string FileBody) { //заменить вид номера используя регулярные выражения
      string Pattern = @"[(]\d{3}[)]\s\d{3}[-]\d{2}[-]\d{2}";
      Regex regex = new Regex(Pattern);
      while (regex.IsMatch(FileBody)) {
        string Number = regex.Match(FileBody).ToString();
        Number = "+38" + Number[1].ToString() + " " + Number[2].ToString() + Number[3].ToString() + " " + Number[6].ToString() + Number[7].ToString() + Number[8].ToString() + " " + Number[10].ToString() + Number[11].ToString() + " " + Number[13].ToString() + Number[14].ToString();
        FileBody = regex.Replace(FileBody, Number, 1);
      }
      return FileBody;
    }
  }

  internal class Program {
    static void Main(string[] args) {     
      Console.WriteLine("введите путь к текстовому файлу: ");
      string Path = Console.ReadLine();
      OhWords.FixFile(Path);
      Console.WriteLine("файл исправлен\nня пока");
      Console.ReadKey();
    }
  }
}
