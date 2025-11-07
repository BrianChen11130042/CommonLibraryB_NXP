using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterTMRobotArm
    {
        const string name = "TMRobot";
    }

    public partial class AdapterTMRobotArm
    {

    }

    public partial class AdapterTMRobotArm : IPlcOperate<PlcPackage>
    {
        public Task<bool> GetDeviceIsReset(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetDeviceName(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetRobotStatus(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetHeartBeat(PlcPackage t)
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

    public partial class AdapterTMRobotArm
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
    }
}
