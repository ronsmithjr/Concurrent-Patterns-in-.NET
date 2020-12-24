using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConcurrentCollections;

namespace ConcurrentCollections.UnitTest
{
    [TestClass]
    public class QueuesTest
    {
        //When the parameter value is 10000 then the expected vlaue will be 49995000.  This is a passing test.
        [TestMethod]
        public void GetSumOfIntegersInQueue()
        {
            try
            {
                int expected = 49995000;
                int actual = new Queues().GetSumOfIntegersInQueue(10000);
                Assert.AreEqual(expected, actual);
            }
            catch(AggregateException e)
            {
                Assert.Fail(e.Message + e.StackTrace);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message + e.StackTrace);
            }
            
        }
    }
}
