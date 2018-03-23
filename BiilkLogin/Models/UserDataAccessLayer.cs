using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BiilkLogin.Models
{
    public class UserDataAccessLayer
    {
        string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Bina;Data Source=DESKTOP-QMN6SBR\\SQLEXPRESS";

        //To View all User details  
        public IEnumerable<User> GetAllUser()
        {
            List<User> UserList = new List<User>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM TblUser", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();

                    user.Id = Convert.ToInt32(rdr["Id"]);
                    user.Name = rdr["Name"].ToString();
                    user.Username = rdr["Username"].ToString();
                    user.Passwordd = rdr["Passwordd"].ToString();
                    user.Mail = rdr["Mail"].ToString();
                    user.Addresskey = Convert.ToInt32(rdr["Addresskey"]);
                    UserList.Add(user);
                }
                con.Close();
            }
            return UserList;
        }

    }
}
