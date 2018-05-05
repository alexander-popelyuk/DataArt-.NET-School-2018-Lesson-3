using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Task_1
{
    //
    // Summary:
    //   Main class containing entry point to the program.
    class Program
    {
        // Limit input deserialization file size.
        const long MaxInputSize = 5 * 1024 * 1024;
        //
        // Summary:
        //   Entry point to the program.
        //   Process command line arguments and run application routine.
        //
        // Parameters:
        //   args:
        //     Command line arguments to the program.
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 1)
                {
                    PrintError("Too many arguments.");
                    PrintUsage();
                }
                else if (args.Length == 1 && args[0] == "/?") PrintHelp();
                else
                {
                    if (args.Length > 0) ProcessClients(GetClients(args[0]));
                    else PrintError("No input specified.");
                }
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            #if DEBUG
                Console.ReadKey(true);
            #endif
        }

        private static BankClient[] GetClients(string path)
        {
            Console.Write("Processing '{0}'...", path);

            if (!File.Exists(path))
            {
                throw new Exception(string.Format("Input file {0} does not exit.", path));
            }
            if (new FileInfo(path).Length > MaxInputSize)
            {
                throw new Exception(string.Format("File size too big to process (max = {0}).", MaxInputSize));
            }

            var serializer = new XmlSerializer(typeof(BankClient[]));

            using (var stream = new StreamReader(path))
            {
                BankClient[] clients = (BankClient[])serializer.Deserialize(stream);
                Console.WriteLine("OK!");
                return clients;
            }
        }

        private static void ProcessClients(BankClient[] clients)
        {
            throw new NotImplementedException();
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
        //
        // Summary:
        //   Print program help text to standard output stream.
        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Process clients database and print statistics.");
            PrintUsage();
        }
        //
        // Summary:
        //   Print program usage text to standard output stream.
        static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine("USAGE: Task_1.exe [ input ]");
            Console.WriteLine();
            Console.WriteLine("input\tInput file to process.");
        }
        //
        // Summary:
        //   Print error message to standard error stream.
        //
        // Parameters:
        //   text:
        //     Error text to print.
        static void PrintError(string text)
        {
            Console.Error.WriteLine();
            Console.Error.WriteLine("ERROR: {0}", text);
        }
    }
}
