namespace Optional.Commands
{
    public interface ICommand
    {
        string Name { get; }

        IApplicationContext ApplicationContext { get; set; }

        /// <returns>The exit code for the application.</returns>
        int Execute();
    }
}