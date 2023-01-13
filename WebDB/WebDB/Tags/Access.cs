using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.Tags {
   public class Access {

      public static List<Tags.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<Tags.Model>("select * from Tags", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(Tags.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into Tags (Tag, Color) values (@Tag, @Color)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new Tags.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM Tags WHERE id = @id", model);



         }


      }

      public static void ReplaceById(Tags.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO Tags (id, Tag, Color) VALUES (@id, @Tag, @Color)", model);



         }


      }

		public static Model GetByID(int id)
		{
			using (IDbConnection con = new SqlConnection(CommonData.db))
			{
				var output = con.Query<Model>("SELECT * FROM Tags WHERE id = @id", new Model { id = id });

				if (output.Count() > 0)
				{
					return output.ToList()[0];
				}
				else
				{
					return new Model();
				}

			}
		}

	}
}