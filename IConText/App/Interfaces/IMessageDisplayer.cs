namespace IConText.App
{
    public interface IMessageDisplayer
    {
        void Display(IEnumerable<IFormattable> messages);
        void Display(IFormattable message);
    }
}