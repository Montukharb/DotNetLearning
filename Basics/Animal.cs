using System.Text;

namespace Basics.Animal
{

    internal class Animal  //base class
    {
        public string? name { get; set; }
        //public readonly string description = "dsf";
        public string? description { get; set; }

    }

    internal class Cat : Animal  //child | derived class
    {
        public int? legs { get; set; }
        public int? age { get; set; }
    }

    internal class Spider: Animal //child class
    {

        //private string? secrectName;
        //public string? spiderType { get; set { secrectName  = value ?? "default"; } }
        public string? spiderType { get; set; }


        //public void objectCreate()
        //{
        //    var catobj = new Cat();

        //    Cat catobj1 = new Cat();
        //    Animal catobj2 = new Cat();
        //}


    }


    #region operation of switch case

    internal class Operation
    {
        public int n = 0;  //field
        public int num { get; set; } //property
        
       internal Operation()
        {

            arrays();
            WriteLine("Tax result = "+TaxCalculator(1000, "IND"));
            //create a animal class array;
            var animalarr = new Animal?[] { 
            
                new Cat{ 
                    name = "sunhari Bili", 
                    age = 2,
                    description = "Lorem ipsum dollar sunhari billi very talented",
                    legs = 4
                },

                new Cat
                {
                    name = "sunhari Bili 2",
                    age = 4,
                    description = "Lorem ipsum dollar sunhari billi 3 very talented",
                    legs = 3
                },

                null,
                new Spider
                {
                    name = "zebra Spider super",
                    description = "lorem ipsum dollar zebra spider quality",
                    spiderType = "Flower bully"
                },
                new Spider
                {
                    name = "zebra Spider super pro",
                    description = "lorem ipsum dollar zebra 2 spider quality",
                    spiderType = "Flower bully 2"
                }
            }; 

             string Message;
            foreach(Animal? animal in animalarr)
            {

                switch(animal)
                {
                    case Cat c when c.age >= 2:
                        Message = $"Cat Name = {c.name}";
                        break;

                    case Cat c when c.legs >= 4:
                        Message = $"Moti meow and name is = {c.name}";
                        break;

                    case null:
                        Message = "Empty item";
                        break;
                    case Spider s when s.spiderType == "Flower bully 2":
                        Message = $"spider name = {s.name}";
                        break;
                    default:
                        Message = "other item";
                        break;
                }
                WriteLine(Message);
            }
            

        }

        internal void arrays()
        {
            string[,] multidim = new string[2,3]
            {
                {"hello","how are","ddf" },
                {"mon","dfs","sdf" }
            };

           for(int i = 0; i<multidim.GetLength(0);i++)
            {
                for(int j = 0;j<multidim.GetLength(1);j++)
                {
                    Write(multidim[i,j]+" ");
                }
                WriteLine();
            }
            WriteLine(multidim.Length);


            //jagged array





            //string[][] jagged = new string[2][];
            // jagged[0] = new string[] { "ds", "Dfs", "sdf", "df" };
            // jagged[1] = new string[] { "fds", "sdf", "sdfe" };
            string[][] jagged = new string[2][]
                {
              new string[] {"ds","Dfs","sdf","df" },
              new string[] {"fds","sdf","sdfe" },
                };

            for (int i = 0;i<jagged.Length;i++)
            {
                for(int j = 0; j < jagged[i].Length;j++)
                {
                    Write(jagged[i][j] + " ");
                }
                WriteLine();
            }

            if(jagged is [_,_])
            {

            }
        }

    
        internal static decimal TaxCalculator(decimal amount, string countryCode)
        {

            decimal rate = countryCode switch
            {
                "IND" or "USD" => 0.04M,
                "UK" or "NEP" => 0.57M,

                _ => 0.12M
            };

            return amount * rate;
        }

    }


    #endregion

    #region multidimentional array

        #endregion
}
