// MIT License
// 
// Copyright(c) 2018 Alexander Popelyuk
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


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
        // Default command line parameters.
        const string DefaultOutputDirectory = ".";
        // Statistics output file name.
        const string OutputFileName = "bank_clients_statistics";
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
                if (args.Length == 0)
                {
                    PrintError("No input specified.");
                    PrintUsage();
                }
                else if (args.Length > 2)
                {
                    PrintError("Too many arguments.");
                    PrintUsage();
                }
                else if (args.Length == 1 && args[0] == "/?")
                {
                    PrintHelp();
                }
                else
                {
                    string output_directory = DefaultOutputDirectory;
                    if (args.Length > 1) output_directory = args[1];
                    BankClient[] clients = GetClients(args[0], FromXml<BankClient[]>);
                    string output_path = Path.Combine(output_directory, OutputFileName + ".xml");
                    ProcessClients(clients, output_path, ToXml);
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
        //
        // Summary:
        //   Load and deserialize clients from the specified file path.
        //
        // Parameters:
        //   path:
        //     Path to the file to load clients information from.
        //
        //   deserializer:
        //     Deserialization function to use.
        //
        // Return:
        //   Array of 'BankClients' objects loaded from specified 'path' argument.
        private static BankClient[] GetClients(string path, Func<string, BankClient[]> deserializer)
        {
            Console.Write("Processing '{0}'... ", path);

            if (!File.Exists(path))
            {
                throw new Exception(string.Format("Input file {0} does not exit.", path));
            }
            if (new FileInfo(path).Length > MaxInputSize)
            {
                throw new Exception(string.Format("File size too big to process (max = {0}).", MaxInputSize));
            }

            BankClient[] clients = deserializer(path);
            Console.WriteLine("OK!");
            return clients;
        }

        private static void ProcessClients(BankClient[] clients, string output_path, Action<object, string> serializer)
        {
            ClientStatistics clients_statistics = new ClientStatistics();

            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }

            serializer(clients_statistics, output_path);
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
            Console.WriteLine("Process clients database print statistics and save it to output file.");
            PrintUsage();
        }
        //
        // Summary:
        //   Print program usage text to standard output stream.
        static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine("USAGE: Task_1.exe input [ output ]");
            Console.WriteLine();
            Console.WriteLine("input\tInput file to process.");
            Console.WriteLine("output\tDirectory for output (default: '{0}').", DefaultOutputDirectory);
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
