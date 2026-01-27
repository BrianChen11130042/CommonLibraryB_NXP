using CommonLibraryB_NXP.Library.UPS.Adapter;
using CommonLibraryB_NXP.Library.UPS.Config;
using CommonLibraryB_NXP.Library.UPS.Property;
using CommonLibraryB_NXP.Manager.ModbusRtu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS
{
    public partial class UpsLibrary<T>
    {
        public ModbusRtuManager modbusRtuManager;
        public UpsConfigManager<T> configManager;
        public UpsPropertyManager<T> propertyManager;

        public UpsLibrary(ModbusRtuManager modbusRtuManager, UpsConfigManager<T> configManager, UpsPropertyManager<T> propertyManager)
        {
            this.modbusRtuManager = modbusRtuManager;
            this.configManager = configManager;
            this.propertyManager = propertyManager;
        }

        public Dictionary<T, UpsPackage> Packages;

        public void InitPackage()
        {
            if(Packages == null)
            {
                Packages = new Dictionary<T, UpsPackage>();

                foreach(T dev in Enum.GetValues(typeof(T)))
                {
                    Packages.Add(dev, new UpsPackage());
                }
            }

            foreach(T dev in Enum.GetValues(typeof(T)))
            {
                Packages[dev].config = configManager.table[dev.ToString()];
                Packages[dev].property = propertyManager.table[dev.ToString()];

                if(modbusRtuManager.table.ContainsKey(Packages[dev].config.com))
                {
                    Packages[dev].port = modbusRtuManager.table[Packages[dev].config.com].serialPort;
                }
            }
        }
    }

    public partial class UpsLibrary<T> : IUpsOperate<T>
    {
        UpsAdapter adapter;

        public void InitAdapter()
        {
            List<UpsConfig> cs = configManager.table.Values.ToList();
            adapter = new UpsAdapter(cs);
        }

        IUpsOperate<UpsPackage> SelectAdapter(T t)
        {
            string key = t.ToString();
            UpsConfig c = configManager.table[key];
            return adapter[c.supplier];
        }

        public async Task<bool> GetUpsStatus(T t)
        {
            return await SelectAdapter(t).GetUpsStatus(Packages[t]);
        }
    }
}
