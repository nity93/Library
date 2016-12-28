using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Controllers;
using Library.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.Tests
{
    [TestClass]
    public class BookControllerTest
    {
        [TestMethod]
        public void Can_Get_Books_From_Database()
        {
            var BookController = new BookController();

            List<Book> Books = BookController.GetBooks();

            Assert.IsTrue(Books.Count > 0);
        }
    }
}
