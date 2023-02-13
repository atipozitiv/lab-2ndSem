using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace lab_1 {
	internal class Program {
		static void Main(string[] args) {
			Console.WriteLine("введите число");
			String FirstNumber = Console.ReadLine();
			int Length = FirstNumber.Length;
			String SecondNumber = "";
      Char SecondDigit = FirstNumber[1];
      for (int Position = 0; Position < Length; ++Position) {
				if (Position != 1) {
					SecondNumber += FirstNumber[Position];
				}
			}
			SecondNumber += SecondDigit;
			Console.WriteLine(SecondNumber);
			Console.ReadKey();
		}
	}
}
