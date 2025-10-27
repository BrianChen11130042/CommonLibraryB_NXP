using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryB.Tools.ObjectSaveLoader;

namespace CommonLibraryB.Base.Manager
{
    public enum EErrorManager { NoError, Exception, WrongQuantity, FileNotExist }

    public abstract partial class ManagerBase<TKey, TObject>
    {
        public string[] keys;
        public string filePath;
        public Dictionary<string, TObject> table;

        public ManagerBase(string filePath, string[] keys = null)
        {
            this.filePath = filePath;

            if(typeof(TKey).IsEnum)
            {
                this.keys = Enum.GetNames(typeof(TKey));
            }
            else if(typeof(TKey).IsArray)
            {
                this.keys = keys;
            }

            InitTable();
        }
    }

    public abstract partial class ManagerBase<TKey, TObject>
    {
        public EErrorManager ERROR { get; private set; } = EErrorManager.NoError;
        public string ErrorMessage = string.Empty;

        void InitTable()
        {
            if (!_load(filePath))
                GenerateDefaultTable();
        }

        public bool Save()
        {
            return _save(filePath);
        }

        bool _save(string filePath)
        {
            ERROR = EErrorManager.NoError;
            bool b = ObjectSaveLoader.SaveJason<Dictionary<string, TObject>>(table, filePath);

            if (b)
                return b;

            switch (ObjectSaveLoader.Error)
            {
                case EErrorObjectSaveLoader.SaveException:
                    ERROR = EErrorManager.Exception;
                    ErrorMessage = ObjectSaveLoader.ErrorMessage;
                    break;
            }

            return b;
        }

        public void Set(string key, TObject target)
        {
            if (table != null && table.ContainsKey(key))
            {
                table[key] = target;
            }
        }

        public TObject Get(string key)
        {
            if (table != null && table.ContainsKey(key))
                return table[key];
            else
                return default(TObject);
        }

        bool _load(string filePath)
        {
            ERROR = EErrorManager.NoError;

            try
            {
                Dictionary<string, TObject> loaded;
                bool b = ObjectSaveLoader.LoadJason<Dictionary<string, TObject>>(filePath, out loaded);

                if (!b)
                {
                    switch (ObjectSaveLoader.Error)
                    {
                        case EErrorObjectSaveLoader.LoadFileNotExist:
                            ERROR = EErrorManager.FileNotExist;
                            break;

                        case EErrorObjectSaveLoader.LoadException:
                            ERROR = EErrorManager.Exception;
                            ErrorMessage = ObjectSaveLoader.ErrorMessage;
                            break;
                    }

                    return false;
                }

                if (!_checkAllKeyExist(keys, loaded.Keys))
                {
                    ERROR = EErrorManager.WrongQuantity;
                    return false;
                }

                table = new Dictionary<string, TObject>();

                foreach (var pair in loaded)
                {
                    table.Add(pair.Key, pair.Value);
                }

                return true;
            }
            catch (Exception ex)
            {
                ERROR = EErrorManager.Exception;
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public abstract void GenerateDefaultTable();

        bool _checkAllKeyExist(IEnumerable<string> origin, IEnumerable<string> loaded)
        {
            foreach (string key in origin)
            {
                if (loaded.Contains(key))
                    continue;
                else
                    return false;
            }

            return true;
        }

    }
}
