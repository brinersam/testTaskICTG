namespace IConText.App.Commands
{
    public abstract class CommandBase
    {
        protected Dictionary<Type, CommandArgsBase> _argObjs = new();
        public abstract void Execute();
        public void ResetArgs()
        {
            _argObjs = new();
        }
        public void ParseArgs(IEnumerable<string> args)
        {
            if (args == null || args.Count() <= 0)
                return;

            foreach (var arg in ArgObjFromParams(args))
            {
                _argObjs[arg.GetType()] = arg;
            }
        }
        protected bool TryGetArg<TArgObj>(out TArgObj argObj, bool throwOnNull, bool throwOnInvalid) where TArgObj: CommandArgsBase
        {
            bool success = false;

            argObj = null;

            if (_argObjs.ContainsKey(typeof(TArgObj)))
            {
                argObj = _argObjs[typeof(TArgObj)] as TArgObj;
                success = true;
            }

            if (throwOnNull && argObj == null)
                throw new InvalidOperationException($"Could not get argument of type {typeof(TArgObj).Name}!");

            if (throwOnInvalid && argObj != null && !argObj.IsValid)
                throw new InvalidOperationException($"Could not parse argument of type {typeof(TArgObj).Name}! Given args: {argObj.OriginalArgs()}");

            return success;
        }
        protected abstract IEnumerable<CommandArgsBase> ArgObjFromParams(IEnumerable<string> args);
    }
}

