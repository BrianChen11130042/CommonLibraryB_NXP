using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Adapter
{
    public interface IUpsOperate<T>
    {
        public Task<bool> GetUpsStatus(T t);

        Task<bool> GetDeviceNo(T t);
    }
}
