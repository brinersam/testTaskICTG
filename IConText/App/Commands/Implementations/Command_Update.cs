
using IConText.App.Commands.Implementations.Args;
using IConText.Data;
using IConText.Data.Model;

namespace IConText.App.Commands.Implementations
{
    internal class Command_Update : CommandBase
    {
        private IMessageDisplayer _displayer;
        private IStorage _storage;
        public Command_Update(IStorage storage, IMessageDisplayer displayer)
        {
            _displayer = displayer;
            _storage = storage;
        }

        public override void Execute()
        {
            try
            {
                TryGetArg(out CommandArgs_Entity<Employee> argsEntity, true, true);
                TryGetArg(out CommandArgs_Id argsId, true, true);

                _storage.Update(argsEntity.Ent, (x) => x.Id == argsId.Id);

                _displayer.Display($"Entry with id {argsId.Id} successfuly updated!");
            }
            catch (Exception ex)
            {
                throw new Exception("Update operation failed!", ex);
            }
        }

        protected override IEnumerable<CommandArgsBase> ArgObjFromParams(IEnumerable<string> args)
        {
            yield return new CommandArgs_Entity<Employee>(args.ToArray());
            yield return new CommandArgs_Id(args.ToArray());
        }
    }

}
