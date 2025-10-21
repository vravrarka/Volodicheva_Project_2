using Project2;
using System;
using System.Globalization;
using System.Net.NetworkInformation;
/*Володичева Варвара Андреевна 
БПИ-247
Варинат-7*/

/// <summary>
/// Класс способствует взаимодействию с консолью
/// </summary>
static class ConsoleMenu
{
    /// <summary>
    /// Поддерживает запуск корректных методов, вывод результатов на консоль и получение информации от пользователя
    /// </summary>
    static void Main()
    {
        ConsoleKeyInfo keyToExit; //Позволяет выйти из программы 
        do
        {
            try
            {
                //Получает данные о файле от пользователя 
                Console.WriteLine("\nВведите адрес файла: ");
                string filePath = Console.ReadLine(); 
                SpotifyArtistProcessFile reader = new SpotifyArtistProcessFile();
                List<SpotifyArtist> artists = reader.ReadSpotifyArtist(filePath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Данные успешно загружены!");
                Console.ResetColor();
                //Реализует меню 
                while (true)
                {
                    Menu1();
                    var key = Console.ReadLine();
                    switch (key)
                    {
                        case "1":
                            OneBillion oneBillion = new OneBillion();
                            oneBillion.ArtistsMoreOneBillion(artists);
                            break;
                        case "2":
                            CountTracks countTracks = new CountTracks();
                            countTracks.ArtistsTracks(artists);
                            break;
                        case "3":
                            SpotifyData data = new SpotifyData(artists);
                            data.SpotifyDataShow();
                            break;
                        case "4":
                            Console.WriteLine("\nВведите путь к файлу: ");
                            filePath = Console.ReadLine();
                            reader = new SpotifyArtistProcessFile();
                            artists = reader.ReadSpotifyArtist(filePath);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Данные успешно загружены!");
                            Console.ResetColor();
                            break;
                        case "5":
                            Console.WriteLine("Программа завершена.");
                            return;
                        case "6":
                            try
                            {
                                LastUpdated artistLastUpdated = new LastUpdated();
                                artistLastUpdated.ArtistsLastUpdatedToConsole(artistLastUpdated.ArtistsLastUpdated(artists));
                                Console.WriteLine("Введите название файла, в который сохранится выборка:");
                                string path = Console.ReadLine();
                                List<SpotifyArtist> artistList = artistLastUpdated.ArtistsLastUpdatedToReturn(artistLastUpdated.ArtistsLastUpdated(artists));
                                SpotifyArtistProcessFile writer = new SpotifyArtistProcessFile();
                                writer.WtiteToCsv(artistList, $@"../../../{path}.csv");
                            }
                            catch (FileNotFoundException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Возникла ошибка доступа к файлу при записи данных в файл. {ex.Message}");
                                Console.ResetColor();
                            }
                            catch (PathTooLongException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Возникла ошибка названия файла. {ex.Message}");
                                Console.ResetColor();
                            }
                            catch (ArgumentException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Возникла ошибка: {ex.Message}");
                                Console.ResetColor();
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Возникла ошибка доступа к файлу. {ex.Message}");
                                Console.ResetColor();
                            }
                            catch (DirectoryNotFoundException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Возникла ошибка доступа к файлу. {ex.Message}");
                                Console.ResetColor();
                                break;
                            }
                            break;
                        case "7":
                            LastUpdated lastUpdated = new LastUpdated();
                            lastUpdated.ArtistsLastUpdatedToConsole(lastUpdated.SortedTracksAndMonth(artists));
                            string pathF = "Meed-artist";
                            List<SpotifyArtist> artistsList1 = lastUpdated.ArtistsLastUpdatedToReturn(lastUpdated.SortedTracksAndMonth(artists));
                            SpotifyArtistProcessFile writer1 = new SpotifyArtistProcessFile();
                            writer1.WtiteToCsv(artistsList1, $@"../../../{pathF}.csv");
                            break;
                        default: //Выводится при некорректном использовании мен.
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Некорректный выбор, попрбуйте снова"); 
                                Console.ResetColor();
                                break;
                            }
                    }

                }
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Данные некорректны");
                Console.WriteLine("Попробуйте снова, нажав любую клавишу");
                Console.ResetColor();

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка доступа к файлу. {ex.Message}");
                Console.ResetColor();
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Данные некорректны");
                Console.WriteLine("Попробуйте снова, нажав любую клавишу");
                Console.ResetColor();
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
                Console.ResetColor();
                break;
            }
            keyToExit = Console.ReadKey();
        }
        while (keyToExit.Key != ConsoleKey.Escape);

    }
    /// <summary>
    /// Меню для главного экрана 
    /// </summary>
    private static void Menu1()
    {
        Console.WriteLine("Меню:");
        Console.WriteLine("1. Данные обо всех артистах, достигших миллиарда прослушиваний.");
        Console.WriteLine("2. Выборка артистов, имеющих количество треков менее 100.");
        Console.WriteLine("3. Сводная статистика по данным из файла.");
        Console.WriteLine("4. Изменить набор данных.");
        Console.WriteLine("5. Выйти из программы.");
        Console.WriteLine("6. Выборка артистов, сгруппированных по дню изменения.");
        Console.WriteLine("7. Выборку артистов, у которых больше десяти треков и которых прослушивали в июле и августе.");
    }

}
