using System;
using System.Collections.Generic;

namespace CQG_SpellChecker
{
    class Program
    {
        public static List<string> Dictinary = new List<string>();
        public static List<string> WrongWords = new List<string>();
        public static List<string> Result = new List<string>();

        static void Main(string[] args)
        {
            string input = "rain spain plain plaint pain main mainly the in on fall falls his was === hte rame in pain fells mainy oon teh lain was hints pliant ===";

            int counter = 0;
            var inputArray = input.Split(" ");
            while (inputArray[counter] != "===")
            {
                Dictinary.Add(inputArray[counter]);
                counter++;
            }
            counter++;
            while (inputArray[counter] != "===")
            {
                WrongWords.Add(inputArray[counter]);
                counter++;
            }

            for (int i = 0; i < WrongWords.Count - 1; i++)
            {
                if (Dictinary.Contains(WrongWords[i]))
                {
                    Result.Add(WrongWords[i]);
                }
                else
                {
                    Result.Add(WordFixer(WrongWords[i]));
                }
            }

            foreach (var x in Result)
            {
                Console.WriteLine(x);
            }
        }

        public static string WordFixer(string s)
        {
            int Moves;
            int comparesLetters = 0;

            foreach(var x in Dictinary)
            {
                for(int i = 0; i < s.Length; i++)
                {
                    if (x.Contains(s[i]))
                    {
                        comparesLetters++;
                    }
                    if(x.Length - 1 < s.Length && s.Length < x.Length + 1 && i == s.Length - 1 && comparesLetters >= s.Length - 2)
                    {
                        return x;
                    }
                }
            }
            return $"{{{s}}}";
        }
    }
}

