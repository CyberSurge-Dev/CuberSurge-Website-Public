using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.Users {
   public class Access {

      public static List<Users.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<Users.Model>("select * from Users", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(Users.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into Users (Email, Username, Password, IsAdmin) values (@Email, @Username, @Password, @IsAdmin)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new Users.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM Users WHERE id = @id", model);



         }


      }

      public static void ReplaceById(Users.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO Users (id, Email, Username, Password, IsAdmin) VALUES (@id, @Email, @Username, @Password, @IsAdmin)", model);



         }


      }

		public static Model GetByID(int id)
		{
			using (IDbConnection con = new SqlConnection(CommonData.db))
			{
				var output = con.Query<Model>("SELECT * FROM Users WHERE id = @id", new Model { id = id });

				if (output.Count() > 0)
				{
					return (output.ToArray())[0];
				}
				else
				{
					return new Model();
				}

			}
		}

	}
}