using IConText.Data;

namespace IConText.App.Commands.Implementations.Args
{
    public class CommandArgs_Entity<T> : CommandArgsBase where T: EntityBase<T>, new()
    {
        public T? Ent { get; private set; }

        public CommandArgs_Entity(string[] args) : base(args)
        {}

        protected override bool TryParseArgs(string[] args)
        {
            if (args.Length == 0)
                return false;

            Ent = EntityBase.Parse<T>(args);
            return true;
        }
    }
}

