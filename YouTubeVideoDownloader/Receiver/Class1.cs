using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Converter;

namespace YouTubeVideoDownloader.Receiver
{
    /// <summary>
    /// Получатель команды (Receiver/VideoReceiver.cs)
    /// </summary>
    public class VideoReceiver
    {
        private readonly YoutubeClient _youtubeClient;

        public VideoReceiver()
        {
            _youtubeClient = new YoutubeClient();
        }

        public async Task<Video> GetVideoInfoAsync(string videoUrl)
        {
            try
            {
                var video = await _youtubeClient.Videos.GetAsync(videoUrl);
                return video;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении информации о видео: {ex.Message}");
                throw;
            }
        }

        public async Task DownloadVideoAsync(string videoUrl, string outputFilePath)
        {
            try
            {
                Console.WriteLine("Начинаем скачивание видео...");

                // Настройка для быстрого кодирования
                var conversion = new ConversionRequestBuilder(outputFilePath)
                    .SetPreset(ConversionPreset.UltraFast)
                    .Build();

                await _youtubeClient.Videos.DownloadAsync(videoUrl, conversion);

                Console.WriteLine($"Видео успешно скачано: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при скачивании видео: {ex.Message}");
                throw;
            }
        }

        public void DisplayVideoInfo(Video video)
        {
            Console.WriteLine("\n=== ИНФОРМАЦИЯ О ВИДЕО ===");
            Console.WriteLine($"Название: {video.Title}");
            Console.WriteLine($"Автор: {video.Author}");
            Console.WriteLine($"Длительность: {video.Duration}");
            Console.WriteLine($"Описание: {video.Description}");
            Console.WriteLine("===========================\n");
        }
    }
}