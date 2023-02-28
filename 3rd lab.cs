/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* ООП на C#                       *
* 13.2.2023                       *
**********************************/

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace lab {
  class Overload {
    public static int Determinant(SquareMatrix Matrix) {
      int Result = 0;
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        int WhatAdd = 1;
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          int SecondPos = FirstDigit + SecondDigit;
          if (SecondPos > Matrix.Size - 1) SecondPos = SecondPos - Matrix.Size;
          WhatAdd *= Matrix.Digits[SecondDigit, SecondPos];
        }
        Result += WhatAdd;
      }

      for (int FirstDigit = Matrix.Size - 1; FirstDigit >= 0; --FirstDigit) {
        int WhatDelete = 1;
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          int SecondPos = FirstDigit - SecondDigit;
          if (SecondPos < 0) SecondPos = SecondPos + Matrix.Size;
          WhatDelete *= Matrix.Digits[SecondDigit, SecondPos];
        }
        Result -= WhatDelete;
      }
      return Result;
    }

    public static void ReverseMatrix(SquareMatrix Matrix) {
      Console.WriteLine("Обратная матрица: ");
      Console.Write("\n1 / " + Overload.Determinant(Matrix) + " *\n");
      SquareMatrix NewMatrix = new SquareMatrix();
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          int FirstFlag = 0;
          int SecondFlag = 0;
          for (int NewFirstDigit = 0; NewFirstDigit < Matrix.Size - 1; ++NewFirstDigit) {
            for (int NewSecondDigit = 0; NewSecondDigit < Matrix.Size - 1; ++NewSecondDigit) {
              if (NewFirstDigit == FirstDigit) {
                FirstFlag = 1;
              }
              if (NewSecondDigit == SecondDigit) {
                SecondFlag = 1;
              }
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
    public int CompareTo(SquareMatrix FirstMatrix, SquareMatrix SecondMatrix) {
      if (FirstMatrix.Sum(FirstMatrix) > SecondMatrix.Sum(SecondMatrix)) return 1;
      if (FirstMatrix.Sum(FirstMatrix) < SecondMatrix.Sum(SecondMatrix)) return -1;
      return 0;
    }
    public static SquareMatrix operator+ (SquareMatrix Matrix, int Digit) {
      for (int RowIndex = 0; RowIndex < Matrix.Size; ++RowIndex) {
        for (int ColumnIndex = 0; ColumnIndex < Matrix.Size; ++ColumnIndex) {
          Matrix.Digits[RowIndex, ColumnIndex] = Digit + Matrix.Digits[RowIndex,ColumnIndex];
        }
      }
      return Matrix;
    }
    public static SquareMatrix operator* (SquareMatrix Matrix, int Digit) {
      for (int RowIndex = 0; RowIndex < Matrix.Size; ++RowIndex) {
        for (int ColumnIndex = 0; ColumnIndex < Matrix.Size; ++ColumnIndex) {
          Matrix.Digits[RowIndex, ColumnIndex] = Digit * Matrix.Digits[RowIndex, ColumnIndex];
        }
      }
      return Matrix;
    }
    public int Sum(SquareMatrix Matrix) {
      int Sum = 0;
      for (int RowIndex = 0; RowIndex < Matrix.Size; ++RowIndex) {
        for (int ColumnIndex = 0; ColumnIndex < Matrix.Size; ++ColumnIndex) {
          Sum += Matrix.Digits[RowIndex, ColumnIndex];
        }
      }
      return Sum;
    }
    public static bool operator> (SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) > MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator<(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) < MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator>=(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) >= MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator<=(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) <= MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public void ToString(SquareMatrix Matrix) {
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          Console.Write("{0,5}", Matrix.Digits[FirstDigit, SecondDigit]);
        }
        Console.WriteLine();
      }
    }
    public static bool operator==(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) == MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator!=(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) != MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static explicit operator string(SquareMatrix Matrix) {
      string Result = "";
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          Result += "   " + Convert.ToString(Matrix.Digits[FirstDigit, SecondDigit]);
          Console.Write("{0,5}", Matrix.Digits[FirstDigit, SecondDigit]);
        }
        Result += "\n";
      }
      return Result;
    }
    public static bool operator true(SquareMatrix Matrix) {
      bool Result = true;
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          if (Matrix.Digits[FirstDigit, SecondDigit] == 0) {
            Result = false;
          }
        }
      }
      return Result;
    }
    public static bool operator false(SquareMatrix Matrix) {
      bool Result = false;
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          if (Matrix.Digits[FirstDigit, SecondDigit] == 0) {
            Result = true;
          }
        }
      }
      return Result;
    }
    public override bool Equals(object other) {
      bool result = false;
      if (other is SquareMatrix) {
        var param = other as SquareMatrix;
        if ((param.Digits == this.Digits) && (param.Size == this.Size))
          result = true;
      }
      return result;
    }
    public override int GetHashCode() {
      return (int)this.Digits[0,0];
    }
  }
  class Prototip {
    public int Size;
    public int[,] Digits = new int[10, 10];
    public SquareMatrix ds;
    public Prototip() {
      ds = new SquareMatrix();
    }
  }
  internal class Program {
    static void Main(string[] args) {
      Console.Write("введите размер матрицы(до 10):");
      SquareMatrix MyMatrix = new SquareMatrix();
      MyMatrix.Size = Convert.ToInt32(Console.ReadLine());
      if (MyMatrix.Size > 10) throw new BigSize("Слишком большой размер");
      Console.Clear();
      Random Random = new Random();
      SquareMatrix SecondMatrix = new SquareMatrix();
      for (int RowIndex = 0; RowIndex < MyMatrix.Size; ++RowIndex) {
        for (int ColumnIndex = 0; ColumnIndex < MyMatrix.Size; ++ColumnIndex) {
          MyMatrix.Digits[RowIndex, ColumnIndex] = Random.Next(0, 10);
          SecondMatrix.Digits[RowIndex, ColumnIndex] = Random.Next(0, 10);
        }
      }
      Console.WriteLine("Ваша матрица:");
      MyMatrix.ToString(MyMatrix);
      SecondMatrix.Size = MyMatrix.Size;

      Console.WriteLine("Выберите действие:\n1 - прибавить к матрице число\n2 - умножить матрицу на число\n3 - операция >\n4 - операция <\n5 - операция >=\n6 - операция <=\n7 - операция ==");
      Console.WriteLine("8 - операция !=\n9 - true/false\n10 - вывести детерминант");
      int Choice = Convert.ToInt32(Console.ReadLine());
      switch (Choice) {
        case 1:
          Console.WriteLine("Введите число, которое нужно прибавить: ");
          int DigitPlus = Convert.ToInt32(Console.ReadLine());
          MyMatrix.ToString(MyMatrix + DigitPlus);
          break;
        case 2:
          Console.WriteLine("Введите число, на которое нужно умножить: ");
          int DigitMultiply = Convert.ToInt32(Console.ReadLine());
          MyMatrix.ToString(MyMatrix * DigitMultiply);
          break;
        case 3:
          Console.WriteLine("Вторая матрица: ");
          SecondMatrix.ToString(SecondMatrix);
          Console.WriteLine(MyMatrix > SecondMatrix);
          break;
        case 4:
          SecondMatrix.ToString(SecondMatrix);
          Console.WriteLine(MyMatrix < SecondMatrix);
          break;
        case 5:
          SecondMatrix.ToString(SecondMatrix);
          Console.WriteLine(MyMatrix >= SecondMatrix);
          break;
        case 6:
          SecondMatrix.ToString(SecondMatrix);
          Console.WriteLine(MyMatrix <= SecondMatrix);
          break;
        case 7:
          SecondMatrix.ToString(SecondMatrix);
          Console.WriteLine(MyMatrix == SecondMatrix);
          break;
        case 8:
          SecondMatrix.ToString(SecondMatrix);
          Console.WriteLine(MyMatrix != SecondMatrix);
          break;
        case 9:
          if (MyMatrix) {
            Console.WriteLine("в матрице нету нулей");
          } else {
            Console.WriteLine("в матрице есть нули");
          }
          break;
        case 10:
          Console.WriteLine(Overload.Determinant(MyMatrix));
          break;
      }
      Console.ReadKey();
    }
  }
}
