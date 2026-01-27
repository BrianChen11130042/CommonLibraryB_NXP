using CommonLibraryB_NXP.Base.Manager;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Manager.ModbusRtu
{
    public class ModbusRtuManager : ManagerBase<string[], ModbusRtuConfig>
    {
        public string Directory;
        public const string fileName = "ModbusRtuConfig.json";

        public ModbusRtuManager(string dir) : base(dir + "MachineConfig\\" + fileName, SerialPort.GetPortNames())
        {
            Directory = dir;
        }

        public override void GenerateDefaultTable()
        {
            table = new Dictionary<string, ModbusRtuConfig>();

            foreach (string key in keys)
            {
                if (!table.ContainsKey(key))
                {
                    table.Add(key, new ModbusRtuConfig() { com = key });
                }
            }
        }

        public bool Connect(out string msg)
        {
            msg = string.Empty;

            foreach (string key in keys)
            {
                if (!table[key].OpenPort(out msg))
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
                table[key].ClosePort();
            }

            return true;
        }
    }
}
