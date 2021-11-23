using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerInParcel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public override string ToString()
            {
                return string.Format("id is:{0}\nName is:{1}\n", Id, Name);
            }

        }
    }
    
}
