using System.Collections.Generic;
using System.Threading.Tasks;
using YouTubeVideoDownloader.Commands;

namespace YouTubeVideoDownloader.Invoker
{
    /// <summary>
    /// Инвокер (Invoker/CommandInvoker.cs)
    /// </summary>
    public class CommandInvoker
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public async Task ExecuteAllAsync()
        {
            foreach (var command in _commands)
            {
                await command.ExecuteAsync();
            }

            _commands.Clear();
        }

        public void ClearCommands()
        {
            _commands.Clear();
        }
    }
}