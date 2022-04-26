using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem1.Models;

namespace LibraryManagementSystem1.Controllers
{
    public class BookController : Controller
    {
        // GET: Book

        // Index Action
        public ActionResult Index()
        {

            Books b = new Books();
            BooksModel bookModel = new BooksModel();

            if (Session["Details"] != null)
            {

                bookModel = (BooksModel)Session["Details"];
            }

            bookModel.GetBooks = b.GetList();
            bookModel.GetCategories = b.GetCategoryList();
            bookModel.GetPublishers = b.GetPublisherList();
            bookModel.TotalCount = b.TotalCount;
            return View(bookModel);
        }

        [HttpPost]
        public ActionResult Index(BooksModel booksModel)
        {
            Session["Details"] = booksModel;

            Books b = new Books();
            b.BookName = booksModel.BookName;
            b.BookCategoryId = booksModel.BookCategoryId;
            b.BookPublisherId = booksModel.BookPublisherId;
            b.PageNumber = booksModel.PageNumber;
            b.PageLength = booksModel.PageLength;
            b.multiCategoryStr = booksModel.multiCategoryStr;
            b.multiPublisherStr = booksModel.multiPublisherStr;

            booksModel.GetBooks = b.GetList();
            booksModel.GetCategories = b.GetCategoryList();
            booksModel.GetPublishers = b.GetPublisherList();
            booksModel.TotalCount = b.TotalCount;

            double t = (double)booksModel.TotalCount / (double)booksModel.PageLength;
            int n = (int)t;
            if (n == t)
            {
                booksModel.TotalPages = n;
            }
            else
            {
                booksModel.TotalPages = n + 1;
            }

            return PartialView("_BookListPaging", booksModel);
        }

        //Insert Book Action

        public ActionResult InsertBookAction(int id)
        {
            Books b = new Books();
            BooksModel bookModel = new BooksModel();

            bookModel.GetCategories = b.GetCategoryList();
            bookModel.GetPublishers = b.GetPublisherList();
            if (id > 0)
            {
                b.BookId = id;
                bookModel.BookId = id;
                b.Load();
                bookModel.IsActive = b.IsActive;
                bookModel.BookName = b.BookName;
                bookModel.BookQuantity = b.BookQuantity;
                bookModel.BookCategoryId = b.BookCategoryId;
                bookModel.BookPublisherId = b.BookPublisherId;
                
            }
           
            return View(bookModel);
        }

        [HttpPost]
        public ActionResult InsertBookAction(BooksModel booksModel)
        {
            Books b = new Books();
            b.BookId = booksModel.BookId;
            b.BookName = booksModel.BookName;
            b.BookCategoryId = booksModel.BookCategoryId;
            b.BookPublisherId = booksModel.BookPublisherId;
            b.BookQuantity = booksModel.BookQuantity;
            b.IsActive = booksModel.IsActive;
            b.Save();
            return RedirectToAction("Index", "Book");
        }

        //Delete Action

        public ActionResult DeleteBookAction(int id)
        {
            Books b = new Books();
            b.BookId = id;
            b.Delete();
            return Json(true);
        }

        // Reset Action
        public void ResetSession()
        {
            Session.Remove("Details");
        }

    }
}
