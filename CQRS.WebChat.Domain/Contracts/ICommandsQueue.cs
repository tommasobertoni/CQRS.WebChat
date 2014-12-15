using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CQRS.WebChat.Domain.Contracts
{
    public abstract class ICommandsQueue
    {
        public abstract void Push(ICommand command);

        public abstract ICommand Pop();

        public static byte[] CommandToByteArray(ICommand command)
        {
            if (command == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, command);
            return ms.ToArray();
        }

        public static ICommand CommandFromByteArray(byte[] arrBytes)
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
