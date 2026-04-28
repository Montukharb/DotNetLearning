using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Basics.OOPS
{
    /*
     Brotli is a compression algorithm developed by Google that is designed to provide high compression ratios while maintaining fast decompression speeds. It is commonly used for compressing web content, such as HTML, CSS, and JavaScript files, to reduce the size of data transmitted over the internet.
      
    Best alternative to GZip: Brotli often achieves better compression ratios than GZip, especially for text-based content. It can significantly reduce the size of files, leading to faster load times for web pages.
      
     */
    internal class BrotliCompAndDecompression
    {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");

        internal void compressFile()
        {
            try
            {

                if (File.Exists(Path.Combine(path, "BrotliCompress.br")))
                {
                    throw new Exception("Already Compressed brotli data");
                }
                using(FileStream originalFileStream = File.Open(Path.Combine(path, "Brotli.txt"), FileMode.Open))
                {
                    using(FileStream compressedFileStream = File.Create(Path.Combine(path, "BrotliCompress.br")))
                    {
                        using(BrotliStream brotliStream = new BrotliStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(brotliStream);
                            WriteLine("File compressed successfully.");
                        }
                    }
                }
              
            }
            catch (Exception ex)
            {
                WriteLine("Exception occurred during compressing Folder: " + ex.Message);
            }
        }

        //decompress zip file to original file
        internal void DecompressFile()
        {
            try
            {
                if(!File.Exists(Path.Combine(path, "BrotliCompress.br")))
                {
                    throw new Exception("Compressed brotli data not found");
                }
                else if (!File.Exists(Path.Combine(path, "BrotliDecompress.br")))
                {
                    throw new Exception("Brotli Decompress data alreay exists");
                }
                using (FileStream compressedFileStream = File.Open(Path.Combine(path, "BrotliCompress.br"), FileMode.Open))
                {
                    using (FileStream decompressedFileStream = File.Create(Path.Combine(path, "BrotliDecompress.txt")))
                    {
                        using (BrotliStream brotliStream = new BrotliStream(compressedFileStream, CompressionMode.Decompress))
                        {
                            brotliStream.CopyTo(decompressedFileStream);
                            WriteLine("File decompressed successfully.");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLine("Exception occurred during decompressing Folder: " + ex.Message);
            }
        }

    }
}
