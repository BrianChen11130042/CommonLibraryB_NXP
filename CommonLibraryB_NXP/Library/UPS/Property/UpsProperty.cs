using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Property
{

    public class UpsProperty
    {
        public string device { get; set; }

        public int upsNo { get; set; }



        public double OutputLoad { get; set; } // 單位:%

        public double BatteryVoltage { get; set; } // 單位:V

        public double Temperature { get; set; } // 單位: degrees of centigrade

        public double ChargeInStatus { get; set; } //單位:%

        public double RemainBatteryBackupTime { get; set; } //單位:min



        public int UtilityFail { get; set; } // 1:停電或牆壁電源電壓不穩  0:正常

        public int BatteryLow { get; set; } // 1:電池電量低  0:正常

        public int BypassBoostActive { get; set; } // 1:UPS正在使用旁路模式或升壓模式  0:正常

        public int UpsFailed { get; set; } // 1:UPS故障  0:正常

        public int UpsType { get; set; } // 1:Standby(離線式)  0:On-line (在線式)

        public int TestInProcess { get; set; } // 1:正在進行自我檢查測試  0:不在測試狀態

        public int ShutdownActive { get; set; } // 1:UPS正在關機  0:沒有關機
    }
}
