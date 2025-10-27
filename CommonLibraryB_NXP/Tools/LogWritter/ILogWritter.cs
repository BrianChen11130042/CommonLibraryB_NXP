using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB.Tools.LogWritter
{
    public interface INLogWritterObservable
    {
        void AddNLogWritterObserver(INLogWritterObserver o);

        void RemoveNLogWritterObserver(INLogWritterObserver o);

        Task NotifyNLog(EStatus status, string msg);
    }

    public interface INLogWritterObserver
    {
        Task WriteNLog(EStatus status, string msg);
    }
}
