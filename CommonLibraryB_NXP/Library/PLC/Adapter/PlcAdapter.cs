using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB_NXP.Base.Adapter;
using CommonLibraryB_NXP.Library.PLC.Config;

namespace CommonLibraryB_NXP.Library.PLC.Adapter
{
    public enum EPlcSupplier { TM_Pier1, TM_Pier1_Stub, TM_Pier2, TM_Pier2_Stub, TM_RobotArm, TM_RobotArm_Stub }

    public class PlcAdapter : AdapterBase<PlcConfig, EPlcSupplier, IPlcOperate<PlcPackage>>
    {
        public PlcAdapter(List<PlcConfig> keys) : base(keys)
        {

        }

        protected override void InitAdapter(List<PlcConfig> keys)
        {
            foreach(var v in keys)
            {
                EPlcSupplier supplier = v.supplier;

                if (table.ContainsKey(supplier))
                    continue;

                switch(supplier)
                {
                    case EPlcSupplier.TM_Pier1:
                        table.Add(supplier, new AdapterPier1());
                        break;

                    case EPlcSupplier.TM_Pier2:
                        table.Add(supplier, new AdapterPier2());
                        break;

                    case EPlcSupplier.TM_RobotArm:
                        table.Add(supplier, new AdapterTMRobotArm());
                        break;

                    case EPlcSupplier.TM_Pier1_Stub:
                        table.Add(supplier, new AdapterPier1_Stub());
                        break;

                    case EPlcSupplier.TM_Pier2_Stub:
                        table.Add(supplier, new AdapterPier2_Stub());
                        break;

                    case EPlcSupplier.TM_RobotArm_Stub:
                        table.Add(supplier, new AdapterTMRobotArm_Stub());
                        break;
                }
            }
        }
    }
}
