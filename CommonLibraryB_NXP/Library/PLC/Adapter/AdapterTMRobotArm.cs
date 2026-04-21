using CommonLibraryB_NXP.Tools.TypeConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterTMRobotArm
    {
        const int deviceNo = 3;
    }

    public partial class AdapterTMRobotArm
    {
        enum EGetOperate
        {
            DeviceReady,
            DeviceError,
            RobotStatus,
        }

        void getCmd(EGetOperate operate, PlcPackage t)
        {
            switch(operate)
            {
                case EGetOperate.DeviceReady:
                    cmdDeviceReady(t);
                    break;

                case EGetOperate.DeviceError:
                    cmdDeviceError(t);
                    break;

                case EGetOperate.RobotStatus:
                    cmdRobotStatus(t);
                    break;
            }
        }

        void cmdDeviceReady(PlcPackage t)
        {
            t.station = 1;
            t.startAddress = 1223;
            t.offset = 1;
        }

        void cmdDeviceError(PlcPackage t)
        {
            t.station = 1;
            t.startAddress = 1226;
            t.offset = 1;
        }

        void cmdRobotStatus(PlcPackage t)
        {
            t.station = 1;
            t.startAddress = 1222;
            t.offset = 1;
        }
    }

    public partial class AdapterTMRobotArm
    {
        void unpack(EGetOperate operate, PlcPackage t)
        {
            switch(operate)
            {
                case EGetOperate.DeviceReady:
                    upDeviceReady(t);
                    break;

                case EGetOperate.DeviceError:
                    upDeviceError(t);
                    break;

                case EGetOperate.RobotStatus:
                    upRobotStatus(t);
                    break;
            }
        }

        void upDeviceReady(PlcPackage t)
        {
            t.property.getRobot.isReady = t.rcmd;
        }

        void upDeviceError(PlcPackage t)
        {
            t.property.getRobot.errorCode = t.rcmd;
        }

        void upRobotStatus(PlcPackage t)
        {
            t.property.getRobot.missionStatus = t.rcmd;
        }
    }

    public partial class AdapterTMRobotArm
    {
        enum ESetOperate
        {
            MissionInform,
            MissionStart,
            MissionFinish
        }

        void getCmd(ESetOperate operate, PlcPackage t)
        {
            switch(operate)
            {
                case ESetOperate.MissionInform:
                    cmdMissionInform(t);
                    break;

                case ESetOperate.MissionStart:
                    cmdMissionStart(t);
                    break;

                case ESetOperate.MissionFinish:
                    cmdMissionFinish(t);
                    break;
            }
        }

        void cmdMissionInform(PlcPackage t)
        {
            List<ushort> temp = new List<ushort>();

            temp.Add(t.property.setRobot.boardSize);
            temp.Add(t.property.setRobot.pickZone);
            temp.Add(t.property.setRobot.pickLayer);
            temp.Add(t.property.setRobot.dropZone);
            temp.Add(t.property.setRobot.dropLayer);

            ushort[] tempBarcode;

            StringUshortConverter.StringToUshortArray(t.property.setRobot.barcode,
                                                      EEndian.BigEndian,
                                                      10,
                                                      out tempBarcode);

            temp.AddRange(tempBarcode);

            t.arrayCmd = temp.ToArray();
            t.station = 1;
            t.startAddress = 1205;
            t.offset = 15;
        }

        void cmdMissionStart(PlcPackage t)
        {
            ushort[] temp = new ushort[1];
            temp[0] = t.property.setRobot.missionStart;

            t.arrayCmd = temp;
            t.station = 1;
            t.startAddress = 1202;
            t.offset = 1;
        }

        void cmdMissionFinish(PlcPackage t)
        {
            ushort[] temp = new ushort[1];
            temp[0] = t.property.setRobot.missionFinish;

            t.arrayCmd = temp;
            t.station = 1;
            t.startAddress = 1202;
            t.offset = 1;
        }
    }

    public partial class AdapterTMRobotArm : IPlcOperate<PlcPackage>
    {
        public async Task<bool> GetDeviceNo(PlcPackage t)
        {
            t.property.getRobot.robotNo = deviceNo;
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

        public async Task<bool> GetDeviceIsError(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(EGetOperate.DeviceError, t);
                await getSingleRegisterAsync(t);
                unpack(EGetOperate.DeviceError, t);

                return true;
            }
            catch (Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }

        public async Task<bool> SetRobotMissionInform(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(ESetOperate.MissionInform, t);
                await setMultiRegisterAsync(t);

                return true;
            }
            catch(Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }

        public async Task<bool> SetRobotMissionStart(PlcPackage t)
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

        public async Task<bool> GetRobotStatus(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(EGetOperate.RobotStatus, t);
                await getSingleRegisterAsync(t);
                unpack(EGetOperate.RobotStatus, t);

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
            catch(Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
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

        async Task getSingleRegisterAsync(PlcPackage t)
        {
            t.rcmd = (await t.master.ReadHoldingRegistersAsync((byte)t.station, (ushort)t.startAddress, (ushort)t.offset)).FirstOrDefault();
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
