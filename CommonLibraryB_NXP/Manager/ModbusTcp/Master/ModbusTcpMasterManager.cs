using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB.Base.Manager;

namespace CommonLibraryB.Manager.ModbusTcp.Master
{
    public class ModbusTcpMasterManager : ManagerBase<EModbusTcpMaster, ModbusTcpMasterConfig>
    {
        public string Directory;
        public const string fileName = "ModbusTcpMasterConfig.json";

        public ModbusTcpMasterManager(string dir) : base(dir + "Config\\" + fileName)
        {
            Directory = dir;
            Init();
        }

        public override void GenerateDefaultTable()
        {
            table = new Dictionary<string, ModbusTcpMasterConfig>();

            foreach(string key in keys)
            {
                if (!table.ContainsKey(key))
                {
                    table.Add(key, new ModbusTcpMasterConfig() { device = key});
                }
            }
        }

        void Init()
        {
            foreach(string key in keys)
            {
                table[key].Init();
            }
        }

        public bool Connect(out string msg)
        {
            msg = string.Empty;

            foreach (string key in keys)
            {
                if(!(table[key].Connect(out msg)))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Disconnect()
        {
            foreach (string key in keys)
            {
                if(!(table[key].Disconnect()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
