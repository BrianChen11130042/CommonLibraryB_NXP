using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public interface IPlcOperate<T>
    {

        //******** PIER ********//

        //設置Pier任務開始
        Task<bool> SetPierMissionStart(T t);

        //獲取Pier狀態訊息
        Task<bool> GetPierStatus(T t);

        //設置Pier任務結束
        Task<bool> SetPierMissionFinish(T t);


        //******** ROBOT ********//

        //設置Robot任務資訊
        Task<bool> SetRobotMissionInform(T t);

        //設置Robot任務開始
        Task<bool> SetRobotMissionStart(T t);

        //獲取Robot狀態訊息
        Task<bool> GetRobotStatus(T t);

        //設置Robot任務結束
        Task<bool> SetRobotMissionFinsih(T t);

        //******** COMMON ********//

        //取得設備名稱
        Task<bool> GetDeviceName(T t);
    }
}
