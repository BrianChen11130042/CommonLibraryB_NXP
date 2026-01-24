using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterHeartbeat
    {
        enum ESetOperate
        {
            HeartBeat,
        }

        void getCmd(ESetOperate operate, PlcPackage t)
        {
            switch (operate)
            {
                case ESetOperate.HeartBeat:
                    cmdHeartBeat(t);
                    break;
            }
        }

        void cmdHeartBeat(PlcPackage t)
        {
            ushort[] temp = new ushort[1];
            temp[0] = t.property.setRobot.heartBeat;

            t.arrayCmd = temp;
            t.station = 1;
            t.startAddress = 1203;
            t.offset = 1;
        }
    }

    public partial class AdapterHeartbeat : IPlcOperate<PlcPackage>
    {
        public async Task<bool> SetHeartBeat(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(ESetOperate.HeartBeat, t);
                await setMultiRegisterAsync(t);

                return true;
            }
            catch (Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }
    }

    public partial class AdapterHeartbeat
    {
        void setModbusTcpError()
        {
            throw new InvalidOperationException("Modbus Tcp Disconnect");
        }

        async Task setMultiRegisterAsync(PlcPackage t)
        {
            //write multi register
            await t.master.WriteMultipleRegistersAsync((byte)t.station, (ushort)t.startAddress, t.arrayCmd);
        }
    }

    public partial class AdapterHeartbeat
    {
        public Task<bool> GetDeviceIsReady(PlcPackage t)
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
