using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Property
{

    public class PlcProperty
    {
        public string device { get; set; }

        public SetPierProperty setPier { get; set; } = new SetPierProperty();
        public GetPierProperty getPier { get; set; } = new GetPierProperty();

        public SetRobotProperty setRobot { get; set; } = new SetRobotProperty();
        public GetRobotProperty getRobot { get; set; } = new GetRobotProperty();

        public SetHeartbeatProperty setHeartbeat { get; set; } = new SetHeartbeatProperty();

        public GetWarehouseProperty getWarehouse { get; set; } = new GetWarehouseProperty();

    }

    public class SetPierProperty
    {
        public ushort missionStart { get; set; }

        public ushort missionFinish { get; set; }

        public ushort heartBeat { get; set; }
    }

    public class GetPierProperty
    {
        public int pierNo { get; set; }

        public ushort missionStatus { get; set; }

        public int isReady { get; set; }

        public int errorCode { get; set; }
    }

    public class SetRobotProperty
    {
        public ushort missionStart { get; set; }

        public ushort missionFinish { get; set; }

        public string barcode { get; set; }

        public ushort boardSize { get; set; }

        public ushort pickZone { get; set; }

        public ushort pickLayer { get; set; }

        public ushort dropZone { get; set; }

        public ushort dropLayer { get; set; }
    }

    public class GetRobotProperty
    {
        public int robotNo { get; set; }

        public ushort missionStatus { get; set; }

        public int isReady { get; set; }

        public int errorCode { get; set; }
    }

    public class SetHeartbeatProperty
    {
        public ushort heartBeat { get; set; }
    }

    public class GetWarehouseProperty
    {
        public Dictionary<int, bool> dcWarehouse { get; set; } = new Dictionary<int, bool>();
    }
}
