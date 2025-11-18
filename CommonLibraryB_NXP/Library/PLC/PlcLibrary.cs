using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB_NXP.Manager.ModbusTcp.Master;
using CommonLibraryB_NXP.Library.PLC.Adapter;
using CommonLibraryB_NXP.Library.PLC.Config;
using CommonLibraryB_NXP.Library.PLC.Property;

namespace CommonLibraryB_NXP.Library.PLC
{

    public partial class PlcLibrary<T>
    {
        public ModbusTcpMasterManager modbusTcpManager;
        public PlcConfigManager<T> configManager;
        public PlcPropertyManager<T> propertyManager;

        public PlcLibrary(ModbusTcpMasterManager modbusTcpManager, PlcConfigManager<T> configManager, 
                          PlcPropertyManager<T> propertyManager)
        {
            this.modbusTcpManager = modbusTcpManager;
            this.configManager = configManager;
            this.propertyManager = propertyManager;
        }

        public Dictionary<T, PlcPackage> Packages;

        public void InitPackage()
        {
            if(Packages == null)
            {
                Packages = new Dictionary<T, PlcPackage>();

                foreach(T dev in Enum.GetValues(typeof(T)))
                {
                    Packages.Add(dev, new PlcPackage());
                }
            }

            foreach(T dev in Enum.GetValues(typeof(T)))
            {
                Packages[dev].config = configManager.table[dev.ToString()];
                Packages[dev].property = propertyManager.table[dev.ToString()];

                string master = configManager.table[dev.ToString()].master.ToString();
                Packages[dev].master = modbusTcpManager.table[master].modbusTcpMaster;
            }
        }
    }

    public partial class PlcLibrary<T> : IPlcOperate<T>
    {
        PlcAdapter adapter;

        public void InitAdapter()
        {
            List<PlcConfig> cs = configManager.table.Values.ToList();
            adapter = new PlcAdapter(cs);
        }

        IPlcOperate<PlcPackage> SelectAdapter(T t)
        {
            string key = t.ToString();
            PlcConfig c = configManager.table[key];
            return adapter[c.supplier];
        }

        public async Task<bool> GetDeviceNo(T t)
        {
            return await SelectAdapter(t).GetDeviceNo(Packages[t]);
        }

        public async Task<bool> SetPierMissionStart(T t)
        {
            return await SelectAdapter(t).SetPierMissionStart(Packages[t]);
        }

        public async Task<bool> GetPierStatus(T t)
        {
            return await SelectAdapter(t).GetPierStatus(Packages[t]);
        }

        public async Task<bool> SetPierMissionFinish(T t)
        {
            return await SelectAdapter(t).SetPierMissionFinish(Packages[t]);
        }

        public async Task<bool> GetRobotStatus(T t)
        {
            return await SelectAdapter(t).GetRobotStatus(Packages[t]);
        }

        public async Task<bool> SetRobotMissionFinsih(T t)
        {
            return await SelectAdapter(t).SetRobotMissionFinsih(Packages[t]);
        }

        public async Task<bool> SetRobotMissionInform(T t)
        {
            return await SelectAdapter(t).SetRobotMissionInform(Packages[t]);
        }

        public async Task<bool> SetRobotMissionStart(T t)
        {
            return await SelectAdapter(t).SetRobotMissionStart(Packages[t]);
        }

        public async Task<bool> GetDeviceIsReady(T t)
        {
            return await SelectAdapter(t).GetDeviceIsReady(Packages[t]);
        }

        public async Task<bool> SetHeartBeat(T t)
        {
            return await SelectAdapter(t).SetHeartBeat(Packages[t]);
        }
    }
}
