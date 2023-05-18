using Nhom7_WebsiteClothes.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom7_WebsiteClothes.Controllers
{
    public class ClothesController : Controller
    {
        // GET: Clothes
        public ActionResult Index(int? page)
        {
            var context = new ClothesModelContext();
            int pageIndex = page ?? 1;
            int pageSize = 12;
            var clothes = context.Clothes.OrderBy(p => p.Title).ToPagedList(pageIndex, pageSize);
            ViewBag.PageIndex = pageIndex;
            ViewBag.CategoryId = null;
            return View(clothes);
        }

        public ActionResult Search(string searchString)
        {
            var context = new ClothesModelContext();
            var listClothesSearch = context.Clothes.Where(p => p.Title.Contains(searchString) || p.Description.Contains(searchString)).OrderBy(p => p.Title).ToPagedList(1, 4);
            return View("Index", listClothesSearch);
        }

        public ActionResult GetClothByCategory(int id, int? page)
        {
            var context = new ClothesModelContext();
            int pageIndex = page ?? 1;
            int pageSize = 8;
            var clothes = context.Clothes.Where(p => p.CategoryId == id).OrderBy(p => p.Title).ToPagedList(pageIndex, pageSize);
            ViewBag.PageIndex = pageIndex;
            ViewBag.CategoryId = id;
            return View("Index", clothes);
        }


        public ActionResult GetCategory()
        {
            var context = new ClothesModelContext();
            var listCategory = context.Categories.ToList();
            return PartialView(listCategory);
        }

        public ActionResult Details(int id)
        {
            var context = new ClothesModelContext();
            var firstBook = context.Clothes.FirstOrDefault(p => p.Id == id);
            if (firstBook == null)
                return HttpNotFound("khong tim thay ma sach nay");
            return View(firstBook);
        }
    }
}