using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB_NXP.Base.Manager;

namespace CommonLibraryB_NXP.Library.PLC.Property
{

    public class PlcPropertyManager<E> : ManagerBase<E, PlcProperty>
    {
        public string Directory;
        public const string fileName = "PlcProperty.json";

        public PlcPropertyManager(string dir):base(dir + "Property\\" + fileName)
        {
            Directory = dir;
        }

        public override void GenerateDefaultTable()
        {
            table = new Dictionary<string, PlcProperty>();

            foreach(string key in keys)
            {
                if(!table.ContainsKey(key))
                {
                    table.Add(key, new PlcProperty() { device = key });
                }
            }
        }
    }
}
