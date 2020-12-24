using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;

namespace ConcurrentCollections.UnitTest
{
    [TestClass]
    public class ParallelInvokeTest
    {

        string[] Words => CreateWordArray("http://www.gutenberg.org/files/54700/54700-0.txt");
        ParallelInvokeDto expected = new ParallelInvokeDto()
        {
            LongestWord = "incomprehensible",
            MostCommonWords = new List<string>()
            {
                  "Oblomov",
                  "himself",
                  "Schtoltz",
                  "Gutenberg",
                  "Project",
                  "another",
                  "thought",
                  "Oblomov's",
                  "nothing",
                  "replied"
            },
            CountForWord = new CountForWord
            {
                Word = "sleep",
                WordCount = 57
            }
        };



        [TestMethod]
        public void RunTaskInParallelTest()
        {
            ParallelInvokeDto actual = new ParallelInvokeDto();
            ParallelInvoke runApp = new ParallelInvoke();
            actual = runApp.RunTaskInParallel(Words, "sleep");

            Assert.AreEqual(expected.LongestWord, actual.LongestWord);
            Assert.AreEqual(expected.MostCommonWords.Count, actual.MostCommonWords.Count);
            Assert.AreEqual(expected.CountForWord.Word, actual.CountForWord.Word);
            Assert.AreEqual(expected.CountForWord.WordCount, actual.CountForWord.WordCount);
        }
       


        private string[] CreateWordArray(string uri)
        {
            string s = new WebClient().DownloadString(uri);
            return s.Split(new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '_', '/' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
