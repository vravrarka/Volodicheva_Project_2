using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Класс отвечает за работу с файлами
    /// </summary>
    internal class SpotifyArtistProcessFile
    {
        public SpotifyArtist lineArtist; 
        /// <summary>
        /// Метод читает файл по строкам
        /// </summary>
        /// <param name="pathFile">Адрес к файлу</param>
        /// <returns>Лист с данными об артисте</returns>
        public List<SpotifyArtist> ReadSpotifyArtist(string pathFile)
        {
            List<SpotifyArtist> artist = new List<SpotifyArtist>();
            try
            {
                StreamReader sr = new StreamReader(pathFile);
                sr.ReadLine(); //Читаем заголовок отдельно

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] val = SplitMarker(line);

                    if (val.Length == 8) //Проверяем, что есть все столбцы и присваем значения каждой нужной части массива 
                    {
                        string artistName = val[1];
                        string leadStreams = val[2];
                        string feats = val[3];
                        string tracks = val[4];
                        string oneBillion = val[5];
                        string million = val[6];
                        string lastUpdeted = val[7];
                        try
                        {
                            lineArtist = new SpotifyArtist(artistName, leadStreams, feats, tracks, oneBillion, million, lastUpdeted); //Пробуем записать форматированные значеня в лист, если строка неверная, ее пропускаем (строковые значения там вместо int или иные)
                            artist.Add(lineArtist);
                        }
                        catch (ArgumentNullException ex)
                        {
                            Console.WriteLine($"Ошибка в формате строки. {ex.Message} Строка не учитывается.");
                            continue;
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Ошибка в формате строки. {ex.Message} Строка не учитывается.");
                            continue;
                        }
                        catch (OverflowException ex)
                        {
                            Console.WriteLine($"Ошибка в переполнении. {ex.Message} Строка не учитывается.");
                            throw;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка в формате строки. {ex.Message} Строка не учитывается.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Формат файла не соответствует структуре");
                        Console.WriteLine("Попробуйте снова, нажав любую клавишу");
                        break;
                    }
                }
                sr.Close(); //Закрываем поток чтения 
                return artist;
            }
            catch (ArgumentNullException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка в файле. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка в файле. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Файл не найден. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (PathTooLongException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Путь к файлу слишком длинный");
                Console.ResetColor();
                throw;
            }
            catch (NullReferenceException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникло null значение. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Файл не найден. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка доступа к файлу. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (OutOfMemoryException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Недостатоноч памяти: {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка ввода-вывода. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }
        /// <summary>
        /// Метод разделяет строку в праивильный формат
        /// </summary>
        /// <param name="line">Считанная из файла строка</param>
        /// <returns>Корректный формат строки</returns>
        static string[] SplitMarker(string line)
        {
            var result = new List<string>();
            var current = new List<char>();
            bool flag = false;

            for (int i = 0; i < line.Length; i++) //Идем по каждому знаку строки, чтобы разделить по запятым, если данные не в кавычках
            {
                char c = line[i];

                if (c == '\"') 
                {
                    flag = !flag; 
                }
                else if (c == ',' && !flag)
                {
                    result.Add(new string(current.ToArray()));
                    current.Clear();
                    continue;
                }

                if (c != '\"' || (flag && c != '\"'))
                {
                    current.Add(c);
                }
            }

            if (current.Count > 0)
            {
                result.Add(new string(current.ToArray()));
            }
            string[] strings = result.ToArray();
            for (int i = 0; i < strings.Length; i++) //Убираем запятые внутри кавычек 
            {
                if (strings[i].Contains(','))
                {
                    int num = 0;
                    int j = 1;
                    foreach (char letter in strings[i])
                    {
                        num += 1;
                        if (letter == ',' && num > 0)
                        {
                            strings[i] = strings[i].Remove(num - j, 1);
                            j++;
                        }
                    }
                }
            }
            return strings;
        }
        /// <summary>
        /// Метод записывает данные об артистах в файл
        /// </summary>
        /// <param name="artists">Лист с данными об артистах</param>
        /// <param name="pathFile">Адрес файла, который нужно создать</param>
        public void WtiteToCsv(List<SpotifyArtist> artists, string pathFile)
        {
            try
            {
                StreamWriter sw = new StreamWriter(pathFile);
                sw.WriteLine(",Artist Name,Lead Streams,Feats,Tracks,One Billion,100 Million,Last Updated"); // Записывает заголовок
                int num = 1; //Номер для обозначения порядка артиста
                foreach (SpotifyArtist artist in artists)
                {
                    sw.WriteLine($"{num},{artist.ArtistName},{artist.LeadStreams},{artist.Feats},{artist.Tracks},{artist.OneBillion},{artist.Million},{artist.LastUpdated}"); //Записываем данные по каждому артисту в строку 
                    num += 1;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Данные успешно загружены в файл {Path.GetFileName(pathFile)}!");
                Console.ResetColor();
                sw.Close(); //Закрываем поток записи 
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка доступа к файлу при записи данных в файл. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка доступа к файлу. {ex.Message}");
                Console.ResetColor();
                throw;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Возникла ошибка доступа к файлу. {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }
    
    }
}
