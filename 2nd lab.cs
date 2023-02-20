/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* ООП на C#                       *
* 13.2.2023                       *
**********************************/

using System;

namespace lab {
  public class Document {
    public string Name = "MyGreatDoc";
    public string Author = "Leonardo";
    public string KeyWord = "lol, kek, cheburek";
    public string Topic = "Top";
    public string Storage = "T:/User/direction/file.doc";
    virtual public void Info() {
      Console.WriteLine("Имя файла: " + Name + "\n" + Author + " - " + Topic);
    }
  }

  class Word : Document {
    int PageCount = 17;
    public override void Info() {
      Console.WriteLine("Текстовый Word файл: " + Name + "\nавтор: " + Author + "\nколичество страниц: " + PageCount + "\nКлючевые слова: " + KeyWord);
    }
  }

  class Pdf : Document {
    string Font = "Calibri";
    public override void Info() {
      Console.WriteLine("PDF файл: " + Name + "\nавтор " + Author + "\nшрифт: " + Font);
    }
  }

  class Excel : Document {
    int RowCount = 7;
    int ColumnCount = 9;
    public override void Info() {
      Console.WriteLine("Таблица Excel " + Name + " размером " + RowCount + "x" + ColumnCount);
    }
  }

  class Txt : Document {
    int FontSize = 12;
    public override void Info() {
      Console.WriteLine("TXT файл " + Name + "\nРазмер шриффта: " + FontSize + "\nавтор " + Author);
    }
  }

  class Html : Document {
    int StringCount = 200;
    public override void Info() {
      Console.WriteLine("HTML файл: " + Name + "\nстрок: " + StringCount);
    }
  }

  public class Singleton {
    public static Singleton Instance {
      get {
        if (instance == null) instance = new Singleton();
        return instance;
      }
    }
    public void Method (){
      Console.WriteLine("Выберите, какой файл хотите вывести\n1-Word\n2-PDF\n3-Excel\n4-TXT\n5-HTML");
    }
    private Singleton() { }
    private static Singleton instance;
  }
  internal class Program {
    static void Main(string[] args) {
      Singleton.Instance.Method();
      string Choise = Console.ReadLine();
      Console.Clear();

      Html Html = new Html();
      Word Word = new Word();
      Pdf Pdf = new Pdf();
      Excel Excel = new Excel();
      Txt Txt = new Txt();

      switch (Choise) {
        case "1":
          Word.Info();
          break;
        case "2":
          Pdf.Info();
          break;
        case "3":
          Excel.Info();
          break;
        case "4":
          Txt.Info();
          break;
        case "5":
          Html.Info();
          break;
      }
      Console.ReadKey();
    }
  }
}
