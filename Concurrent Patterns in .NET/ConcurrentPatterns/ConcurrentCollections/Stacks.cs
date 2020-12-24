using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConcurrentModels.Models;

namespace ConcurrentCollections
{

    /// <summary>
    /// 
    /// </summary>
    public class Stacks
    {
        //this is a bad idea.  we are going to let the library handle the number of threadl
        //int ThreadsToRun => Environment.ProcessorCount - 1;
        public async Task<OperationResult<bool>> IsRunStacksSuccessfulAsync(int items)
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();
            OperationResult<bool> retVal = new OperationResult<bool>()
            {
                Messages = new List<string>()
            };    

            Action pusher = () =>
            {
                for (int i = 0; i < items; i++)
                {
                    stack.Push(i);
                }
            };

            pusher();

            stack.Clear();

            Action pushAndPop = () =>
            {
                retVal.Messages.Add($"Task started on {Task.CurrentId}");
                int item;
                for(int i = 0; i < items; i++)
                {
                    stack.Push(i);
                }
                /*this is a note for when we convert this to file loading*/
                //we are going to want to pop full records out when there is an error and continue loading data.
                //we can pop the record out and send it back to the sender
                for(int i =0; i < items; i++)
                {
                    stack.TryPop(out item);
                }
                retVal.Messages.Add($"Task ended on {Task.CurrentId}");
            };

            
            ///We are going to spin up 1 minus the total number of threads we have on the machine.
            //This will be adjusted according to job intensity.
            var task = new Task[5];
            for(int i = 0; i < task.Length; i++)
            {
                task[i] = Task.Factory.StartNew(pushAndPop);
            }

            //we are going to wait for all task to finish before we do anything else
            await Task.WhenAll(task);

            if (!stack.IsEmpty)
            {
                retVal.Messages.Add("Did not take all items off of the stack");
                retVal.Result = Result.Failure ;
            }
            else
            {
                retVal.Result = Result.Success;
            }

            return retVal;
        }
    }
}
