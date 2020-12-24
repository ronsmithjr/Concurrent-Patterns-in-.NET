using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentModels.Models
{
    public class ObjectPool<T>
    {
        private ConcurrentBag<T> objects;
        private Func<T> objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            if(objectGenerator == null)
            {
                throw new ArgumentNullException("objectGenerator");
            }



            objects = new ConcurrentBag<T>();
            this.objectGenerator = objectGenerator;
        }
        public T GetObject()
        {
            T item;
            if(objects.TryTake(out item))
            {
                return item;
            }
            return objectGenerator();
        }

        public void PutObject(T item)
        {
            objects.Add(item);
        }

    }
}
