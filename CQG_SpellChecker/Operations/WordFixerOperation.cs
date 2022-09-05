using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CQG_SpellChecker.Program;

namespace CQG_SpellChecker.Operations
{
    internal class WordFixerOperation : IOperation
    {
        public string Name => "WordFixer";
        public void Execute()
        {

        }
        public static string WordFix(string s)
        {
            string longestWord; 
            string shortestWord;
            int differentLetters = 0; //количество различных букв между словами
            List<string> variants = new List<string>(); //Варианты слов, которые подходят после проверок
            List<int> WrongLetters = new List<int>(); //Количество ошибок в каждом слове, которое прошло проверку

            foreach (var word in Storages.WordStorages.Dictinary) //Сверяем каждое слово с словом из словаря
            {
                if (word.Length > s.Length) //Находим наиболее длинное сравниваемое слово из словаря и исходного текста
                {
                    longestWord = word;
                    shortestWord = s;
                }
                else
                {
                    longestWord = s;
                    shortestWord = word;
                }

                string result = longestWord.Trim(shortestWord.ToCharArray()); //Находим количество различных букв 
                if (!shortestWord.Contains(result) && longestWord.Length == shortestWord.Length)
                {
                    differentLetters += 2;
                }
                else
                {
                    differentLetters += result.Length;
                }

                if (result.Length == 0 && longestWord.Length != shortestWord.Length) //При отсутствии различных символов проверяем слово посимвольно, пока количество различных символов не будет больше 2
                {
                    for (int i = 0; i <= shortestWord.Length - 1; i++)
                    {
                        if (shortestWord[i] != longestWord[i] && differentLetters <= 2)
                        {
                            differentLetters++;
                        }
                    }
                }

                if (result.Length < 2 && differentLetters <= 2) //Собиарем подходящие слова
                {
                    variants.Add(word);
                    WrongLetters.Add(differentLetters);
                }

                differentLetters = 0;
            }

            if (variants.Count == 1) //Если подходящее слово только одно выводим его
            {
                return variants[0];
            }
            else if (variants.Count > 1) //Если подходящих слов больше одного, находим все слова с минимальным количеством ошибок и выводим их в фигурных скобках
            {
                string result = "";
                int resultsCount = 0;
                for (int j = 0; j < variants.Count; j++)
                {
                    if (WrongLetters.Min() == WrongLetters[j])
                    {
                        resultsCount++;
                        result += variants[j] + " ";
                    }
                    if (j == variants.Count - 1 && resultsCount == 1)
                    {
                        return result;
                    }
                }
                result = result.Substring(0, result.Length - 1);
                return $"{{{result}}}";
            }
            return $"{{{s}?}}"; //Выводим слово без изменений, если в нем более 2-х ошибок
        }
    }
}
