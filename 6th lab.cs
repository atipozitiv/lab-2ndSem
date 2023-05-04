/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* Делегаты, события               *
* 01.05.2023                      *
**********************************/

/*
 * расширяющие методы: 20-37
 * делегат: 39, 371-400
 */

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace lab {
  public static class Extensions {
    public static int Transposition(this SquareMatrix Matrix) {
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          Console.Write("{0,5}", Matrix.Digits[SecondDigit, FirstDigit]);
        }
        Console.WriteLine();
      }
      return 0;
    }
    public static int Track(this SquareMatrix Matrix) {
      int Result = 0;
      for (int Turn = 0; Turn < Matrix.Size; ++Turn) {
        Result += Matrix.Digits[Turn, Turn];
      }
      return Result;
    }
  }

  public delegate SquareMatrix DiagMatrix(SquareMatrix Matrix);

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

  public class SquareMatrix {
    public int Size;
    public int[,] Digits = new int[10, 10];

    public int CompareTo(SquareMatrix FirstMatrix, SquareMatrix SecondMatrix) {
      if (FirstMatrix.Sum(FirstMatrix) > SecondMatrix.Sum(SecondMatrix)) return 1;
      if (FirstMatrix.Sum(FirstMatrix) < SecondMatrix.Sum(SecondMatrix)) return -1;
      return 0;
    }
    public static SquareMatrix operator +(SquareMatrix Matrix, int Digit) {
      for (int RowIndex = 0; RowIndex < Matrix.Size; ++RowIndex) {
        for (int ColumnIndex = 0; ColumnIndex < Matrix.Size; ++ColumnIndex) {
          Matrix.Digits[RowIndex, ColumnIndex] = Digit + Matrix.Digits[RowIndex, ColumnIndex];
        }
      }
      return Matrix;
    }
    public static SquareMatrix operator *(SquareMatrix Matrix, int Digit) {
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
    public static bool operator >(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) > MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator <(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) < MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator >=(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) >= MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator <=(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) <= MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public void ToString(SquareMatrix Matrix) {
      for (int FirstDigit = 0; FirstDigit < Matrix.Size; ++FirstDigit) {
        for (int SecondDigit = 0; SecondDigit < Matrix.Size; ++SecondDigit) {
          Console.Write("{0,7}", Matrix.Digits[FirstDigit, SecondDigit]);
        }
        Console.WriteLine();
      }
    }
    public static bool operator ==(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
      if (MatrixLeft.Sum(MatrixLeft) == MatrixRight.Sum(MatrixRight)) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator !=(SquareMatrix MatrixLeft, SquareMatrix MatrixRight) {
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
      return (int)this.Digits[0, 0];
    }
  }

  public abstract class IEvent {
    public string EventType { get; set; }
  }
  class Event1 : IEvent {
    public Event1() { EventType = "Diag Matrix:"; }
  }
  class Event2 : IEvent {
    public Event2() { EventType = "Impossible to get Diag Matrix"; }
  }
  public abstract class BaseHandler {
    public BaseHandler() { Next = null; }
    public virtual void Handle(IEvent ev) {
      if (PrivateEvent.EventType == ev.EventType) {
        Console.WriteLine("{0} successfully handled", PrivateEvent.EventType);
      } else {
        Console.WriteLine("Sending event to next Handler...");
        if (Next != null)
          Next.Handle(ev);
        else
          Console.WriteLine("Unknown event. Can't handle.");

      }
    }
    protected void SetNextHandler(BaseHandler newHandler) {
      Next = newHandler;
    }
    protected BaseHandler Next { get; set; }
    protected IEvent PrivateEvent { get; set; }
  }
  class Handler2 : BaseHandler {
    public Handler2() {
      PrivateEvent = new Event2(); Next = new Handler1();
    }
  }
  class Handler1 : BaseHandler {
    public Handler1() {
      PrivateEvent = new Event1(); Next = new Handler2();
    }
  }
  public class ChainApplication {
    public ChainApplication() {
      eventHandler = new Handler1();
    }
    public void Run(int EventCount) {
      if (EventCount == 1) 
      HandleEvent(GenerateRandomEvent());
    }
    private void HandleEvent(IEvent ev) {
      eventHandler.Handle(ev);
    }
    private IEvent GenerateRandomEvent() {
      IEvent result;
      switch (1) {
        case 0: result = new Event1(); break;
        case 1: result = new Event2(); break;
      }
      Console.WriteLine("Your event: {0}", result.EventType);
      return result;
    }
    private BaseHandler eventHandler;
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
          MyMatrix.Digits[RowIndex, ColumnIndex] = Random.Next(-10, 10);
          SecondMatrix.Digits[RowIndex, ColumnIndex] = Random.Next(-10, 10);
        }
      }
      Console.WriteLine("Ваша матрица:");
      MyMatrix.ToString(MyMatrix);
      SecondMatrix.Size = MyMatrix.Size;

      Console.WriteLine("Выберите действие:\n1 - прибавить к матрице число\n2 - умножить матрицу на число\n3 - операция >\n4 - операция <\n5 - операция >=\n6 - операция <=\n7 - операция ==");
      Console.WriteLine("8 - операция !=\n9 - true/false\n10 - вывести детерминант\n11 - ТРАНСПОНИРОВАНИЕ\n12 - СЛЕД\n13 - ДИАГОНАЛЬНЫЙ ВИД");
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
        case 11:
          MyMatrix.Transposition();
          break;
        case 12:
          Console.WriteLine(MyMatrix.Track());
          break;
        case 13:
          DiagMatrix DiagMatrix = delegate (SquareMatrix Matrix) {
            SquareMatrix ReserveMatrix = Matrix;
            for (int MainDiag = 0; MainDiag < Matrix.Size; ++MainDiag) {
              for (int RowIndex = 0; RowIndex < Matrix.Size; ++RowIndex) {
                for (int ColumnIndex = 0; ColumnIndex < Matrix.Size; ++ColumnIndex) { 
                  if ((RowIndex != MainDiag) && (ColumnIndex != MainDiag)) { 
                    Matrix.Digits[RowIndex, ColumnIndex] = ReserveMatrix.Digits[MainDiag, MainDiag] * ReserveMatrix.Digits[RowIndex, ColumnIndex] - ReserveMatrix.Digits[MainDiag, ColumnIndex] * ReserveMatrix.Digits[RowIndex, MainDiag];
                  }
                  if ((ColumnIndex == MainDiag) && (RowIndex != MainDiag)) {
                    Matrix.Digits[RowIndex, ColumnIndex] = 0;
                  }
                }
              }
              ReserveMatrix = Matrix;
              //ReserveMatrix.ToString(ReserveMatrix);
              //Console.WriteLine();
            }
            Matrix.ToString(Matrix);
            Console.ReadKey();
            ChainApplication app = new ChainApplication();
            if (MyMatrix) {
              app.Run(1);
            } else {
              app.Run(2);
            }
            return Matrix;
          };
          DiagMatrix(MyMatrix);
          break;
      }
      Console.ReadKey();
    }
  }
}
