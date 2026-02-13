using System;
using System.IO;
using System.Threading.Tasks;
using YouTubeVideoDownloader.Receiver;

namespace YouTubeVideoDownloader.Commands
{
    /// <summary>
    /// Команда скачивания видео (Commands/DownloadVideoCommand.cs)
    /// </summary>
    public class DownloadVideoCommand : ICommand
    {
        private readonly VideoReceiver _receiver;
        private readonly string _videoUrl;
        private readonly string _outputFileName;

        public DownloadVideoCommand(VideoReceiver receiver, string videoUrl, string outputFileName)
        {
            _receiver = receiver;
            _videoUrl = videoUrl;
            _outputFileName = outputFileName;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Выполняется команда скачивания видео...");

            try
            {
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), _outputFileName);
                await _receiver.DownloadVideoAsync(_videoUrl, outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось выполнить команду: {ex.Message}");
            }
        }
    }
}
