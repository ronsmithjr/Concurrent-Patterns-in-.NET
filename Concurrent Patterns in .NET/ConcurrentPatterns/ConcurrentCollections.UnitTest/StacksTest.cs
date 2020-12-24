using ConcurrentModels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ConcurrentCollections.UnitTest
{
    [TestClass]
    public class StacksTest
    {
        [TestMethod]
        public async Task RunStacksTest()
        {
            OperationResult<bool> expected = new OperationResult<bool>()
            {
                Result = Result.Success
            };

            OperationResult<bool> result = new OperationResult<bool>();
            Stacks stacks = new Stacks();

            result = await stacks.IsRunStacksSuccessfulAsync(10000);

            Assert.AreEqual(expected.Result, Result.Success);
        }
    }
}
