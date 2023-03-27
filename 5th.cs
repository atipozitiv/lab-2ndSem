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
      //заменить файл новым текстом
    }
    static string ChangeWrongWords(string FileBody) { //исправить слова
      foreach (var Word in WrongWords) {
        if (Word.Key) {                         //(Word.Key == "") {
          
        }
      }
      return FileBody;
    }

    static string ChangePhoneNumbers(string FileBody) { //заменить вид номера используя регулярные выражения
      string Pattern = @"([0-9]{3})[0-9]{3}-[0-9]{2}-[0-9]{2}";
      Regex regex = new Regex(Pattern);
      FileBody = regex.Replace(FileBody, "+38")
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
