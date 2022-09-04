using System;
using System.Collections.Generic;
using System.Linq;

namespace CQG_SpellChecker
{
    class Program
    {
        public static List<string> Dictinary = new List<string>();
        public static List<string> WrongWords = new List<string>();
        public static List<string> Result = new List<string>();
        public static List<string> outputWords = new List<string>();

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
            int comparesLetters = 0; //Совподающие буквы
            int LettersEdits = 0;

            outputWords.Clear();
            
            foreach (var word in Dictinary) //Сверяем каждое слово с словом из словаря
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (i < s.Length - 1 && i < word.Length)
                    {
                        if (LettersEdits == 0 && word[i] == s[i]) 
                        {
                            comparesLetters++;
                        }
                        else if (LettersEdits == 1 && word[i] == s[i + 1])
                        {
                            comparesLetters++;
                        }
                        else
                        {
                            LettersEdits++;
                        }

                        if (LettersEdits > 2)
                        {
                            
                        }
                        if (i == s.Length - 1 /* && comparesLetters >= s.Length - 2 && LettersEdits <= 2 */)
                        {
                            return word;
                        }
                    }
                }
                LettersEdits = 0;
                comparesLetters = 0; //Обнуляем счетчик
            }   
            return $"{{{s}?}}";
        }
    }
}

