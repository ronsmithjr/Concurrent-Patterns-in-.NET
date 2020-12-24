using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentModels.Models;
using ConcurrentFileOperations;
using System.Collections.Generic;

namespace ConcurrentCollections.UnitTest
{
    [TestClass]
    public class GeneralFileProcessingTest
    {
        string DirectoryName => @"C:\$Database\AdventureWorks-oltp-install-script";
        
        public GeneralFileProcessingTest()
        {
            
        }
        //This is a flaky test because this directory is accessable by anyone.
       // [TestMethod]
        //public void ErrorCountIsZero()
        //{
        //    OperationResult<long> expected = new OperationResult<long>()
        //    {
        //        ErrorMessages = new List<string>(),
        //        Messages = new List<string>(),
        //        Value = 1,
        //        Result = Result.Success
        //    };
        //    OperationResult<long> actual = new GeneralFileProcessing().GetDirectorySize(DirectoryName);
        //    Assert.AreEqual(expected.ErrorMessages.Count, actual.ErrorMessages.Count);
        //}
        //This is more of a visual test.  the main thing we wanted to test here was passing in a collection to our Operation Result. 
        // We also wanted to use a thread safe collection in our parallel method.  We can use the concurrent bag for operations in side of thread safe methods then convert it to a list after we are done with our parallel methods.
        //[TestMethod]
        //public void GetData()
        //{
        //    OperationResult<List<string>> expected = new OperationResult<List<string>>()
        //    {
        //        ErrorMessages = new List<string>(),
        //        Messages = new List<string>(),
        //        Value = new List<string>(),
        //        Result = Result.Success
        //    };

        //    OperationResult<List<string>> actual = new GeneralFileProcessing().GetDirectoryInfo(DirectoryName);
        //    Assert.AreEqual(expected.Result, actual.Result);
        //}
    }
}
