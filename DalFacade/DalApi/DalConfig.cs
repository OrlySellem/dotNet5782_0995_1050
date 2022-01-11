using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
   public class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        /// <summary>
        /// Check 
        /// </summary>
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"..\..\xml\dal-config.xml"); //go to dal-config file and open in
            DalName = dalConfig.Element("dal").Value; //
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                           ).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}