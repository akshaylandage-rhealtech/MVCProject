using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem1.Models
{

    

    public class BooksModel
    {
        #region Properties

        public class SelectedBookList
        {

            public int BookId { get; set; }
            public int BookCount { get; set; }
            public int Status { get; set; }
           
        }

        public class IssuedDetailList
        {

            public int StudentId { get; set; }

            public DateTime IssueDate { get; set; }

            public int IssueId { get; set; }
           
        }

        public class IssueBookRemoveList
        {
            public int BookId { get; set; }            
        }

        public string IssueBooksXml { get; set; }

        public string multiCategoryStr { get; set; }
        public string multiPublisherStr { get; set; }

        public string IssueBooksRemoveString { get; set; }

        public string issueIdXml { get; set; }
        public int status { get; set; }
        public int BookCount { get; set; }

        public int IssueId { get; set; }

        public int StudentId { get; set; }       

        public DateTime IssueDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string PageName { get; set; }
        public int Id { get; set; } = 0;

        public int TotalPages { get; set; }
        public int BookId { get; set; }

        public int PageLength { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public List<BooksModel> GetBooks { get; set; }

        public List<BooksModel> GetIssueBookList { get; set; }

        public List<BooksModel> GetCategories { get; set; }

        public List<BooksModel> GetPublishers { get; set; }

        public string BookCategory { get; set; }

        public string BookPublisher { get; set; }
        public string BookName { get; set; }

        public int BookCategoryId { get; set; }
        public int BookPublisherId { get; set; }

        public string BookPublisherName { get; set; }

        public string BookCategoryName { get; set; }

        public int BookQuantity { get; set; }
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int TotalCount { get; set; }

        #endregion

    }

}