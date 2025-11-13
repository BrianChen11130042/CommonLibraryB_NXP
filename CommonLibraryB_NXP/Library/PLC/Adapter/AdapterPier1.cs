using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterPier1
    {
        const string name = "Pier1";
    }

    public partial class AdapterPier1
    {

    }

    public partial class AdapterPier1 : IPlcOperate<PlcPackage>
    {
        public async Task<bool> GetDeviceIsReset(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> GetDeviceName(PlcPackage t)
        {
            t.property.getPier.pierName = "Pier1";
            return true;
        }

        public async Task<bool> GetPierStatus(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> SetHeartBeat(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> SetPierMissionFinish(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> SetPierMissionStart(PlcPackage t)
        {
            return true;
        }
    }

    public partial class AdapterPier1
    {
        public Task<bool> GetRobotStatus(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetRobotMissionFinsih(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetRobotMissionInform(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetRobotMissionStart(PlcPackage t)
        {
            throw new NotImplementedException();
        }
    }
}
