using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;
namespace MobyDickWordCounts
{
    class Program
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string savePath = path + "/mobydickWords.xml";
        //  private static string url = "http://www.gutenberg.org/files/2701/2701.txt";
        private static string url = "http://www.gutenberg.org/files/2701/2701-0.txt";
        private static string contents = "";
        static void Main(string[] args)
        {
            //Kullanıcıdan alınan url ile  dosya indirme işlemini yapar
            /* try
             {
                 FileDownload.getInformation();
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Dosya indirmede hata oluştu" + ex.Message);
             }
             */
            try
            {
                Console.WriteLine("Url'den dosya okunuyor. Lütfen bekleyiniz.");
                using (var webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    contents = webClient.DownloadString(url);
                }

                contents = Regex.Replace(contents, @"(\p{P})", "");
                Console.WriteLine("Metin içerisinden kelimeler ayrıştırılıp sayılıyor. Lütfen bekleyiniz.");
                var sequentialWords = contents
                    .Split(' ')
                    .GroupBy(word => word)//kelimeler alfabetik sıralanıyor
                    .Select(word => new
                    {
                        Word = word.Key,
                        Count = word.Count()

                    }).OrderByDescending(word => word.Count).ToList();// liste kelimelerin sayısına göre büyükten küçüge sıralanıyor.

                // Eğer dosya önceden yaratılmış ise silinir
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                XDocument document = new XDocument();
                XElement xElement = new XElement("Words");
                sequentialWords.ForEach(x =>
                {
                    var word = new XElement("Word",
                        new XAttribute("word", x.Word),
                        new XAttribute("count", x.Count));
                    xElement.Add(word);

                });
                document.Add(xElement);
                document.Save(savePath);
                Console.WriteLine("Moby Dick kelimeleri başarılı bir şekilde masaüstü dizinine kaydedildi.");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Moby Dick icin XMl olusturmada hata olustu. Hata:" + ex.Message);
            }


        }
    }
}
