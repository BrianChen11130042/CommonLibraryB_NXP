using CommonLibraryB_NXP.Library.UPS.Config;
using CommonLibraryB_NXP.Library.UPS.Property;
using CommonLibraryB_NXP.Manager.ModbusRtu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Adapter
{
    public class UpsPackage : ModbusRtuPack
    {
        public UpsConfig config { get; set; }

        public UpsProperty property { get; set; }

        public string errorLog { get; set; }

        public string informLog { get; set; }
    }
}
