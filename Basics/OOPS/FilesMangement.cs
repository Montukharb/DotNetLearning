using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Basics.OOPS
{
    /*File → direct file operations(read / write / delete)
Directory → folders manage karna
Path → file path handle karna
FileInfo / DirectoryInfo → advanced info*/
    internal static class FilesMangement
    {

        private static string path = "D:\\Dot Net Trainning\\ProjectFilesOperations";
        private static void AllOperation()
        {
            //File.WriteAllText("D:\\Dot Net Trainning\\ProjectFilesOperations\\demo.txt", "2 hello this is testing file");
            //WriteLine("path = " + path);
            //var res = File.Create(path + "\\demo.txt");
            //WriteLine("Created");


            /*1.
            create
            ⚠️ Rules:
            File exist hai → purani delete ho jayegi
            Return type = FileStream(close karna zaruri)
            */
            FileStream fs = File.Create(path+"\\Demo.txt");
            fs?.Close();

            /*2.
            WriteAllText  
            new text will be overwrite
            ⚠️ Rules:
            File exist hai → data replace ho jayega
            File nahi hai → new create 
             */
            File.WriteAllText(path + "\\Demo2.txt", "Hello world file nahi to new create agar hai to data replace/overwrite hoga");

            /*
             3.AppendAllText
            File ke end ma text add karna
            File nahi hai auto create ho zaye gi
            Old Data Safe 
             */
            File.AppendAllText(path + "\\append.txt", "Hello this is new data\n");

            /*
             4. ReadAllText()
             Definition:

             File ka sara text read karta hai
             File exist honi chahiye
             warna error 
             */
            if(File.Exists(path+"\\ReadingDocument.txt"))
            {
            string data = File.ReadAllText(path + "\\ReadingDocument.txt");
            WriteLine(data);
            }
            else
            {
                WriteLine("File does't exit");
            }

            /*
             🔥 5. ReadAllLines()
            Definition:

            File ko line-by-line array me read karta hai
            
            Rules:
            Har line separate index me
            large files me memory heavy ho sakta hai
             */
            WriteLine("ReadAllLines");
            if(File.Exists(path+"\\ReadingDocument.txt"))
            {

            string[] lines = File.ReadAllLines(path + "\\ReadingDocument.txt");
                foreach(string line in lines)
                {
                    WriteLine(line);
                }
            }

            /*
             🔥 6. WriteAllLines()
              Definition:
              Array ki lines file me likhta hai

              

               Rules:
              Overwrite karega
              har element ek new line
             */
            string[] data1 = { "A", "B", "C" };
            File.WriteAllLines(path + "\\WriteAllLines.txt", data1); //each element in add new line not recommended heavy task

            /*
             7. Delete
            Definition:

            File ko delete karta hai

            Rules:
            Permanently delete
            agar file open hai → error aa sakta hai
             */

            if(File.Exists(path+"\\demo.txt"))
            { 
            File.Delete("a.txt");
                WriteLine("File deleted");
            }
            else
            {
                WriteLine("File does't exits");
            }
        }
        internal static void CreateFile(string filename)
        {

            string? newPath = path + $@"\{filename}.txt";
            FileStream? fs = null;
            try
            {

            if (File.Exists(newPath))
            {
                throw new Exception("File already exits try another name");
            }
            else
            {
                fs = File.Create(newPath);
                WriteLine("file created.");
            }
            }catch(Exception ex)
            {
                WriteLine($"Error occured during file creation {ex.Message}");
            }
            finally
            {
                fs?.Close();  //file close;
            }
        }
        internal static void WriteAllText(string filename,string content)
        {
            string? newPath = path + $@"\{filename}.txt";
            try
            {
                if(File.Exists(newPath))
                {
                    WriteLine("if you overwrite file type 1 for yes and 0 for No");
                    char? input = (char)Read();
                   
                    bool res = int.TryParse(input.ToString(), out int num);
                    if(res)
                    {
                        if(num ==1)
                        {
                            File.WriteAllText(newPath, content);
                            WriteLine("File Writing success");
                        }
                        else if(num ==0)
                        {
                            File.WriteAllText(newPath, content);
                            WriteLine("File Writing success");
                        }
                    }
                    throw new Exception("Wrong input");
                }
                else
                {
                    File.WriteAllText(newPath, content);
                    WriteLine("File Writing success");
                }
            }catch(Exception ex)
            {
                WriteLine("Error occurred during File" + ex.Message);
            }

        }

        internal static void AppendAllText(string filename,string content)
        {
            string? newPath = path + $@"\{filename}.txt";
            try
            {
                File.AppendAllText(newPath, content);
                WriteLine("Text append successfull");

            }
            catch (Exception ex)
            {
                 WriteLine($"error occurr during append text in file: {ex.Message}");
            }

        }

        internal static void FileDelete(string filename)
        {
            string? newPath = path + $@"\{filename}.txt";
         try{   if (File.Exists(newPath))
            {
                File.Delete(newPath);
                WriteLine("File deleted success");
            }
            else
            {
                WriteLine("Can't find file try correct name");
            }
        }catch(Exception ex)
            {
                WriteLine($"error occured during file deleting {ex.Message}");
            }
        }

        internal static void ReadAllText(string filename)
        {
            string? newPath = path + $@"\{filename}.txt";
            try
            {

                if (File.Exists(newPath))
                {
                    string data = File.ReadAllText(newPath);
                    WriteLine(data);
                }
                else
                {
                    WriteLine("File does't exit");
                }
            }
            catch (Exception ex)
            {
                WriteLine($"error occured during file reading all text: {ex.Message}");
            }
        }

        internal static void ReadLineByLine(string filename)
        {
            string? newPath = path + $@"\{filename}.txt";
            try
            {
                if (File.Exists(newPath))
                {
                    string[] line = File.ReadAllLines(newPath);
                    foreach (string l in line)
                    {
                        WriteLine(l);
                    }
                }
                else
                {
                    WriteLine("file does't exits try another name");
                }
            }
            catch (Exception ex)
            {
                WriteLine("error occurrd during file reading line by line: " + ex.Message);
            }
        }


        internal static void WriteFileLineByLine(string filename, string[] content)
        {
            string? newPath = path + $@"\{filename}.txt";
            try
            {
                if (File.Exists(newPath))
                {
                    WriteLine("file already exists overwrite text");
                }
                File.WriteAllLines(newPath, content);
                WriteLine("Writing lines completed");
            }
            catch (Exception ex)
            {
                WriteLine($"error occurred during writing file line by line {ex.Message}");
            }

        }
        internal static void CopyFile(string source, string destination,bool overwrite)
        {
            try
            {
                string internalPath = path + $"\\{source}.txt";
                string destinationPath = path + $"\\{destination}.txt";
                if (File.Exists(destination))
                {
                    WriteLine("File exits overwriting this file and copying starting...");
                    File.Copy(internalPath, destinationPath, overwrite);
                    WriteLine("Copy successfull");
                }
                else //not exits case
                {
                    WriteLine("File does't exits create new file and copying starting...");
                    File.Copy(internalPath, destinationPath);
                    WriteLine("Copy successfull");
                }
               
            }catch(Exception ex)
            {
                WriteLine($"Error for copying files : {ex.Message}");
            }
        }

        //move file method 
        //source delete and if destionation exits throw error
        internal static void MoveFile(string source, string destination)
        {
            try
            {
                string internalPath = path + $"\\{source}.txt";
                string destinationPath = path + $"\\{destination}.txt";

                if (File.Exists(internalPath))
                {
                    if(File.Exists(destination))
                    {
                        throw new Exception("Destination file already exits please change your destination or delete first");
                    }
                    else
                    {
                        WriteLine("Moving start..");
                        File.Move(internalPath, destinationPath);
                        WriteLine("Moving successfull");
                    }
                }
                else //not exits case
                {

                    throw new Exception("Source does't exits please change file source");
                }

            }
            catch (Exception ex)
            {
                WriteLine($"Error for Moving files : {ex.Message}");
            }
        }

        //replace file 
        //source delete and destination paste
        //destinationfile must be present
        //backup file overwritten if exits safe use for add in end counter

        internal static void ReplaceFile(string source, string destination,string backup)
        {
            try
            {
                string internalPath = path + $"\\{source}.txt";
                string destinationPath = path + $"\\{destination}.txt";
                
                string backupPath = Guid.NewGuid() + ".txt"; //guid = Globally unique identifier which is impossible never repeat
                if (File.Exists(internalPath))
                {
                    if (File.Exists(destination))
                    {
                        File.Replace(internalPath,destinationPath,backupPath);
                    }
                    else
                    {
                        throw new Exception("Destination file does't exits create first");
                    }
                }
                else //not exits case
                {

                    throw new Exception("Source does't exits please change file source");
                }

            }
            catch (Exception ex)
            {
                
                WriteLine($"Error for Replacing files : {ex.Message}");
            }
        }
        internal static void StreamReaderFile(string filename)
        {
            string? newPath = path + $@"\{filename}.txt";

            try
            {
                if (!File.Exists(newPath))
                {
                    return; 
                }
            using(StreamReader sr = new StreamReader(newPath))
            {
                    //WriteLine(sr.ReadToEnd()); //ye eak sath read kar de ga automatic
                    string? line;
                   while((line= sr.ReadLine()) != null)
                        {
                        WriteLine($"{line}");
                    }
            }
            }
            catch(Exception ex)
            {
                WriteLine($"error occurred during reading file using stream: {ex.Message}");
            }
        }

        internal static void StreamWriterFile(string filename, string content)
        {
            string? newPath = path + $@"\{filename}.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(newPath))
                {
                    sw.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                WriteLine($"error occurred during writing file using stream: {ex.Message}");
            }
        }
    }

    internal static class DirectoryClass
    {
        internal static void GetINFO()
        {
            
            DirectoryInfo info =  Directory.CreateDirectory("TestingDir");
            WriteLine("directory path " + info.FullName);
        }
    }
}