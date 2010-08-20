using System;
using Optional.Commands;

namespace TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
//                foreach (var option in Options.Create(args))
//                {
//                    Console.WriteLine(option);
//                }
//                Console.WriteLine(new string('-', 64));
//                Console.WriteLine();

                var factory = new CommandFactory {Default = new DefaultCommand()};
                factory.Register<FooCommand>();
                factory.Register<BarCommand>();
                factory.Register<FastCommand>();
                var command = factory.Create(args);
                command.Execute();

//                var options = new ObjectParser().Parse<TestAppOptions>(args);
//                Console.WriteLine(options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}