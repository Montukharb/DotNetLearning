

namespace Basics.OOPS
{
    internal class DeConstructor
    {
        //create property with get and set;
        internal int customerId { get; set; }
        internal string? customerName { get; set; }

        internal string? address { get; init; }
        internal string? Address2 { get; init; }

        internal DeConstructor()
        {
            Address2 = "Bengal ABC";
        }
      
        internal void settingValue()
        {
            //address = "sdfs"; //can't assign any value in address property because its init property;
            //Address2 = "sdafa";
            WriteLine(address);  //readonly access
        }
        internal void Deconstruct(out int CUSTOMERID, out string? CUSTOMERNAME)
        {
            CUSTOMERID = customerId;
            CUSTOMERNAME = customerName;
            settingValue();
        }

    }
}
