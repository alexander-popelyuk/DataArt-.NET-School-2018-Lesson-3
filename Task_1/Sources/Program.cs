using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("it works");
            Console.ReadKey(true);
        }

        //
        // Summary:
        //   Serialize object to XML format.
        //
        // Parameters:
        //   obj:
        //     Object to serialize.
        // 
        //   path:
        //     File path to which serialized object will be written.
        public static void ToXml(object obj, string path)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var stream = new StreamWriter(path))
            {
                serializer.Serialize(stream, obj);
            }
        }
        //
        // Summary:
        //   Deserialize object in XML format.
        //
        // Parameters:
        //   path:
        //     Path to the object to deserialize.
        //
        // Return:
        //   Deserialized object.
        public static T FromXml<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var stream = new StreamReader(path))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
