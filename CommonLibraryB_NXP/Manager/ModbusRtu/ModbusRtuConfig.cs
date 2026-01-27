using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Manager.ModbusRtu
{
    public class ModbusRtuConfig
    {
        public string com { get; set; } = "COM1";

        public int baudrate { get; set; } = 19200;

        public Parity parity { get; set; } = Parity.None;

        public int dataBits { get; set; } = 8;

        public StopBits stopBits { get; set; } = StopBits.One;

        public bool enable { get; set; } = false;

        [JsonIgnore]
        public SerialPort serialPort { get; set; }

        public bool OpenPort(out string msg)
        {
            msg = string.Empty;

            if (!enable)
                return true;

            try
            {
                if (serialPort != null)
                {
                    serialPort.Close();
                }

                serialPort = new SerialPort(com, baudrate, parity, dataBits, stopBits);

                serialPort.ReadTimeout = 5000;
                serialPort.WriteTimeout = 5000;

                serialPort.Open();

                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool ClosePort()
        {
            if (!enable)
                return true;

            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
