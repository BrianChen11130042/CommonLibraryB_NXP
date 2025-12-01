using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterPier2
    {
        const int deviceNo = 2;
    }

    public partial class AdapterPier2
    {
        enum EGetOperate
        {
            DeviceReady,
            PierStatus,
        }

        void getCmd(EGetOperate operate, PlcPackage t)
        {
            switch (operate)
            {
                case EGetOperate.DeviceReady:
                    cmdDeviceReady(t);
                    break;

                case EGetOperate.PierStatus:
                    cmdPierStatus(t);
                    break;
            }

            void cmdDeviceReady(PlcPackage t)
            {
                t.station = 1;
                t.startAddress = 1223;
                t.offset = 1;
            }

            void cmdPierStatus(PlcPackage t)
            {
                t.station = 1;
                t.startAddress = 1221;
                t.offset = 1;
            }
        }
    }

    public partial class AdapterPier2
    {
        void unpack(EGetOperate operate, PlcPackage t)
        {
            switch (operate)
            {
                case EGetOperate.DeviceReady:
                    upDeviceReady(t);
                    break;

                case EGetOperate.PierStatus:
                    upPierStatus(t);
                    break;
            }
        }

        void upDeviceReady(PlcPackage t)
        {
            t.property.getPier.isReady = t.rcmd;
        }

        void upPierStatus(PlcPackage t)
        {
            t.property.getPier.missionStatus = t.rcmd;
        }
    }

    public partial class AdapterPier2
    {
        enum ESetOperate
        {
            HeartBeat,
            MissionStart,
            MissionFinish
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

                case ESetOperate.MissionFinish:
                    cmdMissionFinish(t);
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
            t.startAddress = 1201;
            t.offset = 1;
        }

        void cmdMissionFinish(PlcPackage t)
        {
            ushort[] temp = new ushort[1];
            temp[0] = t.property.setPier.missionFinish;

            t.arrayCmd = temp;
            t.station = 1;
            t.startAddress = 1201;
            t.offset = 1;
        }
    }

    public partial class AdapterPier2 : IPlcOperate<PlcPackage>
    {
        public async Task<bool> GetDeviceNo(PlcPackage t)
        {
            t.property.getPier.pierNo = deviceNo;
            return true;
        }

        public async Task<bool> GetDeviceIsReady(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(EGetOperate.DeviceReady, t);
                await getSingleRegisterAsync(t);
                unpack(EGetOperate.DeviceReady, t);

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
                t.errorLog = ex.Message;
                return false;
            }
        }

        public async Task<bool> GetPierStatus(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(EGetOperate.PierStatus, t);
                await getSingleRegisterAsync(t);
                unpack(EGetOperate.PierStatus, t);

                return true;
            }
            catch (Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }

        public async Task<bool> SetPierMissionFinish(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(ESetOperate.MissionFinish, t);
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

    public partial class AdapterPier2
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

        async Task getSingleRegisterAsync(PlcPackage t)
        {
            t.rcmd = (await t.master.ReadHoldingRegistersAsync((byte)t.station, (ushort)t.startAddress, (ushort)t.offset)).FirstOrDefault();
        }
    }

    public partial class AdapterPier2
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
