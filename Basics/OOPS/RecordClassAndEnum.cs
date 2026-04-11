

using System.Runtime.Intrinsics.X86;

namespace Basics.OOPS
{
    /*
    C# mein Record ek special type ka reference type hai (jo class ki tarah kaam karta hai) 
    lekin isse khaas taur par Data ko store karne ke liye banaya gaya hai.

    Record ki sabse bari khoobi Immutability(cheezon ka na badalna) aur Value-based Equality hai.
    */

    //internal record class Bus
    internal record Bus
    {

        public string? BusName { get; init; }
        public int? MaxSeat { get; init; }
        internal Bus(string BusName,int MaxSeat)
        {

           this.BusName = BusName;
           this.MaxSeat = MaxSeat;
            //default constructor call paramerized constructor 
        }
    }

    //internal record struct Traffic { 
    
     
    //}
    internal enum MobileCodes
    {
        KAT,  //0
        PAT, //1
        IND = +91,
        PAK = +92,
        USD = 89,
        NEP = 45,
        BHU = 46,
        AUS = 96,
        UK = 98,
        BAN,
        JP,
        MP,

    }


    [Flags] //
    public enum Permission{
        Read = 1,
        Write = 0,
        Execute = 0,
    }
    internal class RecordClassAndEnum
    {
      
        internal void Show()
        {
        Bus obj = new("Antil",60);
        Bus obj1 = new("Antil",60);
            WriteLine($"Comparing object = {obj == obj1}");
            WriteLine(obj);

         
            WriteLine("country name = " + MobileCodes.JP);
            WriteLine((int)MobileCodes.IND);
            Permission p = Permission.Read | Permission.Write;
            if ((p & Permission.Read) != 0)
            {
                WriteLine("permission Granted read allowed");
            }
            else
            {
                WriteLine("Permission Restricted can't Read");
            }
            if ((p & Permission.Write) != 0)
            {
                WriteLine("permission Granted write allowed");
            }
            else
            {
                WriteLine("Permission Restricted can't write");
            }
            if ((p & Permission.Execute) != 0)
            {
                WriteLine("permission Granted execute program");
            }
            else
            {
                WriteLine("Permission Restricted can't Execute");
            }
            
        }

       
    }
}
