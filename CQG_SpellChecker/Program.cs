using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string input = "rain spain plain plaint pain mainly main the in on fall falls his was === hte rame in pain fells mainy oon teh lain was hints pliant ===";

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

            

            for (int i = 0; i < WrongWords.Count; i++)
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
            string longestWord;
            string shortestWord;
            int differentLetters = 0;
            List<string> variants = new List<string>();
            List<int> WrongLetters = new List<int>();

            outputWords.Clear();

            foreach (var word in Dictinary) //Сверяем каждое слово с словом из словаря
            {
                if (word.Length > s.Length)
                {
                    longestWord = word;
                    shortestWord = s;
                }
                else
                {
                    longestWord = s;
                    shortestWord = word;
                }

                string result = longestWord.Trim(shortestWord.ToCharArray());
                if (!shortestWord.Contains(result) && longestWord.Length == shortestWord.Length)
                {
                    differentLetters += 2;
                }
                else
                {
                    differentLetters += result.Length;
                }

                if(result.Length == 0)
                {
                    for(int i = 0; i <= shortestWord.Length - 1; i++)
                    {
                        if(shortestWord[i] != longestWord[i] && differentLetters <= 2)
                        {
                            differentLetters++;
                        }
                        if (differentLetters > 2)
                        {
                            return $"{{{s}?}}";
                        }
                    }
                }

                if (result.Length < 2 && differentLetters <= 2)
                {
                    variants.Add(word);
                    WrongLetters.Add(differentLetters);
                }

                differentLetters = 0;
            }

            if (variants.Count == 1)
            {
                return variants[0];
            }
            else if (variants.Count > 1)
            {
                string result = "";
                int resultsCount = 0;
                for(int j = 0; j < variants.Count; j++)
                {
                    if(WrongLetters.Min() == WrongLetters[j])
                    {
                        resultsCount++;
                        result += variants[j] + " ";
                    }
                    if(j == variants.Count - 1 && resultsCount == 1)
                    {
                        return result;
                    }
                }
                result = result.Substring(0, result.Length - 1);
                return $"{{{result}}}";
            }
            return $"{{{s}?}}";
        }
    }
}

/*
if (LettersEdits == 0 && word[i] == s[i]) 
                        {
                            comparesLetters++;
                        }
                        else if (LettersEdits == 1 && (word[i - 1] == s[i] || word[i] == s[i] || s[i-1] == word[i]))
                        {
                            comparesLetters++;
                        }
                        else
                        {
                            LettersEdits++;
                        }

                        if (i == s.Length - 1 && word.Length > s.Length)
                        {
                            LettersEdits += word.Length - s.Length;
                        }
                        if (i == word.Length - 1 && s.Length > word.Length)
                        {
                            LettersEdits += s.Length - word.Length;
                        }

if (LettersEdits > 2)
{
    break;
}
if (i == s.Length - 1 && comparesLetters > s.Length - 2 && LettersEdits <= 2)
{
    variants.Add(word);
    WrongLetters.Add(LettersEdits);
}
*/