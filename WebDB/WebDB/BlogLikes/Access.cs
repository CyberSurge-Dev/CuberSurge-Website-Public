using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.BlogLikes {
   public class Access {

      public static List<BlogLikes.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<BlogLikes.Model>("select * from BlogLikes", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(BlogLikes.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into BlogLikes (BlogPostID, UserID) values (@BlogPostID, @UserID)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new BlogLikes.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM BlogLikes WHERE id = @id", model);



         }


      }

      public static void ReplaceById(BlogLikes.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO BlogLikes (id, BlogPostID, UserID) VALUES (@id, @BlogPostID, @UserID)", model);



         }


      }

		public static bool UserLiked(int UserID, int BlogPostID)
		{
			using (IDbConnection con = new SqlConnection(CommonData.db))
			{
				var output = con.Query<Model>("SELECT * FROM BlogLikes WHERE UserID = @UserID and BlogPostID = @BlogPostID", new Model { UserID = UserID, BlogPostID = BlogPostID });

				if (output.Count() > 0)
				{
                    return true;
				}
				else
				{
                    return false;
				}

			}
		}

		public static int LikeCountByPostID(int id)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{


				var output = con.Query<BlogFiles.Model>("select * from BlogLikes where BlogPostID = @BlogPostID", new Model { BlogPostID = id });

				return output.ToList().Count();


			}


		}
		public static void RemoveByUserId(int ItemId)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{


				con.Execute("DELETE FROM BlogLikes WHERE UserID = @UserID", new Model()
				{
					UserID = ItemId
				});



			}


		}

		public static void RemoveByPostID(int ItemId)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{



				var model = new Model();



				model.BlogPostID = ItemId;



				con.Execute("DELETE FROM BlogLikes WHERE BlogPostID = @BlogPostID", model);



			}


		}

	}
}