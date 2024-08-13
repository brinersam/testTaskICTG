using IConText.App.Commands.Implementations.Args;
using IConText.Data;
using IConText.Data.Model;

namespace IConText.App.Commands.Implementations
{
    public class Command_Get : CommandBase
    {
        private IMessageDisplayer _displayer;
        private IStorage _storage;
        private bool _throwIfEmpty;
        public Command_Get(IStorage storage, IMessageDisplayer displayer, bool throwIfEmpty = false)
        {
            _displayer = displayer;
            _storage = storage;
            _throwIfEmpty = throwIfEmpty;
        }

        public override void Execute()
        {
            try
            {
                TryGetArg(out CommandArgs_Id args, false, true);

                IEnumerable<Employee> result;

                if (args == null)
                    result = _storage.Read((x) => true);
                else
                    result = _storage.Read((x) => x.Id == args.Id);

                if (_throwIfEmpty)
                {
                    if (result == null || result.Count() == 0)
                    {
                        string msg = "Get returned nothing!";

                        if (args != null)
                            msg += $"Arguments used : {args.OriginalArgs()}";

                        throw new Exception(msg);
                    }
                }
                _displayer.Display(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Get operation failed!", ex);
            }
        }
        protected override IEnumerable<CommandArgsBase> ArgObjFromParams(IEnumerable<string> args)
        {
            yield return new CommandArgs_Id(args.ToArray());
        }
    }
}

