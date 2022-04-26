using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LibraryManagementSystem1.Models;

namespace LibraryManagementSystem1.DAL
{
    public class IssueBooks
    {

        #region Variable Declaration


        private Database db;
        #endregion

        #region Constructors

        public IssueBooks()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }
        public IssueBooks(int bookId)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.BookId = bookId;
        }
        #endregion


        #region Properties

        public string IssueBooksXml { get; set; }

        public string IssueBooksRemoveString { get; set; }

        public string issueIdXml { get; set; }
        public int status { get; set; }

        public int BookCount { get; set; }
        public int IssueId { get; set; }

        public int StudentId { get; set; }

        public int BookId { get; set; }

        public int BookQuantity { get; set; }

        public DateTime IssueDate { get; set; }
       

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }



        #region Actions

        public int IssuedBooksUsingXML()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("SP_IssueBookDetailsUpdate");

                this.db.AddInParameter(com, "IssueId", DbType.Int32, this.IssueId);
                this.db.AddInParameter(com, "StudentId", DbType.Int32, this.StudentId);
                this.db.AddInParameter(com, "IssueDate", DbType.DateTime, this.IssueDate);
                this.db.AddInParameter(com, "IssuedBookxml", DbType.Xml, this.IssueBooksXml);
                this.db.AddInParameter(com, "IssuedBookRemoveStr", DbType.String, this.IssueBooksRemoveString);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception)
            {
                return 0;
            }           
            return 0;
        }

        public List<BooksModel> IssueBookGetList()
        {
            
            DataSet dsBooksList = null;
            List<BooksModel> IssueBookList = null;
            try
            {
                
                DbCommand com = this.db.GetStoredProcCommand("SP_IssueBooksGetDetails");
                this.db.AddInParameter(com, "IssueId", DbType.Int32, this.IssueId);               
                this.db.ExecuteNonQuery(com);
                dsBooksList = db.ExecuteDataSet(com);

                IssueBookList = new List<BooksModel>();

                for (int i = 0; i < dsBooksList.Tables[0].Rows.Count; i++)
                {
                    IssueBookList.Add(new BooksModel()
                    {
                        IssueId = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["IssueId"]),
                        
                        StudentId = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["StudentId"]),
                        
                        IssueDate = Convert.ToDateTime(dsBooksList.Tables[0].Rows[i]["IssueDate"]),
                        BookCount = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["BookCount"]),
                        BookId = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["BookId"]),
                        BookName = dsBooksList.Tables[0].Rows[i]["BookName"].ToString(),
                        BookCategoryName = dsBooksList.Tables[0].Rows[i]["BookCategoryName"].ToString(),
                        BookPublisherName = dsBooksList.Tables[0].Rows[i]["BookPublisherName"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                //To Do: Handle Exception
            }

            return IssueBookList;
        }

        public int Save()
        {
            if (this.IssueId == 0)
            {
                IssueBook();
                return IssueId;
            }
            else
            {
                /*if(this.status == 0)
                {
                    return IssueBookDetailsUpdate();
                }*/
                if (this.IssueId > 0)
                {
                    return this.IssuedBooksUsingXML();
                }
                else
                {
                    this.IssueId = 0;
                    return 0;
                }
            }         
        }
        public bool IssueBookUpdate()
        {
            IssueBookDetailsUpdate();
            return true;
        }
        private int IssueBook()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("IssueBookProc");
                this.db.AddOutParameter(com, "IssueId", DbType.Int32, 1);
                
                ///////////////////////////////////////////////////////////////////////////
                if (this.StudentId > 0)
                {
                    this.db.AddInParameter(com, "StudentId", DbType.Int32, this.StudentId);
                }
                else
                {
                    this.db.AddInParameter(com, "StudentId", DbType.Int32, DBNull.Value);
                }
                if (this.IssueDate > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "IssueDate", DbType.DateTime, this.IssueDate);
                }
                else
                {
                    this.db.AddInParameter(com, "IssueDate", DbType.DateTime, DBNull.Value);
                }
                
                if (this.CreatedBy > 0)
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, 1);
                }
               
                if (this.ModifiedBy > 0)
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, 1);
                }
               
                this.db.ExecuteNonQuery(com);
                this.IssueId = Convert.ToInt32(this.db.GetParameterValue(com, "IssueId"));      // Read in the output parameter value
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return 0;
            }

            return this.IssueId; // Return whether ID was returned
        }

        private int IssueBookDetails()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("IssueBookDetailsProc");
                
                ///////////////////////////////////////////////////////////////////////////
                if (this.IssueId > 0)
                {
                    this.db.AddInParameter(com, "IssueId", DbType.Int32, this.IssueId);
                }
                else
                {
                    this.db.AddInParameter(com, "IssueId", DbType.Int32, DBNull.Value);
                }
                if (this.BookId > 0)
                {
                    this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
                }
                else
                {
                    this.db.AddInParameter(com, "BookId", DbType.Int32, DBNull.Value);
                }

                if (this.BookCount > 0)
                {
                    this.db.AddInParameter(com, "BookCount", DbType.Int32, this.BookCount);
                }
                else
                {
                    this.db.AddInParameter(com, "BookCount", DbType.Int32, DBNull.Value);
                }

                if (this.CreatedBy > 0)
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, 1);
                }
                
                if (this.ModifiedBy > 0)
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, 1);
                }
                
                this.db.ExecuteNonQuery(com);               
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return 0;
            }

            return this.IssueId; // Return whether ID was returned
        }
       

        private int IssueBookDetailsUpdate()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("SP_IssueBookDetailsUpdate");

                ///////////////////////////////////////////////////////////////////////////
                if (this.IssueId > 0)
                {
                    this.db.AddInParameter(com, "IssueId", DbType.Int32, this.IssueId);
                }
                else
                {
                    this.db.AddInParameter(com, "IssueId", DbType.Int32, DBNull.Value);
                }
                if (this.StudentId > 0)
                {
                    this.db.AddInParameter(com, "StudentId", DbType.Int32, this.StudentId);
                }
                else
                {
                    this.db.AddInParameter(com, "StudentId", DbType.Int32, DBNull.Value);
                }
                if (this.IssueDate > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "IssueDate", DbType.DateTime, this.IssueDate);
                }
                else
                {
                    this.db.AddInParameter(com, "IssueDate", DbType.DateTime, DBNull.Value);
                }
                if (this.BookId > 0)
                {
                    this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
                }
                else
                {
                    this.db.AddInParameter(com, "BookId", DbType.Int32, DBNull.Value);
                }

                if (this.BookCount > 0)
                {
                    this.db.AddInParameter(com, "BookCount", DbType.Int32, this.BookCount);
                }
                else
                {
                    this.db.AddInParameter(com, "BookCount", DbType.Int32, DBNull.Value);
                }

                if (this.CreatedBy > 0)
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, 1);
                }

                if (this.ModifiedBy > 0)
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, 1);
                }

                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return 0;
            }

            return this.IssueId; // Return whether ID was returned
        }

    }

    #endregion

    #endregion
}