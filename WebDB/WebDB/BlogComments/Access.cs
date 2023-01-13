using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.BlogComments {
   public class Access {

      public static List<BlogComments.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<BlogComments.Model>("select * from BlogComments", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(BlogComments.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into BlogComments (BlogPostID, UserID, Comment) values (@BlogPostID, @UserID, @Comment)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new BlogComments.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM BlogComments WHERE id = @id", model);



         }


      }

      public static void ReplaceById(BlogComments.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO BlogComments (id, BlogPostID, UserID, Comment) VALUES (@id, @BlogPostID, @UserID, @Comment)", model);



         }


      }

		public static List<Model> GetByPostID(int id)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{


				var output = con.Query<Model>("select * from BlogComments where BlogPostID = @BlogPostID", new Model { BlogPostID = id });

				return output.ToList();


			}


		}


		public static void RemoveByPostID(int ItemId)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{



				var model = new BlogComments.Model();



				model.BlogPostID = ItemId;



				con.Execute("DELETE FROM BlogComments WHERE BlogPostID = @BlogPostID", model);



			}


		}

	}
}