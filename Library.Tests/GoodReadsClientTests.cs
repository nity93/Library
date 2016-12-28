using GoodReads.API;
using GoodReads.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Library.Tests
{
    [TestClass]
    public class GoodReadsClientTests
    {
        private const string configDataLocation = "App_Data/authConfig.json";
        private AuthenticationToken _token;

        public GoodReadsClientTests()
        {
            // read authentication information from configuration file.
            _token = JsonConvert.DeserializeObject<AuthenticationToken>(File.ReadAllText(configDataLocation));
        }
        [TestMethod]
        public async Task Can_Retrieve_Book_By_Api_Search()
        {

            List<Work> books = await GoodReadsApiInterface.GetBookAsync("Hobbit", _token);

            Assert.IsTrue(books.Count > 0);
        }
    }
}
