using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterHeartbeat_Stub : IPlcOperate<PlcPackage>
    {
        public async Task<bool> SetHeartBeat(PlcPackage t)
        {
            return true;
        }
    }

    public partial class AdapterHeartbeat_Stub
    {
        public Task<bool> GetDeviceIsReady(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetDeviceIsError(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetDeviceNo(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPierStatus(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetRobotStatus(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetWarehouse(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetPierMissionFinish(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetPierMissionStart(PlcPackage t)
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
