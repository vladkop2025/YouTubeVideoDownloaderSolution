using System.Threading.Tasks;

namespace YouTubeVideoDownloader.Commands
{
    /// <summary>
    /// Интерфейс команды (Commands/ICommand.cs)
    /// </summary>
    public interface ICommand
    {
        Task ExecuteAsync();
    }
}