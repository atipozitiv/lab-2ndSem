/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* Перегрузка операций             *
* 20.2.2023                       *
**********************************/

//Указал формат c т.к. почему-то неправильно выделяет текст в cs

using Microsoft.Win32;
using System;
using System.ComponentModel;

namespace lab {

  class Overload {
    public static void Plus(int FirstDigit, int SecondDigit) {
      Console.WriteLine($"{FirstDigit} + {SecondDigit} = {FirstDigit + SecondDigit}");
    }
    public static void Plus(int FirstDigit, int SecondDigit, int ThirdDigit) {
      Console.WriteLine($"{FirstDigit} + {SecondDigit} + {ThirdDigit} = {FirstDigit + SecondDigit + ThirdDigit}");
    }
    public static void Multiply(int FirstDigit, int SecondDigit) {
      Console.WriteLine($"{FirstDigit} * {SecondDigit} = {FirstDigit * SecondDigit}");
    }
    public static void Multiply(float FirstDigit, float SecondDigit) {
      float Result = FirstDigit * SecondDigit;
      Console.WriteLine($"{FirstDigit} * {SecondDigit} = {Result}");
    }
    public static void Compare(int FirstDigit, int SecondDigit) {
      if (FirstDigit > SecondDigit) {
        Console.WriteLine($"{FirstDigit} > {SecondDigit}");
      } else if (SecondDigit > FirstDigit) {
        Console.WriteLine($"{FirstDigit} < {SecondDigit}");
      } else {
        Console.WriteLine("Числа равны");
      }
    }
    public static void Compare(float FirstDigit, float SecondDigit) {
      if (FirstDigit > SecondDigit) {
        Console.WriteLine($"{FirstDigit} > {SecondDigit}");
      } else if (SecondDigit > FirstDigit) {
        Console.WriteLine($"{FirstDigit} < {SecondDigit}");
      } else {
        Console.WriteLine("Числа равны");
      }
    }
    public static void CompareWithEqual(int FirstDigit, int SecondDigit) {
      if (FirstDigit > SecondDigit) {
        Console.WriteLine($"{FirstDigit} >= {SecondDigit}");
      } else {
        Console.WriteLine($"{FirstDigit} <= {SecondDigit}");
      }
    }
    public static void CompareWithEqual(float FirstDigit, float SecondDigit) {
      if (FirstDigit > SecondDigit) {
        Console.WriteLine($"{FirstDigit} >= {SecondDigit}");
      } else {
        Console.WriteLine($"{FirstDigit} <= {SecondDigit}");
      }
    }
    public static void Equal(int FirstDigit, int SecondDigit) {
      if (FirstDigit == SecondDigit) {
        Console.WriteLine($"{FirstDigit} == {SecondDigit}");
      } else {
        Console.WriteLine($"{FirstDigit} != {SecondDigit}");
      }
    }
    public static void Equal(float FirstDigit, float SecondDigit) {
      if (FirstDigit == SecondDigit) {
        Console.WriteLine($"{FirstDigit} == {SecondDigit}");
      } else {
        Console.WriteLine($"{FirstDigit} != {SecondDigit}");
      }
    }
    public static string Transform(int Digit) {
      return Convert.ToString(Digit);
    }
    public static int Transform(string Digit) {
      return Convert.ToInt32(Digit);
    }
    public static bool Bool(bool FirstBool, bool SecondBool) {
      return FirstBool && SecondBool;
    }
    public static bool Bool(bool FirstBool, bool SecondBool, bool ThirdBool) {
      return FirstBool && SecondBool && ThirdBool;
    }
    public static int Determinant(SquareMatrix Matrix, int MatrixSize) {
      int Result = 0;

      for (int FirstDigit = 0; FirstDigit < MatrixSize; ++FirstDigit) {
        int WhatAdd = 1;
        for (int SecondDigit = 0; SecondDigit < MatrixSize; ++SecondDigit) {
          int SecondPos = FirstDigit + SecondDigit;
          if (SecondPos > MatrixSize - 1) SecondPos = SecondPos - MatrixSize;
          WhatAdd *= Matrix.Digits[SecondDigit, SecondPos];
        }
        Result += WhatAdd;
      }

      for (int FirstDigit = MatrixSize - 1; FirstDigit >= 0; --FirstDigit) {
        int WhatDelete = 1;
        for (int SecondDigit = 0; SecondDigit < MatrixSize; ++SecondDigit) {
          int SecondPos = FirstDigit - SecondDigit;
          if (SecondPos < 0) SecondPos = SecondPos + MatrixSize;
          WhatDelete *= Matrix.Digits[SecondDigit, SecondPos];
        }
        Result -= WhatDelete;
      }

      return Result;
    }

    public static int Determinant(SquareMatrix Matrix) {
      int Result = 0;

      int MatrixSize = Matrix.Size;
      for (int FirstDigit = 0; FirstDigit < MatrixSize; ++FirstDigit) {
        int WhatAdd = 1;
        for (int SecondDigit = 0; SecondDigit < MatrixSize; ++SecondDigit) {
          int SecondPos = FirstDigit + SecondDigit;
          if (SecondPos > MatrixSize - 1) SecondPos = SecondPos - MatrixSize;
          WhatAdd *= Matrix.Digits[SecondDigit, SecondPos];
        }
        Result += WhatAdd;
      }

      for (int FirstDigit = MatrixSize - 1; FirstDigit >= 0; --FirstDigit) {
        int WhatDelete = 1;
        for (int SecondDigit = 0; SecondDigit < MatrixSize; ++SecondDigit) {
          int SecondPos = FirstDigit - SecondDigit;
          if (SecondPos < 0) SecondPos = SecondPos + MatrixSize;
          WhatDelete *= Matrix.Digits[SecondDigit, SecondPos];
        }
        Result -= WhatDelete;
      }

      return Result;
    }

    public static void ReverseMatrix(SquareMatrix Matrix, int Size) {
      Console.WriteLine("Обратная матрица: ");
      Console.Write("\n1 / " + Overload.Determinant(Matrix) + " *\n");
      SquareMatrix NewMatrix = new SquareMatrix();
      for (int FirstDigit = 0; FirstDigit < Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Size; ++SecondDigit) {
          int FirstFlag = 0;
          int SecondFlag = 0;
          for (int NewFirstDigit = 0; NewFirstDigit < Size - 1; ++NewFirstDigit) {
            for (int NewSecondDigit = 0; NewSecondDigit < Size - 1; ++NewSecondDigit) {         
              if (NewFirstDigit == FirstDigit) FirstFlag = 1;
              if (NewSecondDigit == SecondDigit)  SecondFlag = 1; 
              NewMatrix.Digits[NewFirstDigit, NewSecondDigit] = Matrix.Digits[NewFirstDigit + FirstFlag, NewSecondDigit + SecondFlag];
            }
          }
          int Result = Convert.ToInt32(Math.Pow(-1.0, Convert.ToDouble(FirstDigit * SecondDigit))) * Overload.Determinant(NewMatrix);
          Console.Write(Result + " ");
        }
        Console.Write("\n");
      }
    }
  }

  public class BigSize : System.Exception {
    public BigSize()
    : base() { }
    public BigSize(string message)
    : base(message) { }
    public BigSize
    (string message, System.Exception inner)
    : base(message, inner) { }

  }

  class SquareMatrix {
    public int Size;
    public int[,] Digits = new int[10, 10];
    public void ToString(SquareMatrix Matrix) {
      String Result = "";
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          Result += Matrix.Digits[FirstDigit, SecondDigit] + " ";
        }
      Result += "\n";
      }
      Console.WriteLine(Result);
    }
    
    public int CompareTo(int FirstDigit, int SecondDigit) {
      if (FirstDigit > SecondDigit) return 1;
      if (FirstDigit < SecondDigit) return -1;
      return 0;
    }

    public SquareMatrix Prototip() => new SquareMatrix();

  }
  internal class Program {
    static void Main(string[] args) {
      Console.Write("введите размер матрицы(до 10):");
      SquareMatrix MyMatrix = new SquareMatrix();
      MyMatrix.Size = Convert.ToInt32(Console.ReadLine());
      if (MyMatrix.Size > 10) throw new BigSize("Слишком большой размер");
      Console.Clear();
      Random Random = new Random();
      for (int Row = 0; Row < MyMatrix.Size; ++Row) {
        for (int Column = 0; Column < MyMatrix.Size; ++Column) {
          MyMatrix.Digits[Row, Column] = Random.Next(0, 10);
          Console.Write(MyMatrix.Digits[Row, Column] + " ");
        }
        Console.Write("\n");
      }
      Console.Write("\n\n");

      Console.WriteLine("Перегрузка операций '+':");
      Overload.Plus(4, 5);
      Overload.Plus(4, 5, 6);
      Console.WriteLine("Перегрузка операции '*':");
      Overload.Multiply(2, 5);
      Overload.Multiply(2.4f, 7.2f);
      Console.WriteLine("Перегрузка операции '> и <':");
      Overload.Compare(2, 5);
      Overload.Compare(2.4f, 2.1f);
      Console.WriteLine("Перегрузка операции '>= и <=':");
      Overload.CompareWithEqual(7, 7);
      Overload.CompareWithEqual(45.1f, 5.2f);
      Console.WriteLine("Перегрузка операции '== и !='");
      Overload.Equal(2, 1);
      Overload.Equal(5.2f, 5.2f);
      Console.WriteLine("Перегрузка приведения типов:");
      Console.WriteLine("32 + 1 = " + (Overload.Transform(32) + 1));
      Console.WriteLine("32 + 1 = " + (Overload.Transform("32") + 1));
      Console.WriteLine("Перегрузка true false:");
      Console.WriteLine("True && False = " + Overload.Bool(true, false));
      Console.WriteLine("True && True && True = " + Overload.Bool(true, true, true));
      Console.WriteLine("Детерминант матрицы(с указанием размера) = " + Overload.Determinant(MyMatrix, MyMatrix.Size));
      Console.WriteLine("Детерминант матрицы(без указания размера) = " + Overload.Determinant(MyMatrix));
      MyMatrix.ToString(MyMatrix);
      Console.WriteLine("CompareTo: " + MyMatrix.CompareTo(MyMatrix.Digits[0, 0], MyMatrix.Digits[0, 1]));
      SquareMatrix MyMatrix2 = new SquareMatrix();
      for (int Row = 0; Row < MyMatrix.Size; ++Row) {
        for (int Column = 0; Column < MyMatrix.Size; ++Column) {
          MyMatrix.Digits[Row, Column] = Random.Next(0, 10);
        }
      }
      Console.WriteLine(MyMatrix.Equals(MyMatrix2));
      Console.WriteLine(MyMatrix.GetHashCode());
      SquareMatrix NewMyMatrix = MyMatrix.Prototip();
      
      Console.ReadKey();
      
    }
  }
}
