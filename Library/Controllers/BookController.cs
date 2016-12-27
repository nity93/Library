using GoodReads.API;
using GoodReads.API.Models;
using Library.Models;
using Library.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        // get a reference to the database context which contains all information
        // about connecting to and working with the database.
        private static ApplicationDbContext _context = new ApplicationDbContext();
        private const string configDataLocation = @"~/Configuration/authConfig.json";

        private AuthenticationToken _token;

       // GET: Book/Index/The Hobbit
        public async Task<ActionResult> Index(string title)
        {
            var works = new List<Work>();
            var books = new List<Library.Models.Book>();


            // if a search, search goodreads api and local repository.
            // otherwise simply return local repository.
            if (!string.IsNullOrEmpty(title))
            {
                // read authentication information from configuration file.
                _token = JsonConvert.DeserializeObject<AuthenticationToken>(System.IO.File.ReadAllText(Server.MapPath(configDataLocation)));

                works = await GoodReadsApiInterface.GetBookAsync(title, _token);
                books = _context.Books.Where(item => item.Title.Contains(title)).ToList();
            }
            else
            {
                // get local library.
                books = _context.Books.Where(item => item.IsDeleted == false).ToList();
            }

            var viewModel = new BookViewModel
            {
                Works = works,
                Books = books
            };

            return View(viewModel);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var model = _context.Books.FirstOrDefault(item => item.ID == id);

            if (model == null)
            {
                model = new Library.Models.Book();
            }

            return View(model);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var model = new BookViewModel();
            model.Authors = _context.Authors.ToList();
            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(BookViewModel model)
        {
            Library.Models.Book existingBook = null;
            try
            {
                existingBook = _context.Books.FirstOrDefault(item => item.ISBN.Equals(model.Book.ISBN)); 
                if(existingBook != null)
                {
                    ModelState.AddModelError("ISBN", "A book by this ISBN already exists.");
                }
            } catch
            {
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here

                    // update virtual properties
                    model.Book.CreatedBy = User.Identity.Name;
                    model.Book.CreatedOn = DateTime.Now;
                    model.Book.LastModifiedBy = User.Identity.Name;
                    model.Book.LastModifiedOn = DateTime.Now;
                    model.Book.IsDeleted = false;

                    // add the new author to the database.
                    _context.Books.Add(model.Book);

                    // save the changes.
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    model.Authors = _context.Authors.ToList();
                    return View(model);
                }
            }
            model.Authors = _context.Authors.ToList();
            return View(model);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new BookViewModel();
            model.Book = _context.Books.FirstOrDefault(item => item.ID == id);
            model.Authors = _context.Authors.ToList();
            return View(model);
         } 

        // POST: Book/Edit/5
        [HttpPost]

        public ActionResult Edit(BookViewModel model)
        {

            try
            {
                // TODO: Add update logic here
             
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        model.Book.LastModifiedOn = DateTime.Now;
                        model.Book.LastModifiedBy = User.Identity.Name;
                        var book = model.Book;

                        //_context.Books.Attach(book);
                        _context.Set<Library.Models.Book>().AddOrUpdate(book);
                       //_context.Entry(book).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
               

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
               throw;
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _context.Books.FirstOrDefault(item => item.ID == id);

            if (model == null)
            {
                model = new Library.Models.Book();
            }

            return View(model);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var model = _context.Books.FirstOrDefault(item => item.ID == id);
                if (model != null)
                {
                    
                    model.LastModifiedOn = DateTime.Now;
                    model.LastModifiedBy = User.Identity.Name;
                    model.IsDeleted = true;
                    _context.Books.Attach(model);
                    _context.Entry(model).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
