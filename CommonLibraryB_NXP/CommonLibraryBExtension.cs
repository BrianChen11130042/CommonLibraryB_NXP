using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB_NXP.Library.PLC.Config;
using CommonLibraryB_NXP.Library.PLC.Property;
using CommonLibraryB_NXP.Library.PLC;
using CommonLibraryB_NXP.Manager.ModbusTcp.Master;
using CommonLibraryB_NXP.Tools.LogWritter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommonLibraryB_NXP
{
    public static class CommonLibraryBExtension
    {
        public static IHostApplicationBuilder AddCommonLibraryB<EPLC>(this IHostApplicationBuilder builder, string filePath = "D:\\")
        {
            #region Tools

            builder.Services.AddSingleton<LogWritter>(provider => new LogWritter(filePath));

            #endregion

            #region Manager

            builder.Services.AddSingleton<ModbusTcpMasterManager>(provider => new ModbusTcpMasterManager(filePath));

            #endregion

            #region Library

            builder.Services.AddSingleton<PlcConfigManager<EPLC>>(provider => new PlcConfigManager<EPLC>(filePath));
            builder.Services.AddSingleton<PlcPropertyManager<EPLC>>(provider => new PlcPropertyManager<EPLC>(filePath));
            builder.Services.AddSingleton<PlcLibrary<EPLC>>();

            #endregion

            return builder;
        }
    }
}
