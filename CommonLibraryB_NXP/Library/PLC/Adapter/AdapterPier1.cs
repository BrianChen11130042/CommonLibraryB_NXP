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
        enum ESetOperate
        {
            HeartBeat,
            MissionStart,
        }

        void getCmd(ESetOperate operate, PlcPackage t)
        {
            switch (operate)
            {
                case ESetOperate.HeartBeat:
                    cmdHeartBeat(t);
                    break;

                case ESetOperate.MissionStart:
                    cmdMissionStart(t);
                    break;
            }
        }

        void cmdHeartBeat(PlcPackage t)
        {
            ushort[] temp = new ushort[1];
            temp[0] = t.property.setPier.heartBeat;

            t.arrayCmd = temp;
            t.station = 1;
            t.startAddress = 1203;
            t.offset = 1;
        }

        void cmdMissionStart(PlcPackage t)
        {
            ushort[] temp = new ushort[1];
            temp[0] = t.property.setPier.missionStart;

            t.arrayCmd = temp;
            t.station = 1;
            t.startAddress = 1200;
            t.offset = 1;
        }
    }

    public partial class AdapterPier1 : IPlcOperate<PlcPackage>
    {
        public async Task<bool> GetDeviceName(PlcPackage t)
        {
            t.property.getPier.pierName = "Pier1";
            return true;
        }

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

        public async Task<bool> SetPierMissionStart(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(ESetOperate.MissionStart, t);
                await setMultiRegisterAsync(t);

                return true;
            }
            catch(Exception ex)
            {
                t.errorLog=ex.Message;
                return false;
            }
        }

        public async Task<bool> GetDeviceIsReset(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> GetPierStatus(PlcPackage t)
        {
            return true;
        }

        public async Task<bool> SetPierMissionFinish(PlcPackage t)
        {
            return true;
        }
    }

    public partial class AdapterPier1
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
