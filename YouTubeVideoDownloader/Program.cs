using System;
using System.Threading.Tasks;
using YouTubeVideoDownloader.Commands;
using YouTubeVideoDownloader.Invoker;
using YouTubeVideoDownloader.Receiver;

namespace YouTubeVideoDownloader
{
    /// <summary>
    /// Главный файл программы (Program.cs)
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== YouTube Video Downloader ===\n");
            Console.WriteLine("Это приложение позволяет получить информацию о YouTube видео и скачать его.");

            while (true)
            {
                try
                {
                    Console.Write("\nВведите ссылку на YouTube видео (или 'exit' для выхода): ");
                    string videoUrl = Console.ReadLine();

                    if (videoUrl?.ToLower() == "exit")
                        break;

                    if (string.IsNullOrWhiteSpace(videoUrl))
                    {
                        Console.WriteLine("Ссылка не может быть пустой!");
                        continue;
                    }

                    // Создаем получателя
                    var receiver = new VideoReceiver();

                    // Создаем инвокер
                    var invoker = new CommandInvoker();

                    // Получаем информацию о видео для создания имени файла
                    var videoInfo = await receiver.GetVideoInfoAsync(videoUrl);
                    receiver.DisplayVideoInfo(videoInfo);

                    // Генерируем имя файла на основе названия видео
                    string safeFileName = GetSafeFileName(videoInfo.Title) + ".mp4";

                    Console.Write("Хотите скачать это видео? (y/n): ");
                    string answer = Console.ReadLine()?.ToLower();

                    if (answer == "y" || answer == "yes")
                    {
                        // Создаем команду получения информации
                        var getInfoCommand = new GetVideoInfoCommand(receiver, videoUrl);

                        // Создаем команду скачивания
                        var downloadCommand = new DownloadVideoCommand(receiver, videoUrl, safeFileName);

                        // Добавляем команды в инвокер
                        invoker.AddCommand(getInfoCommand);
                        invoker.AddCommand(downloadCommand);

                        // Выполняем все команды
                        await invoker.ExecuteAllAsync();

                        Console.WriteLine($"\nВидео сохранено как: {safeFileName}");
                    }
                    else
                    {
                        Console.WriteLine("Скачивание отменено.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Программа завершена. До свидания!");
        }

        private static string GetSafeFileName(string fileName)
        {
            // Удаляем недопустимые символы из имени файла
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }

            // Ограничиваем длину имени файла
            if (fileName.Length > 50)
                fileName = fileName.Substring(0, 50);

            return fileName.Trim();
        }
    }
}
