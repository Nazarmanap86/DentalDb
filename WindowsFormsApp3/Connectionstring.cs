using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    internal class Connectionstring
    {
        public SqlConnection GetCon()
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DentalDb;Integrated Security=True;TrustServerCertificate=True";
            return baglanti;
        }
    }
}
