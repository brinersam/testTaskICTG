using System.Text;

namespace IConText.App.Commands
{
    public abstract class CommandArgsBase
    {
        private readonly string[] _originalArguments;
        public readonly bool IsValid;
        protected CommandArgsBase(string[] args)
        {
            _originalArguments = args;
            IsValid = TryParseArgs(args);
        }

        public string OriginalArgs()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach (var arg in _originalArguments)
            {
                sb.Append("[");
                sb.Append(arg);
                sb.Append("]");
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
        protected abstract bool TryParseArgs(string[] args);
    }
}
