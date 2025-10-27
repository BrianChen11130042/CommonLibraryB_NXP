using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB.Manager.ModbusTcp.Master;
using CommonLibraryB_NXP.Library.PLC.Config;
using CommonLibraryB_NXP.Library.PLC.Property;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public class PlcPackage : ModbusTcpMasterPack
    {
        public PlcConfig config { get; set; }

        public PlcProperty property { get; set; }

        public string errorLog { get; set; }

        public string informLog { get; set; }
    }
}
