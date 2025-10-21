using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Выводит на экран из набора исходных данных информацию обо всех артиста, 
    /// достигших миллиарда прослушиваний хотя бы один раз
    /// </summary>
    internal class OneBillion()
    {
        /// <summary>
        /// Выводит на экран из набора исходных данных информацию обо всех артиста, 
        /// достигших миллиарда прослушиваний хотя бы один раз
        /// </summary>
        /// <param name="artists">Данные об артистах из файла</param>
        public void ArtistsMoreOneBillion(List<SpotifyArtist> artists)
        {
            List<SpotifyArtist> artistsMoreOneBillion = new List<SpotifyArtist>();
            foreach (var artist in artists)
            {
                if (artist.OneBillion > 0) //Проверяет, что у артиста больше миллиарда прослушиваний и добавляет в новый список 
                {
                    artistsMoreOneBillion.Add(artist);
                }
            }
            Console.WriteLine($"Артисты, достигшие прослушивания хотя бы один раз:");
            foreach (var artist in artistsMoreOneBillion)
            {
                Console.WriteLine(artist); //вывод на экран
            }
        }
    }
}
