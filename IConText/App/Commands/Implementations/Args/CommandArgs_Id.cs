namespace IConText.App.Commands.Implementations.Args
{
    public class CommandArgs_Id : CommandArgsBase
    {
        public int? Id { get; private set; }
        public CommandArgs_Id(string[] args) : base(args)
        {}

        protected override bool TryParseArgs(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.Split(':')[0].ToLowerInvariant() == "Id".ToLowerInvariant())
                {
                    Id = int.Parse(arg.Split(':')[1]);
                    return true;
                }
            }
            return false;
        }
    }
}

