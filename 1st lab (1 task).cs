using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
	internal class Program
	{
		static void Main(string[] args) {
			Console.WriteLine("введите число");
			int Number = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("в какую степень его возвести?");
			int Power = Convert.ToInt32(Console.ReadLine());
			int NewNumber = Number;
			int StartPower = Power;
			while (Power > 1) {
				NewNumber *= Number;
				Power -= 1;
			}
			Console.WriteLine(Number + " в степени " + StartPower + " = " + NewNumber);
			Console.ReadKey();
		}
	}
}
