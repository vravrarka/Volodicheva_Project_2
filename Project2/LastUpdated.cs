using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Хранит методы для создания сводной статистики по данным о последнем изменении 
    /// </summary>
    internal class LastUpdated()
    {
        /// <summary>
        /// Метод сортирует артистов в Словаре по дате изменений 
        /// </summary>
        /// <param name="artists">Лист с данными обо всех артистах из файла</param>
        /// <returns>Сгруппированный словарь по дате изменений</returns>
        public Dictionary<DateOnly, List<SpotifyArtist>> ArtistsLastUpdated(List<SpotifyArtist> artists)
        {
            Dictionary<DateOnly, List<SpotifyArtist>> groupLastUpdated = new Dictionary<DateOnly, List<SpotifyArtist>>();
            foreach (SpotifyArtist artist in artists)
            {
                if (!groupLastUpdated.ContainsKey(artist.LastUpdated))
                {
                    groupLastUpdated[artist.LastUpdated] = new List<SpotifyArtist>();
                }

                groupLastUpdated[artist.LastUpdated].Add(artist);
            }

            return groupLastUpdated;
        }
        /// <summary>
        /// Метод выводит данные об артистах сгруппированные по последней дате обновлений 
        /// </summary>
        /// <param name="groupLastUpdated">Сгруппированный словарь по дате изменений</param>
        public void ArtistsLastUpdatedToConsole(Dictionary<DateOnly, List<SpotifyArtist>> groupLastUpdated)
        {
            foreach (KeyValuePair<DateOnly, List<SpotifyArtist>> keyLastUpdated in groupLastUpdated)
            {
                Console.WriteLine($"Последнее изменение {keyLastUpdated.Key} произошло у:");
                foreach (SpotifyArtist artist in keyLastUpdated.Value)
                {
                    Console.WriteLine(artist);
                }
            }
        }
        /// <summary>
        /// Метод возвращает данные об артистах сгруппированные по последней дате обновлений 
        /// </summary>
        /// <param name="groupLastUpdated">Сгруппированный словарь по дате изменений</param>
        /// <returns>Лист с группированный по дате изменений артистами</returns>
        public List<SpotifyArtist> ArtistsLastUpdatedToReturn(Dictionary<DateOnly, List<SpotifyArtist>> groupLastUpdated)
        {
            List<SpotifyArtist> listLastUpdatdToReturn = new List<SpotifyArtist>();
            foreach (KeyValuePair<DateOnly, List<SpotifyArtist>> keyLastUpdated in groupLastUpdated)
            {
                foreach (SpotifyArtist artist in keyLastUpdated.Value)
                {
                    listLastUpdatdToReturn.Add(artist);
                }
            }
            return listLastUpdatdToReturn;
        }
        /// <summary>
        /// Метод возвращает словарь с отобранными артистами сгруппированный по дате изменений
        /// </summary>
        /// <param name="artists">Лист с данными обо всех артистах из файла</param>
        /// <returns></returns>
        public Dictionary<DateOnly, List<SpotifyArtist>> SortedTracksAndMonth(List<SpotifyArtist> artists)
        {
            List <SpotifyArtist> list = new List<SpotifyArtist>();
            foreach (SpotifyArtist artist in artists) //Сохраняет в новый лист артистов, которые подходят по критериям (больше 10 треков, их прослушивали в июде и марте)
            {
                if (artist.Tracks > 10 && (artist.LastUpdated.Month == (07) ||  artist.LastUpdated.Month == (08)))
                {
                    list.Add(artist);
                }
            }
            Dictionary<DateOnly, List<SpotifyArtist>> groupedList = ArtistsLastUpdated(list); //Группирует артистов по дате изменений 
            foreach (var artist in groupedList)
            {
                artist.Value.Sort((a, b) => string.Compare(a.ArtistName, b.ArtistName, StringComparison.Ordinal)); //Сортирует артистов в группах по алфавиту 
            }
            return groupedList;
        }
    }

}
