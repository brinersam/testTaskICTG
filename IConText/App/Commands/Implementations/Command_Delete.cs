
using IConText.App.Commands.Implementations.Args;
using IConText.Data;

namespace IConText.App.Commands.Implementations
{
    internal class Command_Delete : CommandBase
    {
        private IMessageDisplayer _displayer;
        private IStorage _storage;
        public Command_Delete(IStorage storage, IMessageDisplayer displayer)
        {
            _displayer = displayer;
            _storage = storage;
        }
        public override void Execute()
        {
            try
            {
                TryGetArg(out CommandArgs_Id arg, true, true);
                IEnumerable<IFormattable> popped = _storage.Delete((x) => x.Id == arg.Id);

                if (popped == null || popped.Count() == 0)
                {
                    string msg = "Search returned nothing!";

                    if (arg != null)
                    {
                        msg += $" Arguments used : {arg.OriginalArgs()}";
                    }

                    throw new Exception(msg);
                }

                _displayer.Display($"Successfully deleted entries:");
                _displayer.Display(popped);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete operation failed!", ex);
            }
        }

        protected override IEnumerable<CommandArgsBase> ArgObjFromParams(IEnumerable<string> args)
        {
            yield return new CommandArgs_Id(args.ToArray());
        }
    }
}
