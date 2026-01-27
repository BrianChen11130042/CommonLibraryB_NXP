using CommonLibraryB_NXP.Base.Manager;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Config
{
    public class UpsConfigManager<E> : ManagerBase<E, UpsConfig>
    {
        public string Directory { get; set; }
        public const string fileName = "UPSConfig.json";

        public UpsConfigManager(string dir) : base(dir + "MachineConfig\\" + fileName)
        {
            Directory = dir;
        }

        public override void GenerateDefaultTable()
        {
            table = new Dictionary<string, UpsConfig>();

            foreach(string key in keys)
            {
                if(!table.ContainsKey(key))
                {
                    table.Add(key, new UpsConfig() { device = key, com = "COM1"});
                }
            }
        }
    }
}
