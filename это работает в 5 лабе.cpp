using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Schema;

namespace lab_1 {
  internal class Program {
    static Dictionary<string, string> WrongWords = new Dictionary<string, string>() {
      ["пирвет-првиет-приевт"] = "привет",
      ["кратошка-катрошка-каротшка"] = "картошка"
    };
    static void Main(string[] args) {
      string Input = "пирвет как у тебя дела катрошка крошка";
      foreach(var Word in WrongWords) {
        string[] EveryKey = Word.Key.Split('-');
        foreach(var Key in EveryKey) {
          Input = Input.Replace(Key, Word.Value);
        }
      }
      Console.WriteLine(Input);
      Console.ReadKey();
    }
  }
}
