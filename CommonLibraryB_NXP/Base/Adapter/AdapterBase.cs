using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB.Base.Adapter
{
    public abstract class AdapterBase<TConfig, TID, TAdatper>
    {
        protected Dictionary<TID, TAdatper> table;

        public AdapterBase(List<TConfig> keys)
        {
            table = new Dictionary<TID, TAdatper>();
            InitAdapter(keys);
        }

        protected abstract void InitAdapter(List<TConfig> keys);

        public TAdatper this[TID key] { get { return table[key]; } }
    }
}
