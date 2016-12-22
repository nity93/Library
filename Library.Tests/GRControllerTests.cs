using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Controllers;
using Library.Models;
using System.Threading.Tasks;

namespace Library.Tests
{
    [TestClass]
    public class GRControllerTests
    {
        [TestMethod]
        public async Task Can_Retrieve_Book_By_Api_Search()
        {
            var grController = new GoodReadsAPIController();

             List<Work> books = await grController.GetBook("Hobbit");

            Assert.IsTrue(books.Count > 0);
        }
    }
}
