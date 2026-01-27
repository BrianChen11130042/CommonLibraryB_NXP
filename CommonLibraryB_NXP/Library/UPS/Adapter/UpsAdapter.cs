using CommonLibraryB_NXP.Base.Adapter;
using CommonLibraryB_NXP.Library.UPS.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB_NXP.Library.UPS.Adapter
{
    public enum EUpsSupplier { Phoenixtec, Stub}

    public class UpsAdapter : AdapterBase<UpsConfig, EUpsSupplier, IUpsOperate<UpsPackage>>
    {
        public UpsAdapter(List<UpsConfig> keys) : base(keys)
        {

        }

        protected override void InitAdapter(List<UpsConfig> keys)
        {
            foreach(var v in keys)
            {
                EUpsSupplier supplier = v.supplier;

                if (table.ContainsKey(supplier))
                    continue;

                switch(supplier)
                {
                    case EUpsSupplier.Phoenixtec:
                        table.Add(supplier, new AdpaterPhoenixtec());
                        break;

                    case EUpsSupplier.Stub:
                        table.Add(supplier, new AdapterStub());
                        break;
                }
            }
        }
    }
}
