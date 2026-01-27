using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Manager.ModbusRtu
{
    public class ModbusRtuPack
    {
        public SerialPort port { get; set; }

        public byte[] cmd { get; set; }

        public byte[] rcmd { get; set; }

        public string cmdStr { get; set; }

        public string rcmdStr { get; set; }
    }
}
