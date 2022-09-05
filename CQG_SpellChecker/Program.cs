using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQG_SpellChecker.Operations;
using CQG_SpellChecker.Storages;
using static System.Net.Mime.MediaTypeNames;

namespace CQG_SpellChecker
{
    partial class Program
    {
        static void Main(string[] args)
        {
            IOperation[] operations = new IOperation[]
           {
                new CollectWordOperation(),
                new WordFixerOperation()
           };

            foreach(var x in operations) //Вызываем каждую операцию из списка операций выше
            {
                x.Execute();
            }
        }
        
    }
}