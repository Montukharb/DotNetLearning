namespace EmptyProjectTesting.State_Configuration
{
    public class State
    {
        //this flag decides whether the loop will run or not
        //it is mandatory to register this as AppSingletone
        public bool IsEnabled { get; set; } = false;
    }
}
