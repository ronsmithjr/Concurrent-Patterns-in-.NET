using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    /// <summary>
    /// The Parallel.Invoke(Action[] actions) method allows us to run an array of action delegates.  We can use these action delegates to run the same method over and over again  concurrently or we can run different methods concurrently.  (There is no garuntee that these methods will run concurrently.)  One example we have of this is the Queues
    /// </summary>
    public class ParallelInvoke
    {
        public ParallelInvokeDto RunTaskInParallel(string[] words, string term = "")
        {
            ParallelInvokeDto retVal = new ParallelInvokeDto();
            CountForWord wordCount = new CountForWord();
            Parallel.Invoke(
                    () => { retVal.CountForWord = GetCountForWord(words, term); },
                    () => { retVal.MostCommonWords =  GetMostCommonWords(words); },
                    () => { retVal.LongestWord = GetLongestWord(words); }
                );
            return retVal;
        }


        private CountForWord GetCountForWord(string [] words, string term)
        {
            var finalWord = from word in words
                            where word.ToUpper().Contains(term.ToUpper())
                            select word;
            CountForWord retVal = new CountForWord();
            retVal.Word = term;
            retVal.WordCount = finalWord.Count();
            return retVal;
        }

        private List<string> GetMostCommonWords(string [] words)
        {
            var frequencyOrder = from word in words
                                 where word.Length > 6
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;
            var commonWords = frequencyOrder.Take(10).ToList();
            return commonWords;
        }

        private string GetLongestWord(string[] words)
        {            
            var retVal = (from w in words
                               orderby w.Length descending
                               select w).FirstOrDefault();
            return retVal;
        }
    }

    public class ParallelInvokeDto
    {
        public string LongestWord { get; set; }
        public List<string> MostCommonWords { get; set; }
        public CountForWord CountForWord { get; set; }
    }
    public class CountForWord
    {
        public string Word { get; set; }
        public int WordCount { get; set; }
    }
}
