using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Хранит и форматирует переменные столбцов 
    /// </summary>
    public class SpotifyArtist
    {
        public string ArtistName { get; set; } 
        public Int64 LeadStreams { get; set; }
        public Int64 Feats { get; set; }
        public int Tracks { get; set; }
        public int OneBillion { get; set; }
        public int Million { get; set; }
        public DateOnly LastUpdated { get; set; }
        /// <summary>
        /// Метод, который форматирует значения 
        /// </summary>
        /// <param name="artistName">ArtistName</param>
        /// <param name="leadStreams">LeadStreams</param>
        /// <param name="feats">Feats</param>
        /// <param name="tracks">Tracks</param>
        /// <param name="oneBillion">OneBillion</param>
        /// <param name="million">Million</param>
        /// <param name="lastUpdated">LastUpdated</param>
        public SpotifyArtist(string artistName, string leadStreams, string feats, string tracks, string oneBillion, string million, string lastUpdated)
        {
            ArtistName = artistName;
            LeadStreams = Int64.Parse(leadStreams);
            Feats = Int64.Parse(feats);
            Tracks = int.Parse(tracks);
            OneBillion = int.Parse(oneBillion);
            Million = int.Parse(million);
            LastUpdated = DateOnly.Parse(lastUpdated);
        }
        ~SpotifyArtist() { } 

        /// <summary>
        /// Форматирует данные в нужный формат
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Artist Name: {ArtistName}, Lead Streams: {LeadStreams}, Feats: {Feats}, Tracks: {Tracks}, One Billion: {OneBillion}, 100 Million: {Million}, Last Updated: {LastUpdated}";
        }
        
    }
}
