using CQG_SpellChecker.Storages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static CQG_SpellChecker.Program;
using static System.Net.Mime.MediaTypeNames;

namespace CQG_SpellChecker.Operations
{
    internal class CollectWordOperation : IOperation
    {
        public string Name => "Collecting words";

        public void Execute()
        {
            CollectWords();
        }

        public void CollectWords()
        {
            WordStorages storage = new WordStorages(); //Используем хранилище слов

            string path = ""; //Создаем переменную пути
            while (path == "") //Пока путь не написан спрашиваем заново
            {
                Console.WriteLine("Введите путь до файла: ");
                path = Console.ReadLine();
            }
            string input = ""; //Считываем всю информацию из файла в переменную input
            try 
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    input = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Файл не может быть прочитан или он отсутствует.");
                Environment.Exit(0);
            }
            try //Проверяем формат полученного текста, при успешной проверке добавляем слова в хранилища
            {
                int counter = 0;
                input = input.Replace("\n", " "); //убираем переносы по строкам
                input = input.Replace("\r", "");
                var inputArray = input.Split(" ");
                while (inputArray[counter] != "===") //Считываем слова до "===" и добавляем их в словарь
                {
                    if (inputArray[counter].Length >= 50) //Проверяем слова на длину
                    {
                        Console.WriteLine("Длина слова в словаре или в тексте не может быть более 50 символов.");
                        Environment.Exit(0);
                    }
                    storage.DictAddWord(inputArray[counter]);
                    counter++;
                }
                counter++;
                while (inputArray[counter] != "===") //Считываем слова до второго "===" и добавляем их в исходный текст
                {
                    if (inputArray[counter].Length >= 50) //Проверяем слова на длину
                    {
                        Console.WriteLine("Длина слова в словаре или в тексте не может быть более 50 символов.");
                        Environment.Exit(0);
                    }
                    storage.AddWrongWord(inputArray[counter]);
                    counter++;
                }
            }
            catch (Exception e) //Выводим ошибку
            {
                Console.WriteLine("Неверный формат данных.");
                Environment.Exit(0);
            }


            for (int i = 0; i < WordStorages.WrongWords.Count; i++) //Проверяем слова 
            {
                if (storage.isDictContain(WordStorages.WrongWords[i].ToLower())) //Если слово содержится в словаре то сохраняем его без изменений
                {
                    storage.ResultAdd(WordStorages.WrongWords[i]);
                }
                else //Если слова нет в словаре вызываем проверку слова
                {
                    storage.ResultAdd(WordFixerOperation.WordFix(WordStorages.WrongWords[i]));
                }
            }

            string output = ""; 
            foreach (var x in WordStorages.Result) //Добавляем все исправленные слова в вывод
            {
                output += x + " ";
            }
            output = output.Replace("  ", " ");
            using (StreamWriter writer = new StreamWriter("Result.txt", false)) //Записываем вывод в выходной файл
            {
                writer.WriteLine(output);
            }
        }
    }
}
