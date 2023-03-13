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
  class TextFile : IOriginator {
    public string Path { get; set; }
    public string Content { get; set; }

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
      return new Memento { Content = this.Content };
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
  public class Caretaker {
    private object memento;
    public void SaveState(IOriginator originator) {
      memento = originator.GetMemento();
    }

    public void RestoreState(IOriginator originator) {
      originator.SetMemento(memento);
    }
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
        TextFile MyFile = new TextFile();
        MyFile.Path = Console.ReadLine();
        MyFile.Content = File.ReadAllText(MyFile.Path);
        bool Working = true;
        while (Working) {
          Console.WriteLine("выберите действие:\n1 - добавить символы\n2 - стереть символы\n3 - откатиться назад\n4 - показать текст\n5 - выход");
          string ThisChoice = Console.ReadLine();
          Caretaker ct = new Caretaker();
          switch (ThisChoice) {
            case "1":
              ct.SaveState(MyFile);
              Console.WriteLine("какую строчку добавить?");
              string Appending = Console.ReadLine();
              File.AppendAllText(MyFile.Path, Appending);
              MyFile.Content = File.ReadAllText(MyFile.Path);
              
              break;
            case "2":
              ct.SaveState(MyFile);
              string ReservePath = "";
              string DeathPath = "";
              for (int Position = 0; Position < MyFile.Path.Length - 4; ++Position) {
                ReservePath += MyFile.Path[Position];
                DeathPath += MyFile.Path[Position];
              }
              ReservePath += "reserve.txt";
              DeathPath += "deth.txt";
              Console.WriteLine("сколько символов стереть?");
              int CountOfDeleting = Convert.ToInt32(Console.ReadLine());
              FileStream MomFile = new FileStream(MyFile.Path, FileMode.OpenOrCreate);
              FileStream SonFile = new FileStream(ReservePath, FileMode.OpenOrCreate);
              MomFile.CopyTo(SonFile, (int)MomFile.Length - CountOfDeleting);
              MomFile.Flush();
              MomFile.Close();
              SonFile.Flush();
              SonFile.Close();
              File.Replace(MyFile.Path, ReservePath, DeathPath);
              File.Delete(DeathPath);
              File.Move(ReservePath, MyFile.Path);
              MyFile.Content = File.ReadAllText(MyFile.Path);
              
              break;
            case "3":
              
              ct.RestoreState(MyFile);
              //File.Create(MyFile.Path + ".txt");
              FileStream SecondFile = new FileStream(MyFile.Path + ".txt", FileMode.OpenOrCreate);
              SecondFile.Flush();
              SecondFile.Close();
              File.AppendAllText(MyFile.Path + ".txt", MyFile.Content);
              File.Replace(MyFile.Path, MyFile.Path + ".txt", "1");
              File.Replace(MyFile.Path + ".txt", MyFile.Path, "1");
              break;
            case "4":
              Console.Clear();
              Console.WriteLine(File.ReadAllText(MyFile.Path));
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
