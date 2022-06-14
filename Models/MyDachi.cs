public class MyDachi
{
    public int Fullness {get; set;}
    public int Happiness {get; set;}
    public int Meals {get; set;}
    public int Energy {get; set;}
    public string? Status {get; set;}

    public MyDachi()
    {
        Fullness = 20;
        Happiness = 20;
        Meals = 3;
        Energy = 50;
        Status = "Hello, my name is Chopper!";
    }
}
