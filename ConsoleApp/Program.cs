using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            // Read file.
            var executingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            if (string.IsNullOrEmpty(executingAssemblyLocation))
            {
                Console.WriteLine("Assembly location is invalid");
                Console.ReadLine();
                return;
            }

            var path = Path.GetDirectoryName(executingAssemblyLocation);
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Path is invalid");
                Console.ReadLine();
                return;
            }

            var szImagePath = Path.Combine(path, "sniper.jpg");
            // Check file existence.
            if (!File.Exists(szImagePath))
            {
                Console.WriteLine($"{szImagePath} is invalid");
                Console.ReadLine();
                return;
            }

            var bytes = File.ReadAllBytes(szImagePath);
            // Write file copy.
            var fileName = $"{Guid.NewGuid().ToString("D")}.gz";
            var compressedFile = Path.Combine(path, fileName);

            using (var outputFileStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (var gZipStream = new GZipStream(outputFileStream,
                    CompressionMode.Compress, true))
                    gZipStream.Write(bytes, 0, bytes.Length);
            }
            
            Console.WriteLine("Completed");
            Console.ReadLine();
        }

        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile =
                                File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream compress =
                            new GZipStream(outFile,
                            CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(compress);

                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                                fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }

        public static void Decompress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length -
                        fi.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream decompress = new GZipStream(inFile,
                            CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fi.Name);

                    }
                }
            }
        }
    }
}
