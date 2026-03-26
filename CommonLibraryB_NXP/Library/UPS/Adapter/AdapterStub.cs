using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Adapter
{
    public partial class AdapterStub
    {

    }

    public partial class AdapterStub
    {
        const int devoceNo = 4;
    }

    public partial class AdapterStub : IUpsOperate<UpsPackage>
    {
        public async Task<bool> GetDeviceNo(UpsPackage t)
        {
            t.property.upsNo = devoceNo;

            return true;
        }

        public async Task<bool> GetTestUpsStatus(UpsPackage t)
        {
 
            t.property.OutputLoad = 0;
            t.property.BatteryVoltage = 0;
            t.property.Temperature = 0;
            t.property.ChargeInStatus = 0;
            t.property.RemainBatteryBackupTime = 0;

            t.property.UtilityFail = 0;
            t.property.BatteryLow = 0;
            t.property.BypassBoostActive = 0;
            t.property.UpsFailed = 0;
            t.property.UpsType = 0;
            t.property.TestInProcess = 0;
            t.property.ShutdownActive = 0;

            return true;
        }

        public async Task<bool> InitDevice(UpsPackage t)
        {

            return true;
        }

        public async Task<bool> GetUpsStatus(UpsPackage t)
        {
            t.property.OutputLoad = 0;
            t.property.BatteryVoltage = 0;
            t.property.Temperature = 0;
            t.property.ChargeInStatus = 0;
            t.property.RemainBatteryBackupTime = 0;

            t.property.UtilityFail = 0;
            t.property.BatteryLow = 0;
            t.property.BypassBoostActive = 0;
            t.property.UpsFailed = 0;
            t.property.UpsType = 0;
            t.property.TestInProcess = 0;
            t.property.ShutdownActive = 0;

            return true;
        }
    }
}
