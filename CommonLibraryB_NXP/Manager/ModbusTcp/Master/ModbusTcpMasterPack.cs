using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NModbus;

namespace CommonLibraryB.Manager.ModbusTcp.Master
{

    public class ModbusTcpMasterPack
    {
        public IModbusMaster master { get; set; }

        public ushort cmd { get; set; }

        public ushort rcmd { get; set; }

        public ushort[] arrayCmd { get; set; }

        public ushort[] arrayRcmd { get; set; }

        public bool boolCmd { get; set; }

        public bool boolRcmd { get; set; }

        public bool[] arrayBoolCmd { get; set; }

        public bool[] arrayBoolRcmd { get; set; }

        public int station { get; set; }

        public int startAddress { get; set; }

        public int offset { get; set; }
    }
}
