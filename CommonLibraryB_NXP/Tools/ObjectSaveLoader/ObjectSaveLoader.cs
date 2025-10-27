using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibraryB.Tools.ObjectSaveLoader
{
    public enum EErrorObjectSaveLoader { NoError, LoadException, LoadFileNotExist, SaveException }

    public static partial class ObjectSaveLoader
    {
        public static EErrorObjectSaveLoader Error = EErrorObjectSaveLoader.NoError;
        public static string ErrorMessage { get; private set; } = "";
    }

    public static partial class ObjectSaveLoader
    {
        public static bool SaveJason<T>(T obj, string path)
        {
            Error = EErrorObjectSaveLoader.NoError;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var writer = new StreamWriter(path))
                {
                    string jsonString = JsonSerializer.Serialize<T>(obj);
                    writer.Write(jsonString);
                }

                return true;
            }
            catch (Exception ex)
            {
                Error = EErrorObjectSaveLoader.SaveException;
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public static bool LoadJason<T>(string path, out T obj)
        {
            obj = default(T);
            Error = EErrorObjectSaveLoader.NoError;

            try
            {
                if (File.Exists(path))
                {
                    using (var reader = new StreamReader(path))
                    {
                        string jsonString = reader.ReadToEnd();
                        obj = JsonSerializer.Deserialize<T>(jsonString);
                    }

                    return true;
                }
                else
                {
                    Error = EErrorObjectSaveLoader.LoadFileNotExist;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Error = EErrorObjectSaveLoader.LoadException;
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
