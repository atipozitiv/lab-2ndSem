/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* Стандартный ввод вывод          *
* 03.03.2023                      *
**********************************/

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace lab {

  [Serializable]
  class TextFile: IOriginator {
    string Path { get; set; }
    public string Content { get; set; }
    public static void Redactor(string Path) {
      bool Working = true;
      while (Working) {
        Console.WriteLine("выберите действие:\n1 - добавить символы\n2 - стереть символы\n3 - откатиться назад\n4 - показать текст\n5 - выход");
        string Choice = Console.ReadLine();
        switch (Choice) {
          case "1":
            Console.WriteLine("какую строчку добавить?");
            string Appending = Console.ReadLine();
            File.AppendAllText(Path, Appending);
            break;
          case "2":
            string ReservePath = "";
            string DeathPath = "";
            for (int Position = 0; Position < Path.Length - 4; ++Position) {
              ReservePath += Path[Position];
              DeathPath += Path[Position];
            }
            ReservePath += "reserve.txt";
            DeathPath += "deth.txt";
            Console.WriteLine("сколько символов стереть?");
            int CountOfDeleting = Convert.ToInt32(Console.ReadLine());
            FileStream MomFile = new FileStream(Path, FileMode.OpenOrCreate);
            FileStream SonFile = new FileStream(ReservePath, FileMode.OpenOrCreate);
            MomFile.CopyTo(SonFile,(int)MomFile.Length - CountOfDeleting);
            MomFile.Flush();
            MomFile.Close();
            SonFile.Flush();
            SonFile.Close();
            File.Replace(Path, ReservePath, DeathPath);
            File.Delete(DeathPath);
            File.Move(ReservePath, Path);
            break;
          case "3":
            MyFile.RestoreState(fnc);
            break;
          case "4":
            Console.Clear();
            Console.WriteLine(File.ReadAllText(Path));
            Console.WriteLine("\n\n\nнажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
            break;
          case "5":
            Working = false;
            break;
          default:
            Console.WriteLine("неверный выбор");
            break;
        }
      }
    }
    public void Serialize(FileStream fs) {
      BinaryFormatter bf = new BinaryFormatter();
      bf.Serialize(fs, this);
      fs.Flush();
      fs.Close();
    }
    public void Deserialize(FileStream fs) {
      BinaryFormatter bf = new BinaryFormatter();
      TextFile deserialized = (TextFile)bf.Deserialize(fs);
      Path = deserialized.Path;
      Content = deserialized.Content;
      //можно дописать еще свойства
      fs.Close();
    }
    public void XmlSerializer(XmlSerializer serializer) {
      XmlSerializer bf = new XmlSerializer(this.GetType());
    }
    object IOriginator.GetMemento() {
      return new Memento {Content = this.Content};
    }
    void IOriginator.SetMemento(object memento) {
      if (memento is Memento) {
        var mem = memento as Memento;
        Content = mem.Content;
      }
    }
  }
  class Memento {
    public string Content { get; set; }
  }
  public interface IOriginator {
    object GetMemento();
    void SetMemento(object memento);
  }

  class Search {
    public static void Searching(string Path, string[] KeyWords) {
      string[] Files = Directory.GetFiles(Path, "*.txt");
      foreach (string file in Files) {
        bool Flag = true;
        foreach (string KeyWord in KeyWords) {
          if (KeyWord != null) { 
            if (!File.ReadAllText(file).Contains(KeyWord)) {
              Flag = false;
            }
          }
        }
        if (Flag) {
          Console.WriteLine(file);
        }
      }
    }
  }

  internal class Program {
    static void Main(string[] args) {
      Console.WriteLine("что вы хотите сделать:\n1 - редактировать файл\n2 - индексация файлов в определенной директории");
      int Choice = Convert.ToInt32(Console.ReadLine());
      if (Choice == 1) {
        Console.WriteLine("Введите путь к файлу");
        string MyPath = Console.ReadLine();
        TextFile MyFile = new TextFile();
        MyFile.Content = File.ReadAllText(MyPath);
        TextFile.Redactor(MyPath);

      } else if (Choice == 2) {
        bool OneMoreWord = true;
        Console.WriteLine("Введите путь к каталогу с файлами");
        string MyPath = Console.ReadLine();
        Console.WriteLine("Начните вводить ключевые слова. Для остановки введите 666");
        string[] MyKeyWords = new string[10];
        string NextWord;
        int Turn = 0;
        while (OneMoreWord) {
          NextWord = Console.ReadLine();
          if (NextWord == "666") {
            OneMoreWord = false;
          } else {
            MyKeyWords[Turn] = NextWord;
          }
          Turn += 1;
        }
        Search.Searching(MyPath, MyKeyWords);
      } else {
        Console.WriteLine("неверный выбор");
      }
      Console.ReadKey();
    }
  }
}