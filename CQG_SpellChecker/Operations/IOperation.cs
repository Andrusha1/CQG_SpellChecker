namespace CQG_SpellChecker
{
    partial class Program
    {
        public interface IOperation
        {
            string Name { get; }

            void Execute();
        }
    }
}