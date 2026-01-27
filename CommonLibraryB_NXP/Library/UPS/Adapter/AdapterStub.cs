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

    }

    public partial class AdapterStub : IUpsOperate<UpsPackage>
    {
        public async Task<bool> GetUpsStatus(UpsPackage t)
        {
            t.property.InputVoltage = 0;
            t.property.InputFaultVoltage = 0;
            t.property.OutputVoltage = 0;
            t.property.OutputLoad = 0;
            t.property.InputFrequency = 0;
            t.property.BatteryVoltage = 0;
            t.property.Temperature = 0;

            t.property.UtilityFail = 1;
            t.property.BatteryLow = 1;
            t.property.BypassBoostActive = 1;
            t.property.UpsFault = 1;
            t.property.UpsType = 1;
            t.property.TestInProgress = 1;
            t.property.ShutdownActive = 1;

            return true;
        }
    }
}
