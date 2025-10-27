using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB.Tools.TypeConverter
{
    public enum EEndian
    {
        LittleEndian,
        BigEndian,
    }

    public static class StringUshortConverter
    {
        public static void UshortArrayToString(ushort[] arrayData, EEndian inputEndianType, out string result)
        {
            if(arrayData == null || arrayData.Length == 0)
                throw new ArgumentException("Input ushort array null at UshortArrayToString conversion ");

            List<byte> listByte = new List<byte>();

            foreach (var data in arrayData)
            {
                var bytes = BitConverter.GetBytes(data);

                if ((BitConverter.IsLittleEndian && inputEndianType == EEndian.BigEndian) ||
                   (!BitConverter.IsLittleEndian && inputEndianType == EEndian.LittleEndian))
                {
                    Array.Reverse(bytes);
                }

                listByte.AddRange(bytes);
            }

            byte[] arrayByte = listByte.ToArray();
            int length = Array.FindLastIndex(arrayByte, b => b != 0) + 1;

            string res = Encoding.ASCII.GetString(arrayByte, 0, length);
            result = res;
        }

        public static void StringToUshortArray(string data, EEndian outputEndianType, int targetLength, out ushort[] result)
        {
            if (data == null)
                data = string.Empty;

            List<ushort> listRes = new List<ushort>();

            byte[] arrayByte = ASCIIEncoding.ASCII.GetBytes(data);

            if (arrayByte.Length % 2 != 0)
                arrayByte = arrayByte.Append((byte)0x00).ToArray();

            for (int i = 0; i < arrayByte.Length; i += 2)
            {
                var byteSpan = arrayByte.AsSpan(i, 2);

                switch (outputEndianType)
                {
                    case EEndian.LittleEndian:
                        listRes.Add(BinaryPrimitives.ReadUInt16LittleEndian(byteSpan));
                        break;

                    case EEndian.BigEndian:
                        listRes.Add(BinaryPrimitives.ReadUInt16BigEndian(byteSpan));
                        break;
                }
            }

            while(listRes.Count < targetLength)
            {
                listRes.Add(0);
            }

            if(listRes.Count > targetLength)
            {
                listRes = listRes.Take(targetLength).ToList();
            }

            result = listRes.ToArray();
        }
    }
}
