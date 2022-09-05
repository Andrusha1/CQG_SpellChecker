using System;
using System.Collections.Generic;
using System.Text;

namespace CQG_SpellChecker.Storages
{
    class WordStorages
    {
        public static List<string> Dictinary = new List<string>(); //Словарь
        public static List<string> WrongWords = new List<string>(); //Исходный текст
        public static List<string> Result = new List<string>(); //Выходные слова

        public void DictAddWord(string word) //Добавление слова в словарь
        {
            Dictinary.Add(word);
        }

        public void AddWrongWord(string wrWord) //Добавление слова в исходый текст
        {
            WrongWords.Add(wrWord);
        }

        public bool isDictContain(string word) //Проверка совпадения слова в словаре
        {
            if (Dictinary.Contains(word))
            {
                return true;
            }
            return false;
        }

        public void ResultAdd(string word) //Добавление слова в выходной лист
        {
            Result.Add(word);
        }
    }
}
