using GoodReads.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace GoodReads.API
{
    internal class GoodReadsApiClient : IDisposable
    {
        private HttpClient _client = new HttpClient();
        private AuthenticationToken _authToken;

        public GoodReadsApiClient(AuthenticationToken authToken)
        {
            _authToken = authToken;
            _client.BaseAddress = new Uri("https://www.goodreads.com/");
        }

        public async Task<List<Work>> GetBookAsync(string title)
        {
            // HTML enconde the book title so that special characters are converted.
            var bookTitle = HttpUtility.HtmlEncode(title).Trim();

            // make an async web call to the goodreads api passing in the book title and get the results.
            var response = _client.GetAsync($"search.xml?key={_authToken.Key}&q={bookTitle}");

            // read the result returned into a variable as a string.
            var result = await response.Result.Content.ReadAsStringAsync();

            // To convert an XML node contained in string xml into a JSON string
            // create xml document object.  
            XmlDocument doc = new XmlDocument();

            // load the document with the xml results from GoodReads
            doc.LoadXml(result);

            // drill down to the nodeset that we need which identifies the books returned from the query.
            var workNodes = doc.GetElementsByTagName("work");

            // create a list of works which is what the xml results will be converted to.
            List<Work> Works = new List<Work>();

            // declare variables for xml node values.
            string bid, bcount, pubDate, numRatings, numReviews, avgRating;

            // instantiate book and author objects to hold required data.
            Book book = new Book();
            var author = new Author();

            // declare book and author nodes to be used for parsing.
            XmlNode bookNode, authorNode;

            // iterate over work nodes to and add create a new .NET object, setting values and adding it to the final list.
            foreach (XmlNode work in workNodes)
            {
                try
                {
                    // read values into respective variables. concatenate original_publication_... values into a single date string.
                    bid = work.SelectSingleNode("id").InnerText;
                    bcount = work.SelectSingleNode("books_count").InnerText;
                    pubDate = work.SelectSingleNode("original_publication_month").InnerText + "/" + work.SelectSingleNode("original_publication_day").InnerText + "/" + work.SelectSingleNode("original_publication_year").InnerText;
                    numRatings = work.SelectSingleNode("ratings_count").InnerText;
                    numReviews = work.SelectSingleNode("text_reviews_count").InnerText;
                    avgRating = work.SelectSingleNode("average_rating").InnerText;

                    // select the book node.
                    bookNode = work.SelectSingleNode("best_book");

                    // select the author node.
                    authorNode = bookNode.SelectSingleNode("author");

                    // set properties on the book object with values from the book node.
                    book.Id = bookNode.SelectSingleNode("id").InnerText;
                    book.Title = bookNode.SelectSingleNode("title").InnerText;
                    book.ImageUrl = bookNode.SelectSingleNode("image_url").InnerText;
                    book.SmallImageUrl = bookNode.SelectSingleNode("small_image_url").InnerText;

                    // set properties on the author object with values from the author node.
                    author.Id = authorNode.SelectSingleNode("id").InnerText;
                    author.Name = authorNode.SelectSingleNode("name").InnerText;

                    // set author property of the book object.
                    book.Author = author;

                    // add the work with the property values populated and the book object populated as well.
                    Works.Add(new Work { Id = bid, NumberOfBooks = bcount, OriginalPublicationDate = pubDate, NumberOfRatings= numRatings,NumberOfTextReviews = numReviews, Book = book });
                }
                catch (Exception ex)
                {

                }
            }

            return Works;
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _client.Dispose();
                }
                
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GoodReadsApiClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
