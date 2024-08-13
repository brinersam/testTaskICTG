using IConText.App;

namespace IConText.View
{
    public class ConsoleMessageDisplayer : IMessageDisplayer
    {
        public void Display(IFormattable message)
        {
            Console.WriteLine(message.ToString());
        }

        public void Display(IEnumerable<IFormattable> messages)
        {
            foreach (var message in messages)
                Display(message);
        }
    }
}