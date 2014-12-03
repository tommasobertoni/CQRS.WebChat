using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Contracts
{
    [Serializable]
    public abstract class ICommand
    {
        public static byte[] ToByteArray(ICommand command)
        {
            if (command == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, command);
            return ms.ToArray();
        }

        public static ICommand FromByteArray(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            ICommand command = (ICommand)binForm.Deserialize(memStream);
            return command;
        }
    }
}
