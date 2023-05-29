/**********************************
* ПИ-221                          *
* Иванов Тимофей                  *
* Обобщения и лямбда-выражения    *
* 18.05.2023                      *
**********************************/

using System;
using System.Collections;

namespace lab {
  public class Lambda {
    public class Tree : IEnumerable {
      public Knot<T>[] MyTree;
      public Tree(Knot<T>[] Mass) {
        MyTree = new Knot<T>[Mass.Length];
        for (int Index = 0; Index < Mass.Length; ++Index) {
          MyTree[Index] = Mass[Index];
        }
      }
      IEnumerator IEnumerable.GetEnumerator() {
        return (IEnumerator)GetEnumerator();
      }
      public KnotEnum GetEnumerator() {
        return new KnotEnum(MyTree);
      }
    }
    public class KnotEnum: IEnumerator {
      public Knot<T>[] MyTree;
      int Index = -1;
      public KnotEnum(Knot<T>[] List) {
        MyTree = List;
      }
      public bool MoveNext() {
        ++Index;
        return (Index < MyTree.Length);
      }
      public void Reset() {
        Index = -1;
      }
      object IEnumerator.Current {
        get {
          return Current;
        }
      }
      public Knot<T> Current {
        get {
          try {
            return MyTree[Index];
          } catch (IndexOutOfRangeException) {
            throw new InvalidOperationException();
          }
        }
      }
    }
    public class Knot<T> {
      public Knot(T InputValue) {
        Value = InputValue;
      }
      public T Value { get; set; }
      public Knot<T> Parent { get; set; }
      public Knot<T> LeftBaby { get; set; }
      public Knot<T> RightBaby { get; set; }
      public Knot<T> Next(Knot<T> Start) {
        if (RightBaby == null) {
          if (Parent == null) {
            return Start;
          } else {
            return Next(Parent);
          }
        } else {
          var Next = RightBaby;
          while (Next.LeftBaby != null) {
            Next = Next.LeftBaby;
          }
          return Next;
        }
      }
      public Knot<T> Previous(Knot<T> Start) {
        if (LeftBaby == null) {
          if (Parent == null) {
            return Start;
          } else {
            return Previous(Parent);
          }
        } else {
          var Next = LeftBaby;
          while (Next.RightBaby != null) {
            Next = Next.RightBaby;
          }
          return Next;
        }
      }
      public T Current() {
        return Value;
      }
    }
    public delegate Knot<int>[] Sort(Knot<int>[] SortTree);
    static void Main(string[] args) {
      int Index = 0;
      Random Random = new Random();
      Console.WriteLine("сколько узлов заполнить?");
      int KnotCount = Convert.ToInt32(Console.ReadLine());
      Knot<int>[] MyTree = new Knot<int>[KnotCount];
      Knot<int> Start = new Knot<int>(Random.Next(1, 100));
      MyTree[Index] = Start;
      ++Index;

      Sort SortedTree = (SortTree) => {
        Knot<int>[] Result = new Knot<int>[KnotCount];
        int LastMin = 0;// SortTree[KnotIndex].Current();
        int RealIndex = 0;
        for (int KnotIndex = 0; KnotIndex < KnotCount; ++KnotIndex) {
          int RealMin = 1000;
          for (int SecondIndex = 0; SecondIndex < KnotCount; ++SecondIndex) {
            if ((SortTree[SecondIndex].Current() > LastMin) && (SortTree[SecondIndex].Current() < RealMin)) {
              RealMin = SortTree[SecondIndex].Current();
              RealIndex = SecondIndex;
            }
          }
          Result[KnotIndex] = SortTree[RealIndex];
          LastMin = RealMin;
        }
        return Result;
      };

      for (int Turn = 0; Turn < KnotCount - 1; ++Turn) {
        int NextValue = Random.Next(1, 100);
        Knot<int> ActualKnot = Start;
        if (NextValue > Start.Current()) {
          while (NextValue > ActualKnot.Next(ActualKnot).Current()) {
            ActualKnot = ActualKnot.Next(ActualKnot);
            if (ActualKnot == Start) break;
          }
          if (NextValue != ActualKnot.Next(ActualKnot).Current()) {
            MyTree[Index] = ActualKnot.Next(ActualKnot).LeftBaby = new Knot<int>(NextValue);
            ++Index;
            ActualKnot.Next(ActualKnot).LeftBaby.Parent = ActualKnot;
          }
        }
        if (NextValue < Start.Current()) {
          while (NextValue < ActualKnot.Previous(ActualKnot).Current()) {
            ActualKnot = ActualKnot.Previous(ActualKnot);
            if (ActualKnot == Start) break;
          }
          if (NextValue != ActualKnot.Previous(ActualKnot).Current()) {
            MyTree[Index] = ActualKnot.Previous(ActualKnot).RightBaby = new Knot<int>(NextValue);
            ++Index;
            ActualKnot.Previous(ActualKnot).RightBaby.Parent = ActualKnot;
          }
        }
      }
      Console.WriteLine();
      foreach (var Knot in MyTree) {
        Console.WriteLine(Knot.Value);
      }
      Console.WriteLine("\n\n");

      foreach(var Knot in SortedTree(MyTree)) {
        Console.WriteLine(Knot.Value);
      }
      Console.ReadKey();
    }
  }
}
