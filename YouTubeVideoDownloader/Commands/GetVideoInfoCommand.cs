using System;
using System.Threading.Tasks;
using YouTubeVideoDownloader.Receiver;

namespace YouTubeVideoDownloader.Commands
{
    /// <summary>
    /// Команда получения информации (Commands/GetVideoInfoCommand.cs)
    /// К сожалению YouTube блокирован на территории РФ (февраль 2026)
    /// </summary>
    public class GetVideoInfoCommand : ICommand
    {
        private readonly VideoReceiver _receiver;
        private readonly string _videoUrl;

        public GetVideoInfoCommand(VideoReceiver receiver, string videoUrl)
        {
            _receiver = receiver;
            _videoUrl = videoUrl;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Выполняется команда получения информации о видео...");

            try
            {
                var videoInfo = await _receiver.GetVideoInfoAsync(_videoUrl);
                _receiver.DisplayVideoInfo(videoInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось выполнить команду: {ex.Message}");
            }
        }
    }
}