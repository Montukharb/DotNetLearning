
public class Program : Fruit
{

    public static void Main(string[] args)
    {
        WriteLine("LinQ test main method run ");
        Fruit fi = new Fruit();
        fi.DisplayFruit();
        LanguageIntegratedQuery query = new LanguageIntegratedQuery();
        LanguageIntegratedQuery2 query2 = new();
        query2.LinqGroupJoin();
        query2.LinqGroupJoinQuery();
        
    }
}