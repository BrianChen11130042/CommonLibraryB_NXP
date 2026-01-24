using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public partial class AdapterWarehouse
    {
        enum EGetOperate
        {
            Warehouse
        }

        void getCmd(EGetOperate operate, PlcPackage t)
        {
            switch (operate)
            {
                case EGetOperate.Warehouse:
                    cmdWarehouse(t);
                    break;
            }
        }

        void cmdWarehouse(PlcPackage t)
        {
            t.station = 1;
            t.startAddress = 36;
            t.offset = 458;
        }
    }

    public partial class AdapterWarehouse
    {
        void unpack(EGetOperate operate, PlcPackage t)
        {
            switch (operate)
            {
                case EGetOperate.Warehouse:
                    upWarehouse(t);
                    break;
            }
        }

        void upWarehouse(PlcPackage t)
        {
            Dictionary<int, bool> dcWh = new Dictionary<int, bool>();

            for (int i = 36; i <= 493; i++)
            {
                dcWh.Add(i, t.arrayBoolRcmd[i - 36]);
            }


            t.property.getRobot.dcWarehouse = dcWh;
        }
    }

    public partial class AdapterWarehouse : IPlcOperate<PlcPackage>
    {
        public async Task<bool> GetWarehouse(PlcPackage t)
        {
            try
            {
                if (t.master == null)
                {
                    setModbusTcpError();
                }

                getCmd(EGetOperate.Warehouse, t);
                await getMultiInputAsync(t);
                unpack(EGetOperate.Warehouse, t);

                return true;
            }
            catch (Exception ex)
            {
                t.errorLog = ex.Message;
                return false;
            }
        }
    }

    public partial class AdapterWarehouse
    {
        void setModbusTcpError()
        {
            throw new InvalidOperationException("Modbus Tcp Disconnect");
        }

        async Task getMultiInputAsync(PlcPackage t)
        {
            t.arrayBoolRcmd = await t.master.ReadInputsAsync((byte)t.station, (ushort)t.startAddress, (ushort)t.offset);
        }
    }

    public partial class AdapterWarehouse
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

        public Task<bool> SetHeartBeat(PlcPackage t)
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
