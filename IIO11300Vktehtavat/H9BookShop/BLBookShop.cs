using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H9BookShop
{
    public class Book
    {
        #region PROPERTIES
        private int id;

        public int ID
        {
            get { return id; }
            
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        #endregion
        #region CONSTRUCTORS
        public Book (int id)
        {
            this.id = id;
        }
        public Book (int id, string name, string author, string country, int year)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.country = country;
            this.year = year;
        }
        #endregion
        #region METHODS
        public override string ToString()
        {
            return name + " written by " + author;
        }
        #endregion
    }
    public static class Bookshop
    {
        private static string cs = H9BookShop.Properties.Settings.Default.Kirjakauppa;
        public static List<Book> GetTestBooks()
        {
            List<Book> temp = new List<Book>();
            temp.Add(new Book(1, "Sota ja Rauha", "Leo Tolstoi", "Venäjä", 1867));
            temp.Add(new Book(2, "Anna karenina", "Leo Tolstoi", "Venäjä", 1877));
            return temp;
        }
        public static List<Book> GetBooks(bool useDB)
        {
            try
            {
                DataTable dt;
                List<Book> books = new List<Book>();
                if (useDB)
                {
                    //pyydetään DB-kerrokselta kirjojen tiedot
                    dt = DBBookshop.GetBooks(cs);
                }
                else
                {
                    dt = DBBookshop.GetTestData();
                }
                //ORM = muutetaan datatablen rivit olioiksi
                Book book;
                foreach (DataRow row in dt.Rows)
                {
                    book = new Book((int)row[0]);
                    book.Name = row["name"].ToString();
                    book.Author = row["author"].ToString();
                    book.Country = row["country"].ToString();
                    book.Year = (int)row["year"];
                    books.Add(book);
                }
                //palautus
                return books;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
        public static int UpdateBook(Book book)
        {
            try
            {
                int lkm = DBBookshop.UpdateBook(cs, book.ID, book.Name, book.Author, book.Country, book.Year);
                return lkm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool InsertBook(Book book)
        {
            try
            {
                int lkm = DBBookshop.InsertBook(cs, book.Name, book.Author, book.Country, book.Year);
                if (lkm > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public static bool Deletebook(Book book)
        {
            try
            {
                int lkm = DBBookshop.DeleteBook(cs, book.ID);
                if (lkm > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
