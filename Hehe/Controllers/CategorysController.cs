using Hehe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace Hehe.Controllers
{
    public class CategorysController : Controller
    {
        CategorysDB db = new CategorysDB();
        
        public ActionResult IndexPageList(int page)
        {
            var model = from c in db.ListAll() select c;
            int pageSize = 3;
            int pageNumber = (page - 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        
     
        // GET: CategorysController
        public ActionResult Index(string SearchStr, string CurrentStr, string SortOrder, int page)
        {
            ViewBag.CurrentSort = SortOrder;
            ViewBag.CategoryName = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CategoryNote = SortOrder == "note" ? "note_desc" : "note";

            if (SearchStr != null)
            {
                page = 1;
            }
            else
            {
                SearchStr = CurrentStr;
            }
            ViewBag.CurrentStr = SearchStr;
            var model = from c in db.ListAll() select c;

            if (!String.IsNullOrEmpty(SearchStr))
            {
                model = model.Where(c => c.Category_Name.Contains(SearchStr)
                        || c.Category_Note.Contains(SearchStr));
            }
            switch(SortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending (s => s.Category_Name);
                    break;
                case "note":
                    model = model.OrderBy(s => s.Category_Note);
                    break;
                case "note_desc":
                    model = model.OrderByDescending(s => s.Category_Note);
                    break;
                default:
                    model = model.OrderBy(s => s.Category_Name);
                    break;
            }

                ViewBag.Page = page;
                int pageSize = 3;
                int pageNumber = (page - 1);

                return View(model.ToPagedList(pageNumber, pageSize));
         
        }

        public ActionResult IndexSearch(string searchString)
        {
            var model = from c in db.ListAll()select c;
            ViewBag.SearchString = searchString;
            if (!String.IsNullOrEmpty(searchString) )
            {
                model = model.Where(c => c.Category_Name.Contains(searchString)
                    || c.Category_Note.Contains(searchString));
            }
            return View(model);
        }
        
        public ActionResult IndexSort(string sortOrder)
        {
            var model = from c in db.ListAll() select c;
            ViewBag.CategoryName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CategortNote = sortOrder == "note" ? "note_desc" : "note";
            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(s => s.Category_Name); 
                    break;
                case "note":
                    model = model.OrderBy(s => s.Category_Note);
                    break;
                case "note_desc":
                    model = model.OrderByDescending(s => s.Category_Note);
                    break;
                default:
                    model = model.OrderBy(s => s.Category_Name);
                    break;
            }
            return View(model);
        }

        // GET: CategorysController/Details/5
        public ActionResult Details(int id)
        {
            var model = db.GetByID(id);
            return View(model);
        }

        // GET: CategorysController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorysController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categorys categorys)
        {
            try
            {
                db.Add(categorys);
                return RedirectToAction("Index");
            }
            catch { return View(); }
            {
                return View();
            }
        }

        // GET: CategorysController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = db.GetByID(id);
            return View(model);
        }

        // POST: CategorysController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categorys categorys)
        {
            try
            {
                db.Update(categorys);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategorysController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.GetByID(id);
            return View(model);
        }

        // POST: CategorysController/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                db.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
