using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhTelekomTest.Model
{
    public class PrimaryKey<T>
    {
        public T Model { get; set; }

        public PrimaryKey(T entity)
        {
            this.Model = entity;
        }
    }
}
