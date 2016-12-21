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
using System.Xml.Linq;

namespace Library.Controllers
{
    public class GoodReadsAPIController : Controller
    {
        private const string key = "8HbMIGP0GnHlxaj47M5c8Q";
        private const string secret = "VhVDqLudJh9WxCYjvf0PBQU51yAS4vRmD7NXJ9Z3qRw";

        //public async Task<JsonResult> GetBook(string title)
        public async Task<List<Work>> GetBook(string title)
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
            var workNodes = doc.GetElementsByTagName("work");
            List<Work> GRBooks = new List<Work>();

            foreach (XmlNode work in workNodes)
            {
                try
                {
                    string bid = work.SelectSingleNode("id").InnerText;
                    string bcount = work.SelectSingleNode("books_count").InnerText;
                    GRBooks.Add(new Work { Id = bid, books_count = bcount });
                }
                catch (Exception ex)
                {

                }
            }

            return GRBooks;
        }

        public class Work
        {
            public string Id { get; set; }
            public string books_count { get; set; }
        }

    }
}
