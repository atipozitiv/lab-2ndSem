/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* ООП на C#                       *
* 13.2.2023                       *
**********************************/

using lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace lab {
  public class Document {
    public string Name = "MyGreatDoc";    // и так для всех
    public string Author = "Leonardo";
    public string[] KeyWord = {"lol", "kek", "cheburek"};
    public string Topic = "Top";
    public string Storage = "T:/User/direction/file.doc";
    virtual public void Info() {
      Console.WriteLine("Имя файла: " + Name + "\n" + Author + " - " + Topic);
    }
  }

  class Excel: Document {
    int RowCount = 7;
    int ColumnCount = 9;
    public override void Info() {
      Console.WriteLine("Таблица Excel " + Name + " размером " + RowCount + "x" + ColumnCount);
    }
  }

  class Word: Document {
    int PageCount = 17;
    public override void Info() {
      Console.WriteLine("Текстовый Word файл: " + Name + "\nавтора - " + Author + "\nколичество страниц: " + PageCount + "Ключевые слова: " + KeyWord);
    }
  }
  
  
  internal class Program {
    static void Main(string[] args) {
      //синглтон и свитч кейс где пользователь выбирает цифру и выводит одно из (excel, word...)
    }
  }
}
