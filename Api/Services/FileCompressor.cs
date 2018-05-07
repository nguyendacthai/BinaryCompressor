using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using Api.Interfaces;

namespace Api.Services
{
    public class FileCompressor : IFileCompressor
    {
        public void Compress(FileInfo fi)
        {
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

                            //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            //    fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }

        public void Decompress(FileInfo fi)
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

                        //Console.WriteLine("Decompressed: {0}", fi.Name);

                    }
                }
            }
        }
    }
}