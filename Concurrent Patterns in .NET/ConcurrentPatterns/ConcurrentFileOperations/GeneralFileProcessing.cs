using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using ConcurrentModels.Models;
using ConcurrentFileOperations.ExtensionMethods;


namespace ConcurrentFileOperations
{

    //PLINQ Opperations have been shown to be 57% faster than Parallel operation where appropiate.
    public class GeneralFileProcessing
    {
        
        public OperationResult<long> GetDirectorySize(string DirName)
        {
            OperationResult<long> retVal = new OperationResult<long>()
            {
                ErrorMessages = new List<string>(),
                Messages = new List<string>()
            };
            try
            {
                List<FileInfo> files = new DirectoryInfo(DirName).GetFiles().ToList();                
                long totalSize = 0;
                
                Parallel.For(0, files.Count, (index, state) =>
                {
                    FileInfo file = new FileInfo(files[index].FullName);
                    Interlocked.Add(ref totalSize, file.Length);
                });
                retVal.Value = totalSize;
                return retVal;
            }
            catch(AggregateException e)
            {
                retVal.ErrorMessages.Add(e.Message + Environment.NewLine + e.StackTrace);
                return retVal;
            }
            catch (Exception e)
            {
                retVal.ErrorMessages.Add(e.Message + Environment.NewLine + e.StackTrace);
                return retVal;
            }

        }

        public OperationResult<List<string>> GetDirectoryInfo(string DirName)
        {
            ConcurrentBag<string> bag = new ConcurrentBag<string>();
            List<string> bagList = new List<string>();

            OperationResult<List<string>> retVal = new OperationResult<List<string>>()
            {
                Value = new List<string>(),
                Messages = new List<string>(),
                ErrorMessages = new List<string>()
            };

            try
            {
                List<FileInfo> files = new DirectoryInfo(DirName).GetFiles().ToList();
                Parallel.For(0, files.Count, (index, state) =>
                {
                    FileInfo file = new FileInfo(files[index].FullName);
                    StringBuilder sb = new StringBuilder();
                    
                    string line = string.Format("{0}{1}{2}", file.Name, file.Length.ToSize().PadLeft(70), Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(20));
                   
                    bag.Add(line);
                });


                retVal.Value = bag.ToList();
                retVal.Result = Result.Success;

                return retVal;
            }
            catch(AggregateException e)
            {
                retVal.ErrorMessages.Add(e.Message + Environment.NewLine + e.StackTrace);
                retVal.Result = Result.Failure;
                return retVal;
            }
            catch(Exception e)
            {
                retVal.ErrorMessages.Add(e.Message + Environment.NewLine + e.StackTrace);
                retVal.Result = Result.Failure;
                return retVal;
            }
           
        }
    }

  
}
