using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

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
                //var guid = System.Guid.NewGuid();
                //"fsdaf3532(#$@2340ds543"
                
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
                    //while (!sr.EndOfStream) { } // run till end of stream data
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
           string? path =  @"D:\Dot Net Trainning\ProjectFilesOperations\NewDir";
            DirectoryInfo info =  Directory.CreateDirectory("TestingDir");
            WriteLine("directory path " + info.FullName);
            info =  Directory.CreateDirectory(@"D:\Dot Net Trainning\ProjectFilesOperations\NewDir");
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

            if(Directory.Exists("TestingAno"))
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
                string[] files = Directory.GetFiles(dir.ToString(),"*.txt",searchOption:SearchOption.AllDirectories);
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
            source = Path.Combine("D:\\Dot Net Trainning\\ProjectFilesOperations",source);
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
                catch(Exception ex)
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
                    Directory.CreateDirectory(Path.Combine(path,"Paramount"));
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

            string name = Path.GetFileName(Path.Combine(path,"hello.txt"));
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
               using(FileStream stream = file.OpenRead()) //file open hogi read mode me
                {
                    //create a container that's store file bytes
                    byte[] byteContiner = new byte[stream.Length];
                  
                    //fill a continer with byte data using read method
                    int bytesread = stream.Read(byteContiner, 0, byteContiner.Length);

                    //convert bytes into string
                    string? data = null;
                    if(bytesread>0)
                    {
                        data = Encoding.UTF8.GetString(byteContiner, 0, bytesread);
                    }
                    WriteLine("Data is reading using openRead = "+data);
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

                        using (FileStream fs = file.OpenWrite())
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
                }catch(Exception ex)
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
            void CreateTextFileInfoClassMethod()
            {
                FileInfo file = new FileInfo(Path.Combine("D:", "Dot Net Trainning", "ProjectFilesOperations", "CreateTextMethod.txt"));

                try
                {
                    if(!file.Exists)
                    {
                        using(StreamWriter sw = file.CreateText()) //here is file created
                        {
                            //writing file using stream writer
                            sw.WriteLine("This file is created using FileInfo class method createText auto create and write in just one method simplified stream writer");
                            sw.WriteLine("This is second line");
                            WriteLine("File creation success at location: " + file.FullName);
                        }
                    }
                    else
                    {
                        throw new Exception("File already exits");
                    }
                }
                catch(Exception ex) {

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
                        using (StreamReader sr = file.OpenText())
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
        }

}
}