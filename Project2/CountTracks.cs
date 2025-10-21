using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Сохранять в файл Artists.csv выборку обо всех артистах, имеющих количество треков(Tracks) менее 100
    /// </summary>
    internal class CountTracks()
    {
        /// <summary>
        /// Сохранять в файл Artists.csv выборку обо всех артистах, имеющих количество треков(Tracks) менее 100
        /// </summary>
        /// <param name="artists">Лист с данными об артистах из файла</param>
        public void ArtistsTracks(List<SpotifyArtist> artists)
        {
            List<SpotifyArtist> artistsTracks = new List<SpotifyArtist>();
            foreach (var artist in artists)
            {
                if (artist.Tracks < 100) //Проверяем количество треков у артиста и добавляем в лист, если подходит
                {
                    artistsTracks.Add(artist); 
                }
            }
            SpotifyArtistProcessFile writer = new SpotifyArtistProcessFile(); 
            writer.WtiteToCsv(artistsTracks, @"../../../Artists.csv"); //Записываем в файл
        }
    }
}
