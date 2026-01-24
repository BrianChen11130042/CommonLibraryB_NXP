using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterTMRobotArm_Stub
    {
        const int deviceNo = 3;
    }

    public partial class AdapterTMRobotArm_Stub : IPlcOperate<PlcPackage>
    {
        public async Task<bool> GetDeviceNo(PlcPackage t)
        {
            t.property.getRobot.robotNo = deviceNo;
            return true;
        }

        public async Task<bool> GetDeviceIsReady(PlcPackage t)
        {
            t.property.getRobot.isReady = 1;
            return true;
        }

        public async Task<bool> SetRobotMissionInform(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> SetRobotMissionStart(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> GetRobotStatus(PlcPackage t)
        {
            t.property.getRobot.missionStatus = 20;
            return true;
        }

        public async Task<bool> SetRobotMissionFinsih(PlcPackage t)
        {
            return true;
        }
    }

    public partial class AdapterTMRobotArm_Stub
    {
        public Task<bool> GetPierStatus(PlcPackage t)
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

        public Task<bool> GetWarehouse(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetHeartBeat(PlcPackage t)
        {
            throw new NotImplementedException();
        }
    }
}
