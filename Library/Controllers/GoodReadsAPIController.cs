using Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;

namespace Library.Controllers
{
    public class GoodReadsAPIController : Controller
    {
        private const string key = "8HbMIGP0GnHlxaj47M5c8Q";
        private const string secret = "VhVDqLudJh9WxCYjvf0PBQU51yAS4vRmD7NXJ9Z3qRw";

        //public async Task<JsonResult> GetBook(string title)
        public async Task<List<GRBook>> GetBook(string title)
        {
            // HTML enconde the book title so that special characters are converted.
            var bookTitle = HttpUtility.HtmlEncode(title);

            // create an HTTP Client to make the web call.
            var client = new HttpClient();

            // make an async web call to the goodreads api passing in the book title and get the results.
            var response = client.GetAsync("https://www.goodreads.com/search.xml?key=" + key + "&q=" + bookTitle);

            // read the result returned into a variable as a string.
            var result = await response.Result.Content.ReadAsStringAsync();

            // To convert an XML node contained in string xml into a JSON string   
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            var booksnode = doc.SelectSingleNode("//Segment[@Name='search']");
            var finalnode = booksnode.SelectSingleNode("//Segment[@Name='results']");
            string jsonText = JsonConvert.SerializeXmlNode(doc["search"]["results"]);

            List<GRBook> GRBooks = JsonConvert.DeserializeObject<List<GRBook>>(jsonText);

            var stop = "here";
            // return the json object
            //return Json(jsonText);
            return GRBooks;
        }

    }
}
