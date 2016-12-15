using Library.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        // get a reference to the database context which contains all information
        // about connecting to and working with the database.
        private static ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Author
        [AllowAnonymous]
        public ActionResult Index()
        {
            // create and model to pass to the view.
            // in this case the /Authors/Index view expects a collection of authors.
            var model = _context.Authors.Where(item => item.IsDeleted == false).ToList();

            //pass the model to the view like so.
            return View(model);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var model = _context.Authors.FirstOrDefault(item => item.ID == id);

            if (model == null)
            {
                model = new Author();
            }

            return View(model);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            var model = new Author();
            return View(model);
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(Author model)
        {
            try
            {
                // TODO: Add insert logic here

                // update virtual properties
                model.CreatedBy = User.Identity.Name;
                model.CreatedOn = DateTime.Now;
                model.LastModifiedBy = User.Identity.Name;
                model.LastModifiedOn = DateTime.Now;
                model.IsDeleted = false;

                // add the new author to the database.
                _context.Authors.Add(model);

                // save the changes.
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5

        public ActionResult Edit(int id)
        {
            var model = _context.Authors.FirstOrDefault(item => item.ID == id);

            if (model == null)
            {
                model = new Author();
            }

            return View(model);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var model = _context.Authors.FirstOrDefault(item => item.ID == id);

                if (model != null)
                {
                    model.FirstName = collection["FirstName"];
                    model.LastName = collection["LastName"];
                    model.LastModifiedOn = DateTime.Now;
                    model.LastModifiedBy = User.Identity.Name;

                    _context.Authors.Attach(model);
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

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _context.Authors.FirstOrDefault(item => item.ID == id);

            if (model == null)
            {
                model = new Author();
            }

            return View(model);
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete object here
                var model = _context.Authors.FirstOrDefault(item => item.ID == id);
                if (model != null)
                {

                    model.LastModifiedOn = DateTime.Now;
                    model.LastModifiedBy = User.Identity.Name;
                    model.IsDeleted = true;
                    _context.Authors.Attach(model);
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
