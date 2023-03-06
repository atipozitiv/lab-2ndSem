/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* Стандартный ввод вывод          *
* 03.03.2023                      *
**********************************/

using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace lab {

  [Serializable]
  class TextFile {
    string Path { get; set; }
    public static void Redactor(string Path) {

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
      //можно дописать еще свойства
      fs.Close();
    }
    public void XmlSerializer(XmlSerializer serializer) {
      XmlSerializer bf = new XmlSerializer(this.GetType());
    }
  }

  class Search {
    public static void Searching(string Path, string[] KeyWords) {

    }
  }

  internal class Program {
    static void Main(string[] args) {
      Console.WriteLine("что вы хотите сделать:\n1 - редактировать файл\n2 - индексация файлов в определенной директории");
      int Choice = Convert.ToInt32(Console.ReadLine());
      if (Choice == 1) {
        Console.WriteLine("Введите путь к файлу");
        string MyPath = Console.ReadLine();
        TextFile.Redactor(MyPath);

      } else if(Choice == 2) {
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
    }
  }
}
