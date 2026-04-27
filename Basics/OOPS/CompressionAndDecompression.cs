
using System.IO.Compression;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Basics.OOPS
{
    //✔ “Compression and Decompression using Streams”
    /* 
      System.IO.Compression is a software namespace in the Microsoft .NET platform that provides classes for file and data compression and decompression. It supports industry-standard algorithms such as ZIP and GZIP, enabling developers to efficiently handle compressed data streams within .NET applications.
      Key facts:
       Platform: .NET (Framework, Core, and 5+)
       
       Primary formats: ZIP, GZIP, Brotli
       
       Core classes: ZipArchive, ZipFile, DeflateStream, GZipStream, BrotliStream
       
       Introduced: .NET Framework 2.0; expanded in later .NET releases
     */


    /*
     🔹 ZIP kya hai?

         👉 Multiple files ko ek archive me compress karta hai
         
         👉 Features:
         
         Multiple files + folders support
         Folder structure maintain karta hai
         Windows me direct open ho jata hai
         
         👉 Example:
         
         project.zip → andar 10 files ho sakti hain
         
         🔹 GZIP kya hai?
         
         👉 Single file ko compress karta hai
           Fast hai as compare to Zip but isme folder structure nahi hota or zip ma folder maintain bana rahte hai
          
         
         👉 Features:
         
         Sirf 1 file compress karta hai
         Mostly Linux / web me use hota hai
         .gz extension
         
         🔸 2. Speed & Performance
            GZip → fast + efficient (especially streaming me)
            ZIP → thoda heavy hota hai
            
            👉 Isliye servers pe:
            
            HTTP response → GZip use hota hai
         

               | Situation                            | Best Choice |
           | ------------------------------------ | ----------- |
           | Website speed improve (API response) | GZip        |
           | Backup / sharing files               | ZIP         |
           | Logs compress karna                  | GZip        |
           | Folder bhejna                        | ZIP         |

         👉 Example:
         
         file.txt → file.txt.gz
     */
    internal class CompressionAndDecompression
    {
        //compress data using Gzip compression single file
        internal void CompressData()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            try
            {

                if (!File.Exists(Path.Combine(path, "ok.txt")))
                {
                    throw new Exception("File not exits");
                }
                else if (File.Exists(Path.Combine(path, "ok.gz")))
                {
                    throw new Exception("File Already compressed");
                }
                using (FileStream source = File.OpenRead(Path.Combine(path, "ok.txt")))
                using (FileStream target = File.Create(Path.Combine(path, "ok.gz")))
                using (GZipStream gzip = new GZipStream(target, CompressionMode.Compress))
                {
                    source.CopyTo(gzip);
                    WriteLine("Gzip successfull");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occurred: " + ex.Message);
            }
        }

        //decompress data using Gzip
        internal void DeCompressData()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            try
            {

                if (!File.Exists(Path.Combine(path, "ok.gz")))
                {
                    throw new Exception("File not exits");
                }
                else if (File.Exists(Path.Combine(path, "okDecompressed.txt")))
                {
                    throw new Exception("File Already DeCompressed");
                }

                using (FileStream source = File.OpenRead(Path.Combine(path, "ok.gz")))
                using (FileStream target = File.Create(Path.Combine(path, "okDecompressed.txt")))
                using (GZipStream gzip = new GZipStream(source, CompressionMode.Decompress))
                {
                    gzip.CopyTo(target);
                    WriteLine("Gzip decompression successfull");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occurred: " + ex.Message);
            }
        }

        //compress data using Read method itself hardcoded 
        internal void CompressDataUsingWrite()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            try
            {

                if (File.Exists(Path.Combine(path, "okWriteCompress.gz")))
                {
                    throw new Exception("File already compressed");
                }

                string data = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";

                byte[] buffer = Encoding.UTF8.GetBytes(data);

                using (FileStream target = File.Create(Path.Combine(path, "okWriteCompress.gz")))
                using (GZipStream gzip = new GZipStream(target, CompressionMode.Compress))
                {
                    gzip.Write(buffer, 0, buffer.Length);
                    gzip.Flush(); //ensure data write ho gaya 
                    WriteLine("Gzip compression successfull");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occurred: " + ex.Message);
            }
        }

        //compress data using Read method itself hardcoded 
        internal void DeCompressDataUsingRead()
        {

            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            try
            {

                if (!File.Exists(Path.Combine(path, "okWriteCompress.gz")))
                {
                    throw new Exception("File not exits");
                }
                else if (File.Exists(Path.Combine(path, "okReadDecompress.txt")))
                {
                    throw new Exception("File already De compressed");
                }



                using (FileStream target = File.OpenRead(Path.Combine(path, "okWriteCompress.gz")))
                using (GZipStream gzip = new GZipStream(target, CompressionMode.Decompress))
                using (MemoryStream ms = new MemoryStream())
                {

                    gzip.CopyTo(ms);  //load data into memory
                    ms.Position = 0;
                    byte[] buffer = new byte[ms.Length];
                    int readbyte = ms.Read(buffer, 0, buffer.Length);
                    File.WriteAllBytes(Path.Combine(path, "okReadDecompress.txt"), ms.ToArray());
                    WriteLine("Reading data from okWriteCompress.gz = " + Encoding.UTF8.GetString(buffer, 0, readbyte));
                    WriteLine("Gzip successfull");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occurred: " + ex.Message);
            }
        }


    }

    /*
     📦 0. What is ZipArchive?
        📌 English:
        
        👉 Used to create, read, update ZIP files
        
        🧠 Hinglish:
        
        👉 multiple files ko ek .zip file me store/manage karna
        
        🧠 Real Life Use
        file download (multiple files ek zip me)
        backup system
        project export..

        ZipArchiveMode.Create -> zip create karna
        ZipArchiveMode.Read   -> zip read karna
        ZipArchiveMode.Update  -> add/delete/update

     */
    internal class ZipArchiveClass
    {

        internal void CreateZipBasic()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            try
            {
                if (File.Exists(Path.Combine(path, "Basic.Zip")))
                {
                    throw new Exception("Basic.zip already Exits choose another file");
                }

                using (FileStream fs = new FileStream(Path.Combine(path, "Basic.Zip"), FileMode.Create, FileAccess.ReadWrite, FileShare.None))  //Create zip file
                using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))  //Open Zip Container and link to zip file path
                {
                    ZipArchiveEntry entry = zip.CreateEntry("File.txt");
                    ZipArchiveEntry entry2 = zip.CreateEntry("File2.txt");

                    using (StreamWriter sw = new StreamWriter(entry.Open()))
                    {
                        sw.WriteLine("This is user created file and write through stream writer open using zipArchive Mode");
                    }
                    WriteLine("Zip File Created and entered one custom file");

                    using (StreamWriter sw2 = new StreamWriter(entry2.Open()))
                    {
                        sw2.WriteLine("This is second file write by zip and stream writer class");
                    }

                }
            }
            catch (Exception ex)
            {
                WriteLine("Error occur during Zip file Compressing: " + ex.Message);
            }
        }


        //send multiple files in zip file 
        internal void SendMultipleFiles()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            try
            {
                if (File.Exists(Path.Combine(path, "BasicContainer.Zip")))
                {
                    throw new Exception("BasicContainer.zip already Exits choose another file");
                }

                using (FileStream fs = new FileStream(Path.Combine(path, "BasicContainer.Zip"), FileMode.Create, FileAccess.ReadWrite, FileShare.None))  //Create zip file
                using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))  //Open Zip Container and link to zip file path
                {
                    // send normal file into zip folder and compress
                    zip.CreateEntryFromFile(Path.Combine(path, "New Text Document.txt"), Path.GetFileName("New Text Document.txt"));
                    zip.CreateEntryFromFile(Path.Combine(path, "New Text Writing Document.txt"), "New Text Writing Document.txt");
                    WriteLine("File compressing successfull using zip Archive");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Error occur during Zip file Compressing: " + ex.Message);
            }
        }


        //send bulk files in zip using for loop
        internal void SendBulkMultipleFiles()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            try
            {
                if (File.Exists(Path.Combine(path, "BasicBulkContainer.Zip")))
                {
                    throw new Exception("BasicBulkContainer.Zip already Exits choose another file");
                }

                using (FileStream fs = new FileStream(Path.Combine(path, "BasicBulkContainer.Zip"), FileMode.Create, FileAccess.ReadWrite, FileShare.None))  //Create zip file
                using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))  //Open Zip Container and link to zip file path
                {

                    string[] filenames = { "New Text Document.txt", "New Text Writing Document.txt", "New Text WritingAppending Document.txt", "Ok.txt" };

                    foreach (string file in filenames)
                    {
                        zip.CreateEntryFromFile(Path.Combine(path, file), Path.GetFileName(file));
                    }
                    WriteLine("Zipping bulk file successfull")
;
                }
            }
            catch (Exception ex)
            {
                WriteLine("Error occur during Zip file Compressing: " + ex.Message);
            }
        }

        //send direct folder or directory to zip file
        internal void DirToZiDir()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
          
            try
            {
                if (File.Exists(Path.Combine(path, "BigZipper.zip")))
                {
                    throw new Exception("BigZipper.zip Already Exists");
                }

                using (FileStream fs = new FileStream(Path.Combine(path, "BigZipper.zip"), FileMode.Create, FileAccess.Write, FileShare.None))
                using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    string[] files = Directory.GetFiles(Path.Combine(path, "Confidencial Record"), "*", searchOption: SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        string destinationfileName = file.Substring(path.Length + 1);
                        zip.CreateEntryFromFile(file, destinationfileName);
                    }
                    WriteLine("All Directory are compressed successfully");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occur during Directory Compressing: " + ex.Message);
            }
        }

        /*
         👉 ZipArchiveMode.Read me ZIP file ko open karke zip.Entries ke through andar ki files ko loop karke access karte hain, aur entry.FullName se unka path/name milta hai.
         */
        internal void AccesssAllFilesInZipFile()
        {


        }
    }
}
