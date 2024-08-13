using IConText.App.Commands;
using IConText.Data;

namespace IConText.App
{
    public class AppController
    {
        private readonly IMessageDisplayer _msgDisplayer;
        private readonly IStorage _storage;
        private readonly CmdAssembler _cmdAssembler;
        private readonly Dictionary<string, CommandBase> _commands;

        public AppController(IStorage storage, CmdAssembler cmdAssembler, IMessageDisplayer msgDisplayer)
        {
            _commands = new();
            _storage = storage;
            _cmdAssembler = cmdAssembler;
            _msgDisplayer = msgDisplayer;
        }

        public void Parse(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith('-'))
                {
                    FetchAndExecuteCommandFromParser();

                    if (!_commands.ContainsKey(arg))
                        throw new KeyNotFoundException($"No such command found : |{arg}|");

                    _cmdAssembler.SetCmd(_commands[arg]);
                }
                else
                {
                    _cmdAssembler.AddArg(arg);
                }
            }
            FetchAndExecuteCommandFromParser();
            _storage.SaveChanges();
        }

        public void registerCommand(string cmd, CommandBase cmdClass)
        {
            _commands.Add(cmd, cmdClass);
        }

        private void FetchAndExecuteCommandFromParser()
        {
            if (_cmdAssembler.TryReturnCmd(out CommandBase result))
                result.Execute();
        }
    }
}