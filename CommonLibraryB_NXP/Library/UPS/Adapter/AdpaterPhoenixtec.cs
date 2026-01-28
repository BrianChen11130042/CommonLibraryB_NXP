using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Adapter
{
    public partial class AdpaterPhoenixtec
    {
        enum EGetOperate
        {
            UpsStatus
        }

        void getCmd(EGetOperate operate, UpsPackage t)
        {
            switch(operate)
            {
                case EGetOperate.UpsStatus:
                    cmdUpsStatus(t);
                    break;
            }
        }

        void cmdUpsStatus(UpsPackage t)
        {
            t.cmdStr = "Q1\r";
        }
    }

    public partial class AdpaterPhoenixtec
    {
        void unpack(EGetOperate operate, UpsPackage t)
        {
            switch(operate)
            {
                case EGetOperate.UpsStatus:
                    upUpsStatus(t);
                    break;
            }
        }

        void upUpsStatus(UpsPackage t)
        {
            string[] parts = t.rcmdStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            t.property.InputVoltage = float.Parse(parts[0], CultureInfo.InvariantCulture); // 單位:V
            t.property.InputFaultVoltage = float.Parse(parts[1], CultureInfo.InvariantCulture); // 單位:V
            t.property.OutputVoltage = float.Parse(parts[2], CultureInfo.InvariantCulture); // 單位:V
            t.property.OutputLoad = float.Parse(parts[3], CultureInfo.InvariantCulture); // 單位:%
            t.property.InputFrequency = float.Parse(parts[4], CultureInfo.InvariantCulture); // 單位:Hz
            t.property.BatteryVoltage = float.Parse(parts[5], CultureInfo.InvariantCulture); // 單位:V
            t.property.Temperature = float.Parse(parts[6], CultureInfo.InvariantCulture); // 單位: degrees of centigrade

            string statusBit = parts[7];

            t.property.UtilityFail = statusBit[0]; // 1:停電或牆壁電源電壓不穩  0:正常
            t.property.BatteryLow = statusBit[1]; // 1:電池電量低  0:正常
            t.property.BypassBoostActive = statusBit[2]; // 1:UPS正在使用旁路模式或升壓模式  0:正常
            t.property.UpsFault = statusBit[3]; // 1:UPS故障  0:正常
            t.property.UpsType = statusBit[4]; // 1:Standby(離線式)  0:On-line (在線式)
            t.property.TestInProgress = statusBit[5]; // 1:正在進行自我檢查測試  0:不在測試狀態
            t.property.ShutdownActive = statusBit[6]; // 1:UPS正在關機  0:沒有關機
        }
    }

    public partial class AdpaterPhoenixtec : IUpsOperate<UpsPackage>
    {
        public async Task<bool> GetUpsStatus(UpsPackage t)
        {
            try
            {
                if (t.port == null)
                    setException("Modbus RTU Disconnect");

                if (!t.port.IsOpen)
                    t.port.Open();

                t.port.DiscardInBuffer();

                getCmd(EGetOperate.UpsStatus, t);

                t.port.Write(t.cmdStr);

                await Task.Delay(2000);

                t.rcmdStr = t.port.ReadTo("\r");

                t.rcmdStr = t.rcmdStr.TrimStart('(');
                string[] parts = t.rcmdStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if(parts.Length < 8 || parts[7].Length >= 8)
                    setException("UPS Response Error");

                unpack(EGetOperate.UpsStatus, t);

                return true;

            }
            catch (Exception ex)
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
