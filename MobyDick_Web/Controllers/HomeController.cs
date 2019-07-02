using MobyDick_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MobyDick_Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/mobydickWords.xml";
            XElement xElement = null;
            //XDocument xDocument = null;
            if (HttpContext.Cache[filePath] == null)//Dosya önbellekte yok ise
            {
                string cacheFilePath = filePath;
                xElement = XElement.Load(cacheFilePath);// XML ifade eder ve xml verilerini alır
                                                        // xDocument = XDocument.Load(cacheFilePath);
                                                        //Dosyadaki herhangi bir değişiklikte, önbelleğe alınan öğeyi otomatik olarak kaldırılacak bağımlılık ilişkisini izler.
                HttpContext.Cache.Insert(filePath, xElement, new CacheDependency(cacheFilePath));// Zamana bağlıda yapılabilinir.

            }
            else
            {
                xElement = (XElement)HttpContext.Cache[filePath];//Var olan önbellek dosyasını aktarır
                                                                 // xDocument = (XDocument)HttpContext.Cache[filePath];
            }

            List<DataModel> wordList = new List<DataModel>();

            var words = (from x in xElement.Elements("Word") select x).Take(10);

            foreach (var w in words)
            {
                wordList.Add(new DataModel { word = w.Attribute("word").Value, count = w.Attribute("count").Value });
            }

            //XElement ile XDocument her ikiside xml formatından veri okur
            //Buradaki fark, XElement türünün bir XML parçasını temsil etmesidir; XDocument türü ise tüm ilişkili meta verilere sahip bir XML belgesinin tamamını temsil eder.
            /* int i = 0;
              foreach (var w in xDocument.Element("Words").Elements("Word"))
              {
                  wordList.Add(new DataModel { word = w.Attribute("word").Value, count = w.Attribute("count").Value });
                  i++;
                  if (i==10)
                  {
                      break;
                  }
              }*/

            return View(wordList);
        }
    }
}