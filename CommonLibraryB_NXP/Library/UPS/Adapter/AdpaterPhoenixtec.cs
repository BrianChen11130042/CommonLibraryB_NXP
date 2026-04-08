using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Adapter
{
    public partial class AdpaterPhoenixtec
    {
        const int devoceNo = 4;
    }

    public partial class AdpaterPhoenixtec
    {
        enum EGetOperate
        {
            UpsInit,
            UpsTestStatus,
            UpsMeasureData,
            UpsErdData,
            UpsStateData,
        }

        void getCmd(EGetOperate operate, UpsPackage t)
        {
            switch(operate)
            {
                case EGetOperate.UpsInit:
                    cmdUpsInit(t);
                    break;

                case EGetOperate.UpsTestStatus:
                    cmdUpsTestStatus(t);
                    break;

                case EGetOperate.UpsMeasureData:
                    cmdUpsMeasureData(t);
                    break;

                case EGetOperate.UpsErdData:
                    cmdUpsErdData(t);
                    break;

                case EGetOperate.UpsStateData:
                    cmdUpsStateData(t);
                    break;
            }
        }

        void cmdUpsInit(UpsPackage t)
        {
            t.cmdStr = "TIME1,01\r";

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(t.cmdStr);
        }

        void cmdUpsTestStatus(UpsPackage t)
        {
            t.cmdStr = "Q1,01\r";
        }

        void cmdUpsMeasureData(UpsPackage t)
        {
            byte[] request = new byte[] { 0x01, 0x03, 0x00, 0x60, 0x00, 0x2D };

            byte[] crc = _calculateCrc(request); // CRC 演算法

            t.cmd = new byte[request.Length + 2];

            Array.Copy(request, t.cmd, request.Length);

            t.cmd[t.cmd.Length - 2] = crc[0]; // CRC Low
            t.cmd[t.cmd.Length - 1] = crc[1]; // CRC High
        }

        void cmdUpsErdData(UpsPackage t)
        {
            byte[] request = new byte[] { 0x01, 0x03, 0x00, 0xE0, 0x00, 0x1A };

            byte[] crc = _calculateCrc(request); // CRC 演算法

            t.cmd = new byte[request.Length + 2];

            Array.Copy(request, t.cmd, request.Length);

            t.cmd[t.cmd.Length - 2] = crc[0]; // CRC Low
            t.cmd[t.cmd.Length - 1] = crc[1]; // CRC High
        }

        void cmdUpsStateData(UpsPackage t)
        {
            byte[] request = new byte[] { 0x01, 0x03, 0x00, 0x30, 0x00, 0x01 };

            byte[] crc = _calculateCrc(request); // CRC 演算法

            t.cmd = new byte[request.Length + 2];

            Array.Copy(request, t.cmd, request.Length);

            t.cmd[t.cmd.Length - 2] = crc[0]; // CRC Low
            t.cmd[t.cmd.Length - 1] = crc[1]; // CRC High
        }

        private byte[] _calculateCrc(byte[] data)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < data.Length; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                        crc >>= 1;
                }
            }
            return new byte[] { (byte)(crc & 0xFF), (byte)((crc >> 8) & 0xFF) };
        }
    }

    public partial class AdpaterPhoenixtec
    {
        void unpack(EGetOperate operate, UpsPackage t)
        {
            switch(operate)
            {
                case EGetOperate.UpsTestStatus:
                    upUpsTestStatus(t);
                    break;

                case EGetOperate.UpsMeasureData:
                    upUpsMeasureData(t);
                    break;

                case EGetOperate.UpsErdData:
                    upUpsErdData(t);
                    break;  

                case EGetOperate.UpsStateData:
                    upUpsStateData(t);
                    break;
            }
        }

        void upUpsTestStatus(UpsPackage t)
        {
            //string[] parts = t.rcmdStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //t.property.InputVoltage = float.Parse(parts[1], CultureInfo.InvariantCulture); // 單位:V
            //t.property.InputFaultVoltage = float.Parse(parts[2], CultureInfo.InvariantCulture); // 單位:V
            //t.property.OutputVoltage = float.Parse(parts[3], CultureInfo.InvariantCulture); // 單位:V
            //t.property.OutputLoad = float.Parse(parts[4], CultureInfo.InvariantCulture); // 單位:%
            //t.property.InputFrequency = float.Parse(parts[5], CultureInfo.InvariantCulture); // 單位:Hz
            //t.property.BatteryVoltage = float.Parse(parts[6], CultureInfo.InvariantCulture); // 單位:V
            //t.property.Temperature = float.Parse(parts[7], CultureInfo.InvariantCulture); // 單位: degrees of centigrade

            //string statusBit = parts[8];

            //t.property.UtilityFail = statusBit[0]; // 1:停電或牆壁電源電壓不穩  0:正常
            //t.property.BatteryLow = statusBit[1]; // 1:電池電量低  0:正常
            //t.property.BypassBoostActive = statusBit[2]; // 1:UPS正在使用旁路模式或升壓模式  0:正常
            //t.property.UpsFault = statusBit[3]; // 1:UPS故障  0:正常
            //t.property.UpsType = statusBit[4]; // 1:Standby(離線式)  0:On-line (在線式)
            //t.property.TestInProgress = statusBit[5]; // 1:正在進行自我檢查測試  0:不在測試狀態
            //t.property.ShutdownActive = statusBit[6]; // 1:UPS正在關機  0:沒有關機
        }

        void upUpsMeasureData(UpsPackage t)
        {
            for (int i = 0; i < 45; i++)
            {
                int currentAddr = 0x60 + i;
                int offset = 3 + (i * 2);

                // 組合 High Byte 和 Low Byte
                short rawValue = (short)((t.rcmd[offset] << 8) | t.rcmd[offset + 1]);

                // 4. 套用換算公式 (同先前逻辑)
                if(dcUpsMeasureData.ContainsKey(currentAddr))
                {
                    dcUpsMeasureData[currentAddr] = _convertByMeasureAddress(currentAddr, rawValue);
                }
                else
                {
                    dcUpsMeasureData.Add(currentAddr, _convertByMeasureAddress(currentAddr, rawValue));
                }
            }

            t.property.OutputLoad = dcUpsMeasureData[0x7A];
            t.property.BatteryVoltage = dcUpsMeasureData[0x7C];
            t.property.Temperature = dcUpsMeasureData[0x7D];
            t.property.ChargeInStatus = dcUpsMeasureData[0x7E];
            t.property.RemainBatteryBackupTime = dcUpsMeasureData[0x80];
        }

        Dictionary<int, double> dcUpsMeasureData = new Dictionary<int, double>();

        double _convertByMeasureAddress(int addr, short raw)
        {
            if (addr == 0x7B || addr == 0x81 || addr == 0x83 || addr == 0x85) return raw / 100.0; // 電池單節電壓 #.##

            // 不需要除法的位址
            int[] noDiv = { 0x6C, 0x6D, 0x6E, 0x7A, 0x7E, 0x7F, 0x80, 0x82, 0x84 };
            foreach (int a in noDiv) if (a == addr) return raw;

            return raw / 10.0; // 大部分數據 ###.#
        }

        void upUpsErdData(UpsPackage t)
        {
            for (int i = 0 ; i < 13 ; i++)
            {
                int currentAddr = 0xE0 + i;
                int offset = 3 + (i * 2);

                // 組合 High Byte 和 Low Byte
                short rawValue = (short)((t.rcmd[offset] << 8) | t.rcmd[offset + 1]);

                if(dcUpsErdData.ContainsKey(currentAddr))
                {
                    dcUpsErdData[currentAddr] = _convertByErdAddress(currentAddr, rawValue);
                }
                else
                {
                    dcUpsErdData.Add(currentAddr, _convertByErdAddress(currentAddr, rawValue));
                }
            }

            for (int i = 16 ; i < 26 ; i++)
            {
                int currentAddr = 0xE0 + i;
                int offset = 3 + (i * 2);

                // 組合 High Byte 和 Low Byte
                short rawValue = (short)((t.rcmd[offset] << 8) | t.rcmd[offset + 1]);

                if (dcUpsErdData.ContainsKey(currentAddr))
                {
                    dcUpsErdData[currentAddr] = _convertByErdAddress(currentAddr, rawValue);
                }
                else
                {
                    dcUpsErdData.Add(currentAddr, _convertByErdAddress(currentAddr, rawValue));
                }
            }
        }

        Dictionary<int, double> dcUpsErdData = new Dictionary<int, double>();

        double _convertByErdAddress(int addr, short raw)
        {
            switch(addr)
            {
                case 0xE4:
                case 0xEB:
                case 0xE9:
                    return raw / 10.0;

                case 0xE6:
                case 0xE8:
                case 0xF2:
                    return raw / 1000.0;

                default:
                    return (double)raw;
            }
        }



        void upUpsStateData(UpsPackage t)
        {
            byte upsStatusByte = t.rcmd[4];   // WORD 0 Low
            byte testStatusByte = t.rcmd[3];  // WORD 0 High

            int s00 = (upsStatusByte >> 0) & 1; // Reserved (always 0) 
            t.property.ShutdownActive = (upsStatusByte >> 1) & 1; // 1：Shutdown active
            t.property.TestInProcess = (upsStatusByte >> 2) & 1; // 1：Test in process
            t.property.UpsType = (upsStatusByte >> 3) & 1; // 1：UPS type is standby , 0：UPS type is on line 
            t.property.UpsFailed = (upsStatusByte >> 4) & 1; // 1：UPS failed 
            t.property.BypassBoostActive = (upsStatusByte >> 5) & 1; // 1：Bypass/Boost active 
            t.property.BatteryLow = (upsStatusByte >> 6) & 1; // 1：Battery low 
            t.property.UtilityFail = (upsStatusByte >> 7) & 1; // 1：Utility fail (immediate)

            int s08 = (testStatusByte >> 0) & 1; // 1：Idle
            int s09 = (testStatusByte >> 1) & 1; // 1：Processing
            int s10 = (testStatusByte >> 2) & 1; // 1：Result：no failure
            int s11 = (testStatusByte >> 3) & 1; // 1：Result：failue/warning
            int s12 = (testStatusByte >> 4) & 1; // 1：Not possible or inhibit
            int s13 = (testStatusByte >> 5) & 1; // 1：Test cancel 
            int s14 = (testStatusByte >> 6) & 1; // 1：Reserved 
            int s15 = (testStatusByte >> 7) & 1; // 1：Other values 
        }
    }

    public partial class AdpaterPhoenixtec : IUpsOperate<UpsPackage>
    {
        public async Task<bool> GetDeviceNo(UpsPackage t)
        {
            t.property.upsNo = devoceNo;
            return true;
        }

        public async Task<bool> InitDevice(UpsPackage t)
        {
            try
            {
                if (t.port == null)
                    setException("Modbus RTU Disconnect");

                if (!t.port.IsOpen)
                    t.port.Open();

                getCmd(EGetOperate.UpsInit, t);

                t.port.Write(t.cmdStr);

                await Task.Delay(100);

                t.rcmdStr = t.port.ReadTo("\r");

                return true;
            }
            catch (Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }

        public async Task<bool> GetTestUpsStatus(UpsPackage t)
        {
            try
            {
                if (t.port == null)
                    setException("Modbus RTU Disconnect");

                if (!t.port.IsOpen)
                    t.port.Open();

                getCmd(EGetOperate.UpsTestStatus, t);

                t.port.Write(t.cmdStr);

                await Task.Delay(2000);

                t.rcmdStr = t.port.ReadTo("\r");

                t.rcmdStr = t.rcmdStr.TrimStart('(');
                string[] parts = t.rcmdStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if(parts.Length != 9 || parts[8].Length != 8)
                    setException("UPS Response Error");

                unpack(EGetOperate.UpsTestStatus, t);

                return true;

            }
            catch (Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }

        public async Task<bool> GetUpsStatus(UpsPackage t)
        {
            try
            {
                if (t.port == null)
                    setException("Modbus RTU Disconnect");

                if (!t.port.IsOpen)
                    t.port.Open();

                //getCmd(EGetOperate.UpsErdData, t);
                //t.port.Write(t.cmd, 0, t.cmd.Length);

                //await Task.Delay(1000);

                //t.rcmd = new byte[5 + 26 * 2];
                //t.port.Read(t.rcmd, 0, t.rcmd.Length);

                //if (t.rcmd[1] != 0x03 || t.rcmd[2] != 0X34)
                //    setException("UPS Response Error");

                //unpack(EGetOperate.UpsErdData, t);

                //////////////////////////////////////////////

                getCmd(EGetOperate.UpsMeasureData, t);
                t.port.Write(t.cmd, 0, t.cmd.Length);

                await Task.Delay(1000);

                t.rcmd = new byte[5 + 45 * 2];
                t.port.Read(t.rcmd, 0, t.rcmd.Length);

                if (t.rcmd[1] != 0x03 || t.rcmd[2] != 0X5A)
                    setException("UPS Response Error");

                unpack(EGetOperate.UpsMeasureData, t);

                await Task.Delay(100);

                getCmd(EGetOperate.UpsStateData, t);
                t.port.Write(t.cmd, 0, t.cmd.Length);

                await Task.Delay(1000);

                t.rcmd = new byte[5 + 1 * 2 + 100];
                t.port.Read(t.rcmd, 0, t.rcmd.Length);

                if (t.rcmd[1] != 0x03 || t.rcmd[2] != 0x02)
                    setException("UPS Response Error");

                unpack(EGetOperate.UpsStateData, t);


                return true;

            }
            catch(Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }
    }

    public partial class AdpaterPhoenixtec
    {
        void setException(string msg)
        {
            throw new InvalidOperationException(msg);
        }
    }
}
