using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.BlogPostTags {
   public class Access {

      public static List<BlogPostTags.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<BlogPostTags.Model>("select * from BlogPostTags", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(BlogPostTags.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into BlogPostTags (ForumPostID, TagID) values (@ForumPostID, @TagID)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new BlogPostTags.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM BlogPostTags WHERE id = @id", model);



         }


      }

      public static void ReplaceById(BlogPostTags.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO BlogPostTags (id, ForumPostID, TagID) VALUES (@id, @ForumPostID, @TagID)", model);



         }


      }

		public static List<Model> GetByPostID(int id)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{


				var output = con.Query<Model>("select * from BlogPostTags where ForumPostID = @ForumPostID", new Model { ForumPostID = id });

				return output.ToList();


			}


		}

		public static void RemoveByPostID(int ItemId)
		{


			using (IDbConnection con = new SqlConnection(CommonData.db))
			{



				var model = new Model();



				model.ForumPostID = ItemId;



				con.Execute("DELETE FROM BlogPostTags WHERE ForumPostID = @ForumPostID", model);



			}


		}

	}
}