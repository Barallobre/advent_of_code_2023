using System.Text;

namespace advent_of_code_23
{
    internal class DataConverter_day1
    {
        public static List<int> getData(string data)
        {
            List<int> inputList = new List<int>();
            var word = new StringBuilder();
            var words = data.Replace("\r\n", " ");

            foreach (Char c in words)
            {
                if (c == ' ')
                {
                    if (word.Length == 1)
                    {
                        string reducedNumber = String.Format("{0}{1}", word.ToString()[0], word.ToString()[0]);
                        inputList.Add(Int32.Parse(reducedNumber));
                        word = new StringBuilder();
                        continue;
                    }
                    else if (word.Length > 2)
                    {
                        string reducedNumber = String.Format("{0}{1}", word.ToString()[0], word[word.Length - 1]);
                        inputList.Add(Int32.Parse(reducedNumber));
                        word = new StringBuilder();
                        continue;
                    }
                    inputList.Add(Int32.Parse(word.ToString()));
                    word = new StringBuilder();
                    continue;
                }
                if (Char.IsDigit(c))
                {
                    word.Append(c);
                }
                
            }
            return inputList;
        }        
    }
}
