using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficSimulation
{
    class Serialize
    {
        public Serialize()
        {
        }

        /// <summary>
        /// Serialize object in binary format in order to be saved
        /// </summary>
        /// <param name="filename">The name of the file that will be saved</param>
        /// <param name="objectToSerialize">The object that will be serialized</param>
        public void SerializeObject(string filename, object objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create, FileAccess.Write);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        /// <summary>
        /// Deserialize object in binary format in order to be open from a saved file
        /// </summary>
        /// <param name="filename">The name of the saved file</param>
        /// <returns>The serialized object</returns>
        public object DeSerializeObject(string filename)
        {
            object objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}