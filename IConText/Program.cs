using IConText.App;
using IConText.App.Commands.Implementations;
using IConText.Data;
using IConText.View;

namespace IConText
{
    class Program
    {
        static void Main(string[] args)
        {
            IStorage storage = new JsonStorage(".\\data");
            IMessageDisplayer messageDisplayer = new ConsoleMessageDisplayer();
            
            var controller = new AppController
                (
                    storage,
                    new CmdAssembler(),
                    messageDisplayer
                );

            AppControllerFacade.RegisterCommands(controller, storage, messageDisplayer);

            controller.Parse(args);
        }
    }

    static class AppControllerFacade
    {
        public static void RegisterCommands(AppController target, IStorage storage, IMessageDisplayer displayer)
        {
            target.registerCommand("-add", new Command_AddEntity(storage, displayer));
            target.registerCommand("-update", new Command_Update(storage, displayer));
            target.registerCommand("-delete", new Command_Delete(storage, displayer));
            target.registerCommand("-get", new Command_Get(storage, displayer));
            target.registerCommand("-getall", new Command_Get(storage, displayer, throwIfEmpty: false));
        }
    }
}
