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
               comma = ", ",
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

            int sixDigitNumberInt = int.Parse(sixDigitNumber);
            int _units = int.Parse(sixDigitNumber.Substring(5, 1));
            int _teens = int.Parse(sixDigitNumber.Substring(4, 2));
            int _tens = int.Parse(sixDigitNumber.Substring(4, 1));


            string result = string.Empty;

            // Sets the output to zero if the number is zero
            if (sixDigitNumberInt == 0) result = "Zero";

            // Handles the case for numbers between 1 and 9
            else if (units.ContainsKey(_units) && _tens == 0)
                result = units[_units];

            // Handles the case for numbers between 10 and 19
            else if (_teens >= 10 && _teens <= 19)
                result = teens[_teens];

            // Handles the case for numbers between 20 and 99 where the unit is not zero
            else if (_teens > 19 && _units != 0)
                result = tens[_tens] + dash + units[_units];

            // Handles the case for numbers between 20 and 99 where the unit is zero
            else if (_teens > 19 && _units == 0)
                result = tens[_tens];

            return result;
        }

        static string Hundreds(int number)
        {
            string sixDigitNumber = number.ToString("D6");

            int _hundreds = int.Parse(sixDigitNumber.Substring(3, 1));
            int _tensAndUnits = int.Parse(sixDigitNumber.Substring(4, 2));

            string result = string.Empty;

            if (_hundreds == 0)
                result = TensAndUnits(number);
            // Handles the case for numbers between 100 and 999 where the tens and units are not zero
            else if (_hundreds > 0 && _tensAndUnits != 0)
                result = units[_hundreds] + hundred + andWord + TensAndUnits(number);

            // Handles the case for numbers between 100 and 999 where the tens and units are zero
            else if (_hundreds > 0 && _tensAndUnits == 0)
                result = units[_hundreds] + hundred;

            return result;
        }

        static string TenThousandsAndThousands(int number)
        {
            string sixDigitNumber = number.ToString("D6");

            int _tenThousandsAndThousands = int.Parse(sixDigitNumber.Substring(1, 2));
            int _tenThousands = int.Parse(sixDigitNumber.Substring(1, 1));
            int _thousands = int.Parse(sixDigitNumber.Substring(2, 1));
            int _hundreds = int.Parse(sixDigitNumber.Substring(3, 1));
            int _tensAndUnits = int.Parse(sixDigitNumber.Substring(4, 2));


            string result = string.Empty;

            if (_tenThousandsAndThousands == 0)
                result = Hundreds(number);

            else if (units.ContainsKey(_thousands) && _tenThousands == 0)
            {
                if (_hundreds != 0) result = units[_thousands] + thousand + comma + Hundreds(number);
                else result = units[_thousands] + thousand;
            }

            else if (_tenThousandsAndThousands >= 10 && _tenThousandsAndThousands <= 19)
            {
                if (_hundreds != 0) result = teens[_tenThousandsAndThousands] + thousand + comma + Hundreds(number);
                else if (_hundreds == 0 && _tensAndUnits != 0) result = teens[_tenThousandsAndThousands] + thousand + andWord + TensAndUnits(number);
                else result = teens[_tenThousandsAndThousands] + thousand;
            }

            else if (_tenThousandsAndThousands > 19)
            {
                if (_hundreds != 0) result = tens[_tenThousands] + dash + units[_thousands] + thousand + comma + Hundreds(number);
                else if (_hundreds == 0 && _tensAndUnits != 0) result = tens[_tenThousands] + dash + units[_thousands] + thousand + andWord + TensAndUnits(number);
                else result = tens[_tenThousands] + dash + units[_thousands] + thousand;
            }
            return result;
        }

        static string ConvertNumberToWords(int number)
        {
            if (number == 100000)
                return "One Hundred Thousand";
            else
                return TenThousandsAndThousands(number);
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