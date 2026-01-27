using CommonLibraryB_NXP.Library.UPS.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Config
{

    public class UpsConfig
    {
        public string device { get; set; }

        public string com { get; set; }

        public EUpsSupplier supplier { get; set; } = EUpsSupplier.Phoenixtec;
    }
}
