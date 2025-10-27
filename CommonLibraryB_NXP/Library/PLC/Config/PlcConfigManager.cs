using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB.Base.Manager;

namespace CommonLibraryB_NXP.Library.PLC.Config
{

    public class PlcConfigManager<E> : ManagerBase<E, PlcConfig>
    {
        public string Directory { get; set; }
        public const string fileName = "PlcConfig.json";

        public PlcConfigManager(string dir) : base(dir + "Config\\" + fileName)
        {
            Directory = dir;
        }

        public override void GenerateDefaultTable()
        {
            table = new Dictionary<string, PlcConfig>();

            foreach(string key in keys)
            {
                if(!table.ContainsKey(key))
                {
                    table.Add(key, new PlcConfig() { device = key });
                }
            }
        }
    }
}
