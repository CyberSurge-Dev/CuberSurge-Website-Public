using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.BlogFiles {
   public class Access {

      public static List<BlogFiles.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<BlogFiles.Model>("select * from BlogFiles", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(BlogFiles.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("SET ANSI_WARNINGS OFF; insert into BlogFiles (ForumPostID, Content, Filename) values (@ForumPostID, @Content, @Filename); SET ANSI_WARNINGS ON;", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new BlogFiles.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM BlogFiles WHERE id = @id", model);



         }


      }

      public static void ReplaceById(BlogFiles.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO BlogFiles (id, ForumPostID, Content, Filename) VALUES (@id, @ForumPostID, @Content, @Filename)", model);



         }


      }

		public static List<BlogFiles.Model> GetByPostID(int id)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{


				var output = con.Query<BlogFiles.Model>("select * from BlogFiles where ForumPostID = @ForumPostID", new Model { ForumPostID = id });

                return output.ToList();


			}


		}

		public static void RemoveByPostID(int ItemId)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{



				var model = new Model();



				model.ForumPostID = ItemId;



				con.Execute("DELETE FROM BlogFiles WHERE ForumPostID = @ForumPostID", model);



			}


		}



	}
}