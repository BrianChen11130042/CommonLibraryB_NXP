using CommonLibraryB_NXP.Base.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Property
{
    public class UpsPropertyManager<E> : ManagerBase<E, UpsProperty>
    {
        public string Directory;
        public const string fileName = "UPSProperty.json";

        public UpsPropertyManager(string dir) : base(dir + "MachineProperty\\" + fileName)
        {
            Directory = dir;
        }

        public override void GenerateDefaultTable()
        {
            table = new Dictionary<string, UpsProperty>();

            foreach(string key in keys)
            {
                if(!table.ContainsKey(key))
                {
                    table.Add(key, new UpsProperty() { device = key });
                }
            }
        }
    }
}
