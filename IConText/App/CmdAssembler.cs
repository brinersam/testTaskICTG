using IConText.App.Commands;

namespace IConText.App
{
    public class CmdAssembler
    {
        private CommandBase _cmd;
        private List<string> _args;

        public CmdAssembler() 
        {
            _args = new();
        }

        internal void AddArg(string input)
        {
            if (_cmd == null)
                throw new Exception("Attempt at adding arguments while command not set!");

            _args.Add(input);
        }

        internal void SetCmd(CommandBase cmd)
        {
            Reset();
            _cmd = cmd;
        }

        internal bool TryReturnCmd(out CommandBase result)
        {
            result = _cmd;
            bool success = _cmd != null;

            if (success)
                _cmd.ParseArgs(_args);

            return success;
        }

        private void Reset()
        {
            _cmd?.ResetArgs();
            _cmd = null;
            _args.Clear();
        }
    }


}
