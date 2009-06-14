using Optional.Attributes;

namespace Optional.Commands
{
    public abstract class Command : ICommand
    {
        protected Command()
        {
            Name = GetType().Name.ToLower().Replace("command", "");
        }

        [Ignore]
        public string Name { get; set; }

        [Ignore]
        public IApplicationContext ApplicationContext { get; set; }

        public virtual int Execute()
        {
            return 0;
        }

        #region Utility Methods

        protected void Write(string value)
        {
            ApplicationContext.Out.Write(value);
        }

        protected void Write(string format, params object[] args)
        {
            ApplicationContext.Out.Write(format, args);
        }

        protected void WriteLine()
        {
            ApplicationContext.Out.WriteLine();
        }

        protected void WriteLine(string value)
        {
            ApplicationContext.Out.WriteLine(value);
        }

        protected void WriteLine(string format, params object[] args)
        {
            ApplicationContext.Out.WriteLine(format, args);
        }

        #endregion
    }
}