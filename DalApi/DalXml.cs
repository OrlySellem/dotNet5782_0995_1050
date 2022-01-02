using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    namespace DalXml //IDal
    {
        class DalXml
        {
            #region singelton
            static readonly DalXml instance = new DalXml();
            internal static DalXml Instance { get { return instance; } }
            static DalXml() { }
            public int getName() { return 1;}

            #endregion singelton


        }
    }
}

