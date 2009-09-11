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
//                foreach (var option in new NameValueParser().Parse(args))
//                {
//                    Console.WriteLine("{0}={1}", option.Key, option.Value);
//                }
//                Console.WriteLine(new string('-', 64));
//                Console.WriteLine();

                var factory = new CommandFactory {Default = new DefaultCommand()};
                factory.Register<FooCommand>();
                factory.Register<FastCommand>();
                var command = factory.Create(args);
                command.Execute();

//                var options = new Parser().Parse<Options>(args);
//                Console.WriteLine(options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}