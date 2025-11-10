using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB_NXP.Manager.ModbusTcp.Master;
using CommonLibraryB_NXP.Library.PLC.Adapter;

namespace CommonLibraryB_NXP.Library.PLC.Config
{
    public class PlcConfig
    {
        public string device { get; set; }

        public EModbusTcpMaster master { get; set; } = EModbusTcpMaster.Master1;

        public EPlcSupplier supplier { get; set; } = EPlcSupplier.TM_Pier1;
    }
}
