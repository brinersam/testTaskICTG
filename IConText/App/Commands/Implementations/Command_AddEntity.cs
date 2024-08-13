using IConText.App.Commands.Implementations.Args;
using IConText.Data;
using IConText.Data.Model;

namespace IConText.App.Commands.Implementations
{
    public class Command_AddEntity : CommandBase
    {
        private readonly IStorage _storage;
        private readonly IMessageDisplayer _displayer;
        public Command_AddEntity(IStorage storage, IMessageDisplayer displayer)
        {
            _displayer = displayer;
            _storage = storage;
        }

        public override void Execute()
        {
            try
            {
                TryGetArg(out CommandArgs_Entity<Employee> args, true, true);
                _storage.Create(args.Ent);
                _displayer.Display($"Added new {typeof(Employee).Name}: {args.Ent}");
            }
            catch (Exception ex)
            {
                throw new Exception("Add entity operation failed!", ex);
            }
        }

        protected override IEnumerable<CommandArgsBase> ArgObjFromParams(IEnumerable<string> args)
        {
            yield return new CommandArgs_Entity<Employee>(args.ToArray());
        }
    }
}

