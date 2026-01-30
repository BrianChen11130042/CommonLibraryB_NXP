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

        public float InputVoltage { get; set; } // 單位:V

        public float InputFaultVoltage { get; set; } // 單位:V

        public float OutputVoltage { get; set; } // 單位:V

        public float OutputLoad { get; set; } // 單位:%

        public float InputFrequency { get; set; } // 單位:Hz

        public float BatteryVoltage { get; set; } // 單位:V

        public float Temperature { get; set; } // 單位: degrees of centigrade



        public int UtilityFail { get; set; } // 1:停電或牆壁電源電壓不穩  0:正常

        public int BatteryLow { get; set; } // 1:電池電量低  0:正常

        public int BypassBoostActive { get; set; } // 1:UPS正在使用旁路模式或升壓模式  0:正常

        public int UpsFault { get; set; } // 1:UPS故障  0:正常

        public int UpsType { get; set; } // 1:Standby(離線式)  0:On-line (在線式)

        public int TestInProgress { get; set; } // 1:正在進行自我檢查測試  0:不在測試狀態

        public int ShutdownActive { get; set; } // 1:UPS正在關機  0:沒有關機
    }
}
