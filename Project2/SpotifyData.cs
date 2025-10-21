using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Хранит методы для создания сводной статистики по данными
    /// </summary>
    /// <param name="artists">Данные обо всех артистах из файла</param>
    internal class SpotifyData(List<SpotifyArtist> artists)
    {
        /// <summary>
        /// Считает общее количество всех артистов и выводит в консоль
        /// </summary>
        public void CountArtists()
        {
            int num = 0;
            foreach (var artist in artists)
            {

                num += 1;
            }
            Console.WriteLine($"Количество артистов: {num}");
        }
        /// <summary>
        /// Выбирает самого прослушиваемого и менее прослушиваемого артиста и выводит на экран
        /// </summary>
        public void MostAndLeastListening()
        {
            List<SpotifyArtist> mostListening = new List<SpotifyArtist>(); //Создаем лист, если вдруг у более чем одного артиста будет одинаковое количество 
            List<SpotifyArtist> leastListening = new List<SpotifyArtist>();
            long most = 0;
            long least = 10000;
            foreach (var artist in artists)
            {
                if (artist.LeadStreams > most) 
                {
                    most = artist.LeadStreams;
                    mostListening = new List<SpotifyArtist> { artist };
                }
                else if (artist.LeadStreams == most )
                {
                    most = artist.LeadStreams;
                    mostListening.Add( artist );
                }

                if (artist.LeadStreams > least)
                {
                    least = artist.LeadStreams;
                    leastListening = new List<SpotifyArtist> { artist };
                }
                else if (artist.LeadStreams == least)
                {
                    least = artist.LeadStreams;
                    leastListening.Add(artist);
                }
            }
            Console.WriteLine($"Самый прослушиваемый (-ые) артист (-ы):");
            foreach (var artist in mostListening)
            {
                Console.WriteLine(artist);
            }
            Console.WriteLine($"Наименее прослушиваемый (-ые) артист (-ы):");
            foreach (var artist in leastListening)
            {
                Console.WriteLine(artist);
            }
        }
        /// <summary>
        /// Считаем количество артистов,у которых есть цифры в имени и выводим на экран 
        /// </summary>
        public void NumbersInName()
        {
            int numbersInName = 0;
            foreach ( var artist in artists)
            {
                foreach (char c in artist.ArtistName)
                {
                    if (char.IsDigit(c))
                    {
                        numbersInName++;
                    }
                }
            }
            Console.WriteLine($"Количество исполнителей, имеющих в названии цифры: {numbersInName}");
        }
        /// <summary>
        /// Считаем общее количество исолнителей 100-миллиоников и выводим на экран
        /// </summary>
        public void CountMillionArtist()
        {
            int numbersOfMillion = 0;
            foreach ( var artist in artists )
            {
                if (artist.Million > 0)
                {
                    numbersOfMillion++;
                }
            }
            Console.WriteLine($"Общее количество исполнителей 100-миллионников: {numbersOfMillion}");
        }
        /// <summary>
        /// Считаем общее количество артистов миллиардников, менее чем с 150 треками и выводим на экран количество 
        /// </summary>
        public void CountBillionArtistWithTracks()
        {
            int numbersOfMillion = 0;
            foreach (var artist in artists)
            {
                if (artist.OneBillion > 0 & artist.Tracks < 150)
                {
                    numbersOfMillion++;
                }
            }
            Console.WriteLine($"Общее количество исполнителей миллиардников, менее чем с 150 треками: {numbersOfMillion}");
        }
        /// <summary>
        /// Дополнительное меню, которое открывает варианты для сводной статистики 
        /// </summary>
        public void SpotifyDataShow()
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Количество артистов.");
                Console.WriteLine("2. Данные о самом прослушиваемом артисто и наименее прослушиваемом артисте.");
                Console.WriteLine("3. Количество исполнителей, имеющих в названии цифры.");
                Console.WriteLine("4. Общее количество исполнителей 100-миллионников с любым количеством треков.");
                Console.WriteLine("5. Количество исполнителей миллиардников, менее чем с 150 треками. ");
                Console.WriteLine("6. Выйти");
                var key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        CountArtists();
                        break;
                    case "2":
                        MostAndLeastListening();
                        break;
                    case "3":
                        NumbersInName();
                        break;
                    case "4":
                        CountMillionArtist();
                        break;
                    case "5":
                        CountBillionArtistWithTracks();
                        break;
                    case "6":
                        Console.WriteLine("Программа завершена.");
                        return;
                    default:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Некорректный выбор, попрбуйте снова");
                            Console.ResetColor();
                            break;
                        }
                }
            }

        }
    }
}
