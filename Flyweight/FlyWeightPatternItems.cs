namespace Flyweight
{
    using System.Collections.Generic;
    using System.Linq;

    public class Sentence
    {
        private string[] Words { get; set; }
        private Dictionary<int, WordToken> textCollection = new Dictionary<int, WordToken>();

        public Sentence(string text)
        {
            this.Words = text.Split(' ').ToArray();
        }

        //indexation
        public WordToken this[int index]
        {
            get
            {
                WordToken wt = new WordToken();
                textCollection.Add(index, wt);
                return textCollection[index];
            }
        }

        public override string ToString()
        {
            var capitalizedText = new List<string>();
            for (int i = 0; i < Words.Length; i++)
            {
                var word = Words[i];
                if (textCollection.Keys.Contains(i) && textCollection[i].IsCapitalized)
                {
                    word = word.ToUpperInvariant();
                }
                capitalizedText.Add(word);
            }

            return string.Join(" ", capitalizedText);
        }
    }

    public class WordToken
    {
        public bool IsCapitalized { get; set; }
    }
}
