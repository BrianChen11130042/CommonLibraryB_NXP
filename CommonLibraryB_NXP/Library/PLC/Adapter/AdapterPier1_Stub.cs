using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterPier1_Stub
    {
        const int deviceNo = 1;

        int missionAction { get; set; } = 0;

        Dictionary<int, ushort> dcFinish = new Dictionary<int, ushort>()
        {
            { 0, 0 },
            { 1, 5 },
            { 2, 15 },
            { 3, 25 },
            { 4, 35 },
        };
    }

    public partial class AdapterPier1_Stub : IPlcOperate<PlcPackage>
    {

        public async Task<bool> GetDeviceNo(PlcPackage t)
        {
            t.property.getPier.pierNo = deviceNo;
            return true;
        }

        public async Task<bool> GetDeviceIsReady(PlcPackage t)
        {
            t.property.getPier.isReady = 1;
            return true;
        }

        public async Task<bool> SetPierMissionStart(PlcPackage t)
        {
            missionAction = t.property.setPier.missionStart;
            return true;
        }

        public async Task<bool> GetPierStatus(PlcPackage t)
        {
            t.property.getPier.missionStatus = dcFinish[missionAction];

            return true;
        }

        public async Task<bool> SetPierMissionFinish(PlcPackage t)
        {
            return true;
        }
    }

    public partial class AdapterPier1_Stub
    {
        public async Task<bool> GetWarehouse(PlcPackage t)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetHeartBeat(PlcPackage t)
        {
            throw new NotImplementedException();
        }

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
