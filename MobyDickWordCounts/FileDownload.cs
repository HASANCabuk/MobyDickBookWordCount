using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace MobyDickWordCounts
{
    class FileDownload
    {

        public static void getInformation()
        {
            Console.WriteLine("Lütfen İndirmek istediğiniz dosyanın URL adresini doğru bir şekilde yazınız");
            string url = Console.ReadLine();
            Regex r = new Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
            try
            {
                if (r.IsMatch(url))              
                fileDownload(url, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "AA");
                else
                    getInformation();
            }
            catch (Exception)
            {
                throw;
            }

        }
        private static void fileDownload(string url, string directoryTo, string fileName)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(new Uri(url), directoryTo + "/" + fileName + ".txt");
            webClient.DownloadProgressChanged += progressChanged;    
            webClient.DownloadFileCompleted += completed;

        }
        private static void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("Dosya indiriliyor: %{0}", e.ProgressPercentage);
            Console.Read();
        }
        private static void completed(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Dosya indirme tamamlandı.");
            Console.Read();
        }


    }
}
