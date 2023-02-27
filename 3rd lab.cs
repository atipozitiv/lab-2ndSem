/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* ООП на C#                       *
* 13.2.2023                       *
**********************************/

using Microsoft.Win32;
using System;
using System.ComponentModel;

namespace lab {
  class Overload {
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
              if (NewSecondDigit == SecondDigit) SecondFlag = 1;
              NewMatrix.Digits[NewFirstDigit, NewSecondDigit] = Matrix.Digits[NewFirstDigit + FirstFlag, NewSecondDigit + SecondFlag];
            }
          }
          int Result = Convert.ToInt32(Math.Pow(-1.0, Convert.ToDouble(FirstDigit * SecondDigit))) * Overload.Determinant(NewMatrix);
          Console.Write(Result + " ");
        }
        Console.Write("\n");
      }
    }

    public static operator+ (SquareMatrix Matrix, int Digit) {

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
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          Console.Write("{0,5}", Matrix.Digits[FirstDigit, SecondDigit]);
        }
        Console.WriteLine(); 
      }
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
      for (int RowIndex = 0; RowIndex < MyMatrix.Size; ++RowIndex) {
        for (int ColumnIndex = 0; ColumnIndex < MyMatrix.Size; ++ColumnIndex) {
          MyMatrix.Digits[RowIndex, ColumnIndex] = Random.Next(0, 10);
        }
      }
    }
  }
}
