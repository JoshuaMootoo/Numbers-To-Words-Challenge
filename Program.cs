using System;
using System.Collections;
using System.Collections.Generic;

namespace NumberToWords
{
    class Program
    {
        static Dictionary<int, string> units = new Dictionary<int, string>
        {
            { 0, "" },
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
            { 4, "Four" },
            { 5, "Five" },
            { 6, "Six" },
            { 7, "Seven" },
            { 8, "Eight" },
            { 9, "Nine" }
        };

        static Dictionary<int, string> teens = new Dictionary<int, string>
        {
            { 10, "Ten" },
            { 11, "Eleven" },
            { 12, "Twelve" },
            { 13, "Thirteen" },
            { 14, "Fourteen" },
            { 15, "Fifteen" },
            { 16, "Sixteen" },
            { 17, "Seventeen" },
            { 18, "Eighteen" },
            { 19, "Nineteen" }
        };

        static Dictionary<int, string> tens = new Dictionary<int, string>
        {
            { 2, "Twenty" },
            { 3, "Thirty" },
            { 4, "Forty" },
            { 5, "Fifty" },
            { 6, "Sixty" },
            { 7, "Seventy" },
            { 8, "Eighty" },
            { 9, "Ninety" }
        };

        static string hundred = " Hundred",
               thousand = " Thousand",
               andWord = " and ",
               comma = ",",
               dash = "-";


        static void Main()
        {
            int number = 0;

            while (true)
            {
                Console.Write("Enter a number between 1 and 100,000: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out number) && number >= 0 && number <= 100000)
                    break;

                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            string result = ConvertNumberToWords(number);
            Console.WriteLine($"You entered: {number}");
            Console.WriteLine($"In words: {result}");
        }

        static string TensAndUnits(int number)
        {
            string sixDigitNumber = number.ToString("D6");

            string result = string.Empty;

            // Sets the output to zero if the number is zero
            if (int.Parse(sixDigitNumber) == 0) result = "Zero";

            // Handles the case for numbers between 1 and 9
            else if (units.ContainsKey(int.Parse(sixDigitNumber.Substring(5, 1))) && int.Parse(sixDigitNumber.Substring(4, 1)) == 0)
                result = units[int.Parse(sixDigitNumber.Substring(5, 1))].ToString();

            // Handles the case for numbers between 10 and 19
            else if (int.Parse(sixDigitNumber.Substring(4, 2)) >= 10 && int.Parse(sixDigitNumber.Substring(4, 2)) <= 19)
                result = teens[int.Parse(sixDigitNumber.Substring(4, 2))].ToString();

            // Handles the case for numbers between 20 and 99 where the unit is not zero
            else if (int.Parse(sixDigitNumber.Substring(4, 2)) > 19 && int.Parse(sixDigitNumber.Substring(5,1)) != 0)
                result = tens[int.Parse(sixDigitNumber.Substring(4, 1))] + dash + units[int.Parse(sixDigitNumber.Substring(5, 1))].ToString();

            // Handles the case for numbers between 20 and 99 where the unit is zero
            else if (int.Parse(sixDigitNumber.Substring(4, 2)) > 19 && int.Parse(sixDigitNumber.Substring(5, 1)) == 0)
                result = tens[int.Parse(sixDigitNumber.Substring(4, 1))];

            return result;
        }

        static string Hundreds(int number)
        {
            string sixDigitNumber = number.ToString("D6");

            string result = string.Empty;

            Console.WriteLine(int.Parse(sixDigitNumber.Substring(3, 1)));

            if (int.Parse(sixDigitNumber.Substring(3, 1)) == 0)
                result = TensAndUnits(number);
            // Handles the case for numbers between 100 and 999 where the tens and units are not zero
            else if (int.Parse(sixDigitNumber.Substring(3, 1)) > 0 && int.Parse(sixDigitNumber.Substring(4,2)) != 0)
                result = units[int.Parse(sixDigitNumber.Substring(3, 1))] + hundred + " " + andWord + " " + TensAndUnits(number);

            // Handles the case for numbers between 100 and 999 where the tens and units are zero
            else if (int.Parse(sixDigitNumber.Substring(3, 1)) > 0 && int.Parse(sixDigitNumber.Substring(4, 2)) == 0)
                result = units[int.Parse(sixDigitNumber.Substring(3, 1))] + hundred;

            return result;
        }

        static string ConvertNumberToWords(int number)
        {
            return Hundreds(number);
        }
    }
}



/*
01 - one
02 - two
03 - three
04 - four
05 - five
06 - six
07 - seven
08 - eight
09 - nine
10 - ten
11 - eleven
12 - twelve
13 - thirteen
14 - fourteen
15 - fifteen
16 - sixteen
17 - seventeen
18 - eighteen
19 - nineteen
20 - twenty
30 - thirty
40 - forty
50 - fifty
60 - sixty
70 - seventy
80 - eighty
90 - ninety
100 - hundred
1000 - thousand
*/