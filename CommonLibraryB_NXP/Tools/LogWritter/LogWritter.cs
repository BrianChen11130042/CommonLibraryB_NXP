using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Web;

namespace CommonLibraryB.Tools.LogWritter
{
    public enum EStatus
    {
        Event,
        Info,
        Error
    }

    public class LogWritter : INLogWritterObserver
    {
        public Logger logger;
        public string _directory;
        static Mutex mutex;

        public LogWritter(string dir)
        {
            _directory = dir + "Logs";

            mutex = new Mutex();

            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);

            logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        }

        public async Task WriteNLog(EStatus status, string msg)
        {
            mutex.WaitOne();

            switch (status)
            {
                case EStatus.Event:
                    logger.Trace(msg);
                    break;

                case EStatus.Info:
                    logger.Info(msg);
                    break;

                case EStatus.Error:
                    logger.Error(msg);
                    break;

                default:
                    break;
            }

            mutex.ReleaseMutex();
        }
    }
}
