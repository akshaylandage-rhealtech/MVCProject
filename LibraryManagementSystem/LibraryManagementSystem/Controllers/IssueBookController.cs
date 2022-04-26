using LibraryManagementSystem1.DAL;
using LibraryManagementSystem1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem1.Controllers
{
    public class IssueBookController : Controller
    {
        public ActionResult IssueBookListPopup()
        {

            return View();
        }

        [HttpPost]
        public ActionResult IssueBookListPopup(BooksModel booksModel)
        {
            Books b = new Books();
            booksModel.GetBooks = b.GetList();

            return PartialView("_IssueBookList", booksModel);
        }

        public ActionResult IssueBookList()
        {
            return View();
        }

        public ActionResult IssueBookStudent(int Id)
        {
            Books b = new Books();
            BooksModel bookModel = new BooksModel();
            IssueBooks issueBooks = new IssueBooks();
            issueBooks.IssueId = Id;
            if (Id > 0)
            {
                bookModel.GetIssueBookList = issueBooks.IssueBookGetList();
                try
                {
                    bookModel.StudentId = bookModel.GetIssueBookList[0].StudentId;
                    bookModel.IssueDate = bookModel.GetIssueBookList[0].IssueDate;
                    bookModel.IssueId = bookModel.GetIssueBookList[0].IssueId;
                }
                catch (Exception ex)
                {

                }
            }

            bookModel.GetBooks = b.GetList();

            return View(bookModel);
        }


        [HttpPost]
        public ActionResult IssueBookList(List<BooksModel.IssuedDetailList> DetailList, List<BooksModel.SelectedBookList> SelectedList, List<BooksModel.IssueBookRemoveList> RemoveList)
        {

            BooksModel model = new BooksModel();


            IssueBooks issuebook = new IssueBooks();
            issuebook.IssueId = DetailList[0].IssueId;
            issuebook.StudentId = DetailList[0].StudentId;
            issuebook.IssueDate = DetailList[0].IssueDate;


            /*string issueIdXml = "<IssuedBookDetails>";
            for (int i = 0; i < SelectedList.Count; i++)
            {


                issueIdXml += "<ID><IssueId>" + DetailList[0].IssueId + "</IssueId>";
                issueIdXml += "<StudentId>" + DetailList[0].StudentId + "</StudentId>";
                issueIdXml += "<IssueDate>" + DetailList[0].IssueDate + "</IssueDate></ID>";

            }
            issueIdXml += "</IssuedBookDetails>";
            issuebook.issueIdXml = issueIdXml;*/

            if (SelectedList != null)
            {
                string issueBooksXml = "<IssuedBookDetails>";
                for (int i = 0; i < SelectedList.Count; i++)
                {

                    issueBooksXml += "<Books><BookId>" + SelectedList[i].BookId + "</BookId>";
                    issueBooksXml += "<BookCount>" + SelectedList[i].BookCount + "</BookCount>";
                    issueBooksXml += "<IssueStatus>" + SelectedList[i].Status + "</IssueStatus></Books>";


                }
                issueBooksXml += "</IssuedBookDetails>";
                issuebook.IssueBooksXml = issueBooksXml;
            }
            string str = "";
            if (RemoveList != null)
            {
                for (int i = 0; i < RemoveList.Count; i++)
                {
                    str += RemoveList[i].BookId+",";
                }
            }
            issuebook.IssueBooksRemoveString=str.Trim(',');
            issuebook.IssuedBooksUsingXML();

            return Json(model);
        }

        /* [HttpPost]
         public ActionResult IssueBookList(BooksModel booksModel)
         {

             IssueBooks issueBooks = new IssueBooks();
             Books books = new Books();

             issueBooks.IssueId = booksModel.IssueId;
             issueBooks.StudentId = booksModel.StudentId;
             issueBooks.IssueDate = Convert.ToDateTime(booksModel.IssueDate);
             issueBooks.BookId = booksModel.BookId;
             issueBooks.status = booksModel.status;
             issueBooks.BookCount = booksModel.BookCount;



             int id = issueBooks.Save();
             booksModel.IssueId = id;
             books.BookId = issueBooks.BookId;
             books.Load();
             books.BookQuantity = books.BookQuantity - issueBooks.BookCount;
             books.Save();
             return Json(booksModel);
         }*/
    }
}