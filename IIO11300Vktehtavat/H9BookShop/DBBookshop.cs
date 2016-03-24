using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H9BookShop
{
    public class DBBookshop
    {
        public static DataTable GetTestData()
        {
            //luodaan testausta varten oikeanlainen datatable
            DataTable dt = new DataTable();
            //sarakkeiden määrittely
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("author", typeof(string));
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("year", typeof(int));
            //rivit eli tietueet
            dt.Rows.Add(11, "Pekka Lipposen seikkailut", "OUTSIDER", "Suomi", 1950);
            dt.Rows.Add(12, "Lucky Luke", "René Coscinny", "Belgia", 1946);
            return dt;
        }
        public static DataTable GetBooks(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT id, name, author, country, year FROM books";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Books");
                    da.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int UpdateBook(string connStr, int id, string name, string author, string country, int year)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //HUOM: käytetään SQL-kyselyn parametrejä kahdesta syystä:
                    //1:heittomerkki (hipsa) engl. apostrophe esim. nimessä O'Hara
                    //2:tietoturva: parametrisoidut kyselyt ovat turvallisempia
                    string sql = string.Format("UPDATE books SET name=@Nimi, author=@kirjailija, country='{3}', year={4} WHERE id={0}", id, name, author, country, year);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //lisätään komennolle parametrit
                    SqlParameter sp;
                    sp = new SqlParameter("Nimi", SqlDbType.NVarChar);
                    sp.Value = name;
                    cmd.Parameters.Add(sp);
                    sp = new SqlParameter("kirjailija", SqlDbType.NVarChar);
                    sp.Value = author;
                    cmd.Parameters.Add(sp);
                    //suoritetaan kysely
                    int lkm = cmd.ExecuteNonQuery();
                    conn.Close();
                    return lkm;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int InsertBook(string connStr, string name, string author, string country, int year)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //HUOM: käytetään SQL-kyselyn parametrejä kahdesta syystä:
                    //1:heittomerkki (hipsa) engl. apostrophe esim. nimessä O'Hara
                    //2:tietoturva: parametrisoidut kyselyt ovat turvallisempia
                    string sql = "INSERT INTO books (name, author, country, year) VALUES (@Nimi, @kirjailija, @Maa, @Vuosi)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //lisätään komennolle parametrit
                    cmd.Parameters.AddWithValue("@Nimi", name);
                    cmd.Parameters.AddWithValue("@kirjailija", author);
                    cmd.Parameters.AddWithValue("@Maa", country);
                    cmd.Parameters.AddWithValue("@Vuosi", year);
                    //suoritetaan kysely
                    int lkm = cmd.ExecuteNonQuery();
                    conn.Close();
                    return lkm;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int DeleteBook(string connStr, int id)
        {
            try
            {
                using (SqlConnection conn=new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("DELETE FROM books WHERE id={0}", id), conn);
                    int lkm = cmd.ExecuteNonQuery();
                    return lkm;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
