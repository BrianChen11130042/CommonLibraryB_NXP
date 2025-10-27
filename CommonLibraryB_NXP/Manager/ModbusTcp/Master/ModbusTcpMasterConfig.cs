using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NModbus;

namespace CommonLibraryB.Manager.ModbusTcp.Master
{
    public class ModbusTcpMasterConfig
    {
        [Required]
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)")]
        public string Ip { get; set; } = "127.0.0.1";

        [Required]
        [Range(0, 65535)]
        public int Port { get; set; } = 502;

        public string device { get; set; }

        public bool Enable { get; set; } = false;

        [JsonIgnore]
        public TcpClient tcpClient { get; set; }

        [JsonIgnore]
        public IModbusFactory modbusFactory { get; set; }

        [JsonIgnore]
        public IModbusMaster modbusTcpMaster { get; set; }

        public void Init()
        {
            tcpClient = new TcpClient();
            modbusFactory = new ModbusFactory();
        }

        public bool Connect(out string msg)
        {
            msg = string.Empty;

            if (!Enable)
                return true;

            try
            {
                if(tcpClient.Connected)
                {
                    tcpClient.Close();
                }

                tcpClient = new TcpClient();
                
                tcpClient.Connect(Ip, Port);

                modbusTcpMaster = modbusFactory.CreateMaster(tcpClient);

                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool Disconnect()
        {
            if (!Enable)
                return true;

            try
            {
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                    tcpClient.Dispose();
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
