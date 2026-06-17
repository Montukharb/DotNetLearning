using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            FileStream fs = File.Create(path + "\\Demo.txt");
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
            if (File.Exists(path + "\\ReadingDocument.txt"))
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
            if (File.Exists(path + "\\ReadingDocument.txt"))
            {

                string[] lines = File.ReadAllLines(path + "\\ReadingDocument.txt");
                foreach (string line in lines)
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

            if (File.Exists(path + "\\demo.txt"))
            {
                File.Delete("a.txt");
                WriteLine("File deleted");
            }
            else
            {
                WriteLine("File does't exits");
            }
            //using (var fs = File.OpenRead("\\abc.txt"))
            //{

            //}
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
            }
            catch (Exception ex)
            {
                WriteLine($"Error occured during file creation {ex.Message}");
            }
            finally
            {
                fs?.Close();  //file close;
            }
        }
        internal static void WriteAllText(string filename, string content)
        {
            string? newPath = path + $@"\{filename}.txt";
            try
            {
                if (File.Exists(newPath))
                {
                    WriteLine("if you overwrite file type 1 for yes and 0 for No");
                    char? input = (char)Read();

                    bool res = int.TryParse(input.ToString(), out int num);
                    if (res)
                    {
                        if (num == 1)
                        {
                            File.WriteAllText(newPath, content);
                            WriteLine("File Writing success");
                        }
                        else if (num == 0)
                        {
                            throw new Exception("Go to greate method create new file");
                            //WriteLine("File Writing success");
                        }
                    }
                    throw new Exception("Wrong input");
                }
                else
                {
                    File.WriteAllText(newPath, content);
                    WriteLine("File Writing success");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Error occurred during File" + ex.Message);
            }

        }

        internal static void AppendAllText(string filename, string content)
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
            try
            {
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                    WriteLine("File deleted success");
                }
                else
                {
                    WriteLine("Can't find file try correct name");
                }
            }
            catch (Exception ex)
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
        internal static void CopyFile(string source, string destination, bool overwrite)
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

            }
            catch (Exception ex)
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
                    if (File.Exists(destination))
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

        internal static void ReplaceFile(string source, string destination, string backup)
        {
            try
            {
                string internalPath = path + $"\\{source}.txt";
                string destinationPath = path + $"\\{destination}.txt";
                //var guid = System.Guid.NewGuid();
                //"fsdaf3532(#$@2340ds543"

                string backupPath = Guid.NewGuid() + ".txt"; //guid = Globally unique identifier which is impossible never repeat
                if (File.Exists(internalPath))
                {
                    if (File.Exists(destination))
                    {
                        File.Replace(internalPath, destinationPath, backupPath);

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
                using (StreamReader sr = new StreamReader(newPath))
                {
                    //WriteLine(sr.ReadToEnd()); //ye eak sath read kar de ga automatic
                    //while (!sr.EndOfStream) { } // run till end of stream data
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        WriteLine($"{line}");
                    }
                }
            }
            catch (Exception ex)
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
            string? path = @"D:\Dot Net Trainning\ProjectFilesOperations\NewDir";
            DirectoryInfo info = Directory.CreateDirectory("TestingDir");
            WriteLine("directory path " + info.FullName);
            info = Directory.CreateDirectory(@"D:\Dot Net Trainning\ProjectFilesOperations\NewDir");
            WriteLine(info.CreationTime.ToString());


            //exits
            if (Directory.Exists(path))
            {
                Console.WriteLine("Folder exists");

            }
            else
            {
                Console.WriteLine("Folder not found");
            }

            if (Directory.Exists("TestingAno"))
            {
                Directory.Delete("TestingAno", true); //True means recursive delete iska ander ka data bhi delete karana ho tab use karte hai empty directory me true ki need nahi hai
            }
            else
            {
                WriteLine("Can't find");
            }

            DirectoryInfo? dir = Directory.GetParent(path);
            if (Directory.Exists(dir?.ToString()))
            {
                string[] files = Directory.GetFiles(dir.ToString(), "*.txt", searchOption: SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    WriteLine($"{file}");
                }
            }
            else
            {
                WriteLine("Not Exists directory");
            }

            //get directories sub folder ki list deta hai
            dir = Directory.GetParent(path);
            if (Directory.Exists(dir?.ToString()))
            {
                string[] alldir = Directory.GetDirectories(dir.ToString(), "*.txt", searchOption: SearchOption.AllDirectories);
                foreach (string childdir in alldir)
                {
                    WriteLine($"{childdir}");
                }
            }
            else
            {
                WriteLine("Not Exists directory");
            }

        }
        internal static void MoveDirectory(string source, string destination)
        {
            source = Path.Combine("D:\\Dot Net Trainning\\ProjectFilesOperations", source);
            try
            {
                if (Directory.Exists(source))
                {
                    if (!Directory.Exists(destination))
                    {
                        Directory.Move(source, destination);  //rename hoga
                                                              //Directory.Move(source, "backup\\newdata");  //backup phele se create hona chaiya folder ka ander data dir chali zaye gi

                        WriteLine("Move file success");
                    }
                    else
                    {
                        throw new Exception("change your destination");
                    }
                }
                else
                {
                    throw new Exception("Change your source");
                }

            }
            catch (Exception ex)
            {
                WriteLine($"error occurred {ex.Message}");
            }
        }


    }

    internal static class PathClass
    {
        internal static void AllOperationsPathClass()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            WriteLine("path = " + path);
            try
            {

                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(Path.Combine(path, "Paramount"));
                    WriteLine("Directory created");
                }
            }
            catch (Exception ex)
            {
                WriteLine($"error occur : {ex.Message}");
            }

        }
        internal static void BasicOperationsPathClass()
        {
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");

            string name = Path.GetFileName(Path.Combine(path, "hello.txt"));
            WriteLine(name);
            WriteLine("full file name = " + Path.GetFileNameWithoutExtension(name));
            WriteLine("Only file name" + Path.GetFileNameWithoutExtension(name));
            WriteLine("Get file ext name = " + Path.GetExtension(name));
            WriteLine("Full path = " + Path.GetFullPath("hello.txt"));
            WriteLine(Path.GetFileName(path));
            WriteLine(Path.GetExtension(path));

            // Change extension
            //real file change nahi hoti only view mode me change 
            //file modify nahi hoti bas string change hoti hai.
            string jsonPath = Path.ChangeExtension(path, ".json");

            // Full path
            WriteLine(Path.GetFullPath(path));

            // Temp file automatic unique file create karta hai or path return 
            string temp = Path.GetTempFileName();
            WriteLine(temp);

        }

    }
    internal class FileInfoClass
    {

        internal void AllOperation()
        {
            FileInfo? file = new FileInfo(Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "Ok.txt"));
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");
            //   ⚠️ Deep Rules:
            //✔ file exist nahi → new banegi
            //✔ exist hai → overwrite
            //❗ stream close nahi ki → file lock

            try
            {

                if (file.Exists)
                {
                    WriteLine("File exists");
                    //return;
                }
                else
                {
                    WriteLine("File not exists we are creating ok.txt file");
                    using (FileStream stream = file.Create())
                    {
                        //WE CAN WRITE HERE ANY DATA IN BYTES
                        string? mydata = " Hello world this text is writing using filestream";
                        byte[] data = Encoding.UTF8.GetBytes(mydata);   //convert string in bytes
                        stream.Write(data, 0, data.Length); //write data using write method and streams
                                                            //data = bytes, 0 = starting point of data, data.length = how long data write / data.length means complete data 
                    }

                }
            }
            catch (Exception ex)
            {
                WriteLine("Error occur during file creating using FileInfo create method: " + ex.Message);
            }
            /*
             🔥 3. Delete()
             📌 Kya karta hai:
             
             👉 file permanently delete karta hai
             
             📌 Return:
             
             👉 void
             
             📌 Example:
             ⚠️ Deep Rules:
             ✔ recycle bin nahi
             ❗ file open hai → error
             ❗ permission issue → error
             */
            //file.Delete();

            void CopyToMethod()
            {
                try
                {
                    //copyto original safe overwrite option true or false
                    FileInfo fileinfo = file.CopyTo(Path.Combine(path + "okCopied.txt"), true);
                    //string rootpath = Path.GetPathRoot("okCopied.txt") ?? "null";
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }

            }
            CopyToMethod();

            void MoveToMethod()
            {
                try
                {
                    string destination = Path.Combine(path + "okCopied.txt");
                    if (!File.Exists(destination))
                    {
                        throw new Exception("File already exits");
                    }

                    //copyto original safe overwrite option true or false
                    file.MoveTo(destination);
                    WriteLine("Moving successfull");
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }

            }
            //MoveToMethod();

            //file.openRead() file ko read mode me open karta hai
            void OpenReadModeFile()
            {
                using (FileStream stream = file.OpenRead()) //file open hogi read mode me
                {
                    //create a container that's store file bytes
                    byte[] byteContiner = new byte[stream.Length];

                    //fill a continer with byte data using read method
                    int bytesread = stream.Read(byteContiner, 0, byteContiner.Length);

                    //convert bytes into string
                    string? data = null;
                    if (bytesread > 0)
                    {
                        data = Encoding.UTF8.GetString(byteContiner, 0, bytesread);
                    }
                    WriteLine("Data is reading using openRead = " + data);
                }
            }
            OpenReadModeFile();


            void WritingDataUsingOpenWriteMethod()
            {
                //target hai long file read karni hai
                FileInfo file = new FileInfo(Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "New Text Writing Document.txt"));
                try
                {
                    if (!file.Exists)
                    {

                        using (FileStream fs = file.OpenWrite()) //if file not exits create it self
                        {

                            string? data = "Hello vishal how are you what are you doing";

                            //convert data into bytes
                            byte[] byteData = Encoding.UTF8.GetBytes(data);

                            fs.Write(byteData, 0, byteData.Length);
                            WriteLine("data writing successfull at locaion " + file.FullName);
                        }

                    }
                    else
                    {
                        throw new Exception("File already exits");
                    }
                }
                catch (Exception ex)
                {
                    WriteLine("error occur during writing file using openwrite: " + ex.Message);
                }
            }
            WritingDataUsingOpenWriteMethod();

            //append text using FileInfo object class
            void AppendDataUsingAppendTextFileInfoClassMethod()
            {
                //target hai long file read karni hai
                FileInfo file = new FileInfo(Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "New Text WritingAppending Document.txt"));
                try
                {
                    if (!file.Exists)
                    {

                        using (StreamWriter sw = file.AppendText())
                        {
                            sw.WriteLine("First line");
                            sw.WriteLine("Second line");
                            sw.Write("Third line");
                            sw.WriteLine("Fourth Line");

                            WriteLine("Append Writing successfull at location: " + file.FullName);
                        }

                    }
                    else
                    {
                        throw new Exception("File already exits");
                    }
                }
                catch (Exception ex)
                {
                    WriteLine("error occur during writing file using openwrite: " + ex.Message);
                }
            }
            AppendDataUsingAppendTextFileInfoClassMethod();

            //createText method file auto create kare ge or write
            async Task CreateTextFileInfoClassMethod()
            {
                FileInfo file = new FileInfo(Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "CreateTextMethod.txt"));

                try
                {
                    if (!file.Exists)
                    {
                        // if file exists overwrite if not exists create it and write
                        using (StreamWriter sw = file.CreateText()) //here is file created
                        {
                            //writing file using stream writer
                            sw.WriteLine("This file is created using FileInfo class method createText auto create and write in just one method simplified stream writer");
                            sw.WriteLine("This is second line");
                            await sw.WriteLineAsync("This is third line");
                            WriteLine("File creation success at location: " + file.FullName);
                        }
                    }
                    else
                    {
                        throw new Exception("File already exits");
                    }
                }
                catch (Exception ex)
                {

                    WriteLine("error occured during creating file: " + ex.Message);
                }

            }
            CreateTextFileInfoClassMethod();



            //open file OpenText file INfo method
            void OpenTextFileInfoClassMethod()
            {
                FileInfo file = new FileInfo(Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "CreateTextMethod.txt"));

                try
                {
                    if (file.Exists)
                    {
                        //👉 Refresh() = file ki latest state dubara load karna
                        //refresh ka use tab karte hai jab user mannually file edit kar de system old byte size de
                        //return void
                        //file.Refresh();

                        using (StreamReader sr = file.OpenText()) //mainly text file ko read karne ka liya use hoti hai easy way ma read kar deti hai long file ko bytes ma data nahi lena padta automatic karti hai other format ma data loose ho sakte hai better hoga filestream ka use kare other type me
                        {
                            WriteLine(sr.ReadToEnd()); //reading data till end
                            //string? data = null;
                            //while((data = sr.ReadLine())!=null)
                            //{
                            //    WriteLine($"{data}");
                            //}
                            //while(!sr.EndOfStream)
                            //{
                            //    WriteLine(sr.ReadLine());
                            //}
                        }
                    }
                    else
                    {
                        throw new Exception("File does't exits");
                    }
                }
                catch (Exception ex)
                {

                    WriteLine("error occured during creating file: " + ex.Message);

                }

            }
            OpenTextFileInfoClassMethod();

            void FileStreamMethod()
            {
                string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "FileStreamDirectLowLevel.txt");

                try
                {
                    /*
                           🎯 Real Use Cases
                           Image read / write 🖼️
                           Video file handling 🎥
                           Binary files(.exe, .dat)
                           Large file processing
                           Network streams
                           🚀 Final Summary
                           FileMode method
                           | Mode         | Meaning              |
                           | ------------ | -------------------- |
                           | Open         | existing file open   |
                           | Create       | new file (overwrite) |
                           | Append       | end me data add      |
                           | OpenOrCreate | open ya create       |

                           FileAccess method
                           | Type      | Meaning    |
                           | --------- | ---------- |
                           | Read      | sirf read  |
                           | Write     | sirf write |
                           | ReadWrite | dono       |

                           ✔ FileStream = byte level control
                           ✔ Write() → data likhna
                           ✔ Read() → data padhna
                           ✔ Position → pointer
                           ✔ Seek() → jump
                           ✔ using → auto close*/

                    /*FileStream me resume Download/Upload kar sakta ha 
                    fs.Position = 5000;
                    0------ - 4999------ - END
          
                    yahan se continue */
                    
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        if (fs.Length <= 0)
                        {
                            //write data;

                            string? data = "Hello how are you this file is created using FileStream Class low level";
                            byte[] buffer = Encoding.UTF8.GetBytes(data); //convert data into bytes

                            fs.Write(buffer, 0, buffer.Length);
                            WriteLine("Writing successfull");
                        }
                        else
                        {

                            //read data
                            byte[] Readingcontainer = new byte[fs.Length]; //blank container created

                            int readingCapicty = fs.Read(Readingcontainer, 0, Readingcontainer.Length);
                            
                            WriteLine("data read by Pure FileStream Class = " + Encoding.UTF8.GetString(Readingcontainer, 0, readingCapicty));

                        }

                    }
                }
                catch (Exception ex)
                {
                    WriteLine("Error occur during File read and write using FileStream class: " + ex.Message);
                }
            }
            FileStreamMethod();
        }

    }

    /*
     Def and Rules in Environment class
     System related info aur operations ke liye use hota 
       OS info, user info, system variables, current directory, memory / processors
     */
    internal class EnvironMentClass
    {
        //basics operations
        internal void BasicOperation()
        {
            WriteLine($"Machine name: {Environment.MachineName}");
            WriteLine($"User name: {Environment.UserName}");
            WriteLine($"User Domain name: {Environment.UserDomainName}");
            WriteLine($"Current Directory name: {Environment.CurrentDirectory}");
            WriteLine($"Operating system version: {Environment.OSVersion}");
            WriteLine($"System Directory: {Environment.SystemDirectory}"); //os konsi dir me hai
            WriteLine($"Processor count: {Environment.ProcessorCount}");
            WriteLine($"is 64 bit operating system or not: {Environment.Is64BitOperatingSystem}");
            WriteLine($"Dot Net Version: {Environment.Version}");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            WriteLine(path);
            string history = Environment.GetFolderPath(Environment.SpecialFolder.History);
            string[] hislist = Directory.GetFiles(history, "*", searchOption: SearchOption.AllDirectories);
            foreach (string his in hislist)
            {
                WriteLine(his);
            }


        }

    }
    internal class DirectroyInfoClass
    {
        internal readonly static string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations");

        //DirectoryInfo create reference
        internal DirectoryInfo di = new DirectoryInfo(Path.Combine(path, "DirectoryINFO"));
        internal void CreateDirectory()
        {

            //create object Directory info;
            /*
             System.IO.DirectoryInfo is a class in the .NET Framework and .NET Core libraries that provides object-oriented
             access to directories and their subdirectories. It allows developers to create, move, enumerate, and delete
             directories while retrieving metadata such as creation time, attributes, and parent relationships.
             
             */
            if (di.Exists)
            {
                WriteLine("yes Dir already exits DirectoryInfo class smoothley ignored re creation");
            }
            else
            {
                WriteLine("A fresh dir created");
            }

            //There are no need to check directory exists or not it is automatic check itself and same directory.create method auto check if exists smoothley ignore
            di.Create();
            WriteLine("Directory created at location: " + di.FullName);
        }
        internal void DeleteDirectory()
        {
            if (!di.Exists)
            {
                WriteLine("Directory does't exists");
                return;
            }
            else
            {
                di.Delete(true);  //true means recursively deleted all data;
                WriteLine("Directory deleted at location: " + di.FullName);
            }
        }

        //if sub directory already exists it is never re create sub dir auto ignore and return directory info
        internal void CreateSubDirectory()
        {
            //formalty message check and stop method but no need
            DirectoryInfo sub = di.CreateSubdirectory("Subdir");
            WriteLine("sub directory created successfull");

            if (sub.Exists)
            {
                WriteLine($"sub directory already exists: {di.FullName}");
            }
        }

        //return fileinfo array with sub directory
        internal void GetAllDirectoryies()
        {
            DirectoryInfo[] dirs = di.GetDirectories("*", SearchOption.AllDirectories);

            if (dirs.Length > 0)
            {
                foreach (DirectoryInfo dir in dirs)
                {
                    WriteLine($"{dir.FullName}");
                }
            }
            else
            {
                WriteLine("No sub dir");
            }
        }
        internal void GetAllFiles()
        {
            FileInfo[] files = di.GetFiles("*", searchOption: SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    WriteLine($"{file.FullName}");
                }
            }
            else
            {
                WriteLine("No files");
            }
        }

        internal void MoveToDirectory()
        {
            DirectoryInfo di2 = new DirectoryInfo(Path.Combine(path, "MoveAbleDirInfo"));
            string destination = Path.Combine(path, "Paramount", "MoveAbleDirInfo");
            if (!di2.Exists)
            {
                WriteLine("source does't exits");
            }
            if (!Directory.Exists(destination))
            {
                di2.MoveTo(destination);
                WriteLine("Move successfull");
            }
            else
            {
                WriteLine("Choose diff destionation");
            }
        }

        internal void BasicOperations()
        {
            WriteLine("Parent dir: " + di.Parent?.FullName);
            WriteLine("current Dir name: " + di.Name);
            WriteLine("current Dir FullName: " + di.FullName);
            WriteLine("Creation Time: " + di.CreationTime);
            WriteLine("Last Write Time: " + di.LastWriteTime);

        }

    }
    /*
     Normaly data disk par write karte hai
     MemoryStream ma Data Ram ma store karte hai current operation speed se perporm karne ka liya
     temporary data handle karna ho
     fast processing chahiye
     file banane se pehle memory me kaam karna ho
     API / network me data bhejna ho
     
     */

    internal class MemoryStreamClass
    {
        internal void CreateMemoryStream()
        {

            //create memory stream 
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    //create byte data;
                    byte[] data = Encoding.UTF8.GetBytes("Hello this Memory Stream Data Write in Ram Memory");

                    //writing memory stream

                    ms.Write(data, 0, data.Length);
                    WriteLine("Writing Successfull");

                    // we cam't read data out of the this method scope it's not available 
                    //second method store data in hard disk after all operation

                    //we can read here first
                    ms.Position = 0; //position must be set before reading beacause writer write data using position and reader read data using position index;
                                     //ms.Seek(0, SeekOrigin.Begin); //same work as position it is provide little bit advance options as need.

                    byte[] buffer = new byte[ms.Length];

                    int byteRead = ms.Read(buffer, 0, buffer.Length);

                    string readableData = Encoding.UTF8.GetString(buffer, 0, byteRead);
                    WriteLine("data read from memory stream = " + readableData);
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occurr when creating MemoryStream: " + ex.Message);
            }
        }

        //Costrutor passing data auto write in MemoryStream

        internal void CreateMemoryStreamUsingConstructor()
        {
            //create memory stream 
            try
            {
                //create byte data;
                byte[] data = Encoding.UTF8.GetBytes("Hello this Memory Stream Data Write using constructor in Ram Memory");
                using (MemoryStream ms = new MemoryStream(data)) //writing auto in memorystream
                {

                    WriteLine("Writing Successfull");

                    // we cam't read data out of the this method scope it's not available 
                    //second method store data in hard disk after all operation

                    //we can read here first
                    ms.Position = 0; //position must be set before reading beacause writer write data using position and reader read data using position index;
                                     //ms.Seek(0, SeekOrigin.Begin); //same work as position it is provide little bit advance options as need.

                    byte[] buffer = new byte[ms.Length];

                    int byteRead = ms.Read(buffer, 0, buffer.Length);

                    string readableData = Encoding.UTF8.GetString(buffer, 0, byteRead);
                    WriteLine("data read from memory stream = " + readableData);
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occurr when creating MemoryStream: " + ex.Message);
            }
        }

        //After all operation peroform in memorystream we can store data in hard disk
        internal void StoreDataHardDiskAfterMemoryStreamOperation()
        {
            //create memory stream 
            try
            {
                //create byte data;
                byte[] data = Encoding.UTF8.GetBytes("Hello this Memory Stream Data Write using constructor in Ram Memory Also store after all operation in hard disk file for safted data not lose after destroyed memory");
                MemoryStream ms = new MemoryStream(data); //writing auto in memorystream


                WriteLine("Writing Successfull in MemoryStream");

                //Memory ---> File data
                File.WriteAllBytes(Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "MemoryStreamData", "RAM.txt"), ms.ToArray());

                WriteLine("Data store in hard disk file successfull");


                //File ---> Memory
                string mpath = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "MemoryStreamData", "RAM.txt");
                if (File.Exists(mpath))
                {
                    byte[] buffer = File.ReadAllBytes(mpath);

                    WriteLine("Data read from file to Memory stream = " + Encoding.UTF8.GetString(buffer, 0, buffer.Length));
                }
                ms.Dispose(); //realease all resources used by the stream.

            }

            catch (Exception ex)
            {
                WriteLine("Exception occurr when creating MemoryStream: " + ex.Message);
            }

        }
        internal void MemoryWriterUsingStreamReadAndWrite()
        {
            //create MemoryStream Reference

            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.WriteLine("Data write in memoryStream using Stream Writer class");
                sw.Flush(); //Now push the streamWriter data in memory stream

                ms.Seek(0, SeekOrigin.Begin); //set reading in starting index;

                using (StreamReader sr = new StreamReader(ms))
                {
                    WriteLine("Reading data using stream Reader from Memory stream = " + sr.ReadToEnd());
                }
            }

        }
    }


    //binary Reader and Binary Writer
    /*
     🔹 BinaryWriter kya hai?

        👉 BinaryWriter ek class hai jo data ko binary (byte) format me stream me likhne ke liye use hoti hai
        👉 Matlab:
           int, float, string sab ko bytes me convert karke write karta hai
        👉 BinaryWriter:
        “A class used to write primitive data types to a stream in binary format.”
        
        👉 BinaryReader:
        “A class used to read primitive data types from a stream in binary format.”
     */


    //Note Write order == Read order || jis order ma write aussi me read
    internal class BinaryReaderWriter
    {
        internal void BinaryWriterMethod()
        {
            //byte[] bit = BitConverter.GetBytes(10.56f);

            //foreach(byte b in bit)
            //{
            //    Write(b);
            //}
            //WriteLine();
            //float data = BitConverter.ToSingle(bit, 0);
            //WriteLine(data);
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "BinaryStreams", "bindata.txt");
            try
            {

                if (!File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(1052);
                        bw.Write("hello writs a length prefixed string");
                        bw.Write(true);
                        WriteLine("Binary data writing successfull");
                    }
                }
                else
                {
                    WriteLine("binary File already exits");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occur: " + ex.Message);
            }

        }

        internal void BinaryReaderMethod()
        {
            //byte[] bit = BitConverter.GetBytes(10.56f);

            //foreach(byte b in bit)
            //{
            //    Write(b);
            //}
            //WriteLine();
            //float data = BitConverter.ToSingle(bit, 0);
            //WriteLine(data);
            string path = Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "BinaryStreams", "bindata.txt");
            try
            {

                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        int num = br.ReadInt32();
                        string str = br.ReadString();
                        bool lable = br.ReadBoolean();

                        WriteLine($"Binary data Reading successfull {num} - {str} - {lable}");
                    }
                }
                else
                {
                    WriteLine("binary File not exits");
                }
            }
            catch (Exception ex)
            {
                WriteLine("Exception occur: " + ex.Message);
            }

        }
    }
}