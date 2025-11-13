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
        enum ESetOperate
        {
            HeartBeat,

        }

        void getCmd(ESetOperate operate, PlcPackage t)
        {
            switch(operate)
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

    public partial class AdapterTMRobotArm : IPlcOperate<PlcPackage>
    {
        public async Task<bool> GetDeviceIsReset(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> GetDeviceName(PlcPackage t)
        {
            t.property.getPier.pierName = "Robot";
            return true;
        }

        public async Task<bool> GetRobotStatus(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> SetHeartBeat(PlcPackage t)
        {
            try
            {
                if(t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(ESetOperate.HeartBeat, t);
                await setMultiRegisterAsync(t);

                return true;
            }
            catch(Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }

        public async Task<bool> SetRobotMissionFinsih(PlcPackage t)
        {
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
    }

    public partial class AdapterTMRobotArm
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
