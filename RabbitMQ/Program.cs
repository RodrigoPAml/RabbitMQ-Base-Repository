using Examples;

Console.WriteLine("Put the number of the example:");
string input = Console.ReadLine();

switch(Int32.Parse(input))
{
    case 1:
        SingleConsumer.Execute();
        break;
    case 2:
        ManyConsumers.Execute();
        break;
    case 3:
        Exchange.Execute();
        break;
    case 4:
        Routing.Execute();
        break;
}