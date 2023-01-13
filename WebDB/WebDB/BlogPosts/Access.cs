using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
using System.Drawing;

namespace WebDB.BlogPosts {
   public class Access {

      public static List<BlogPosts.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<BlogPosts.Model>("select * from BlogPosts", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(BlogPosts.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into BlogPosts (Body, Title, PostDate) values (@Body, @Title, @PostDate)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new BlogPosts.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM BlogPosts WHERE id = @id", model);



         }


      }

      public static void ReplaceById(BlogPosts.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("UPDATE BlogPosts\nSET Title = @Title\nWHERE id = @id\nUPDATE BlogPosts\nSET Body = @Body\nWHERE id = @id", model);



         }


      }

		public static List<BlogPosts.Model> GetByContent(BlogPosts.Model model)
		{
			using (IDbConnection con = new SqlConnection(CommonData.db))
			{
				var output =  con.Query<BlogPosts.Model>("SELECT * FROM BlogPosts WHERE (Title like @Title) and (Body like @Body)", model);

                return output.ToList();
			}
		}

        public static BlogPosts.Model GetByID(int id)
        {
			using (IDbConnection con = new SqlConnection(CommonData.db))
			{
				var output = con.Query<BlogPosts.Model>("SELECT * FROM BlogPosts WHERE id = @id", new Model { id = id });
                
                if (output.Count() > 0)
                {
					return (output.ToArray())[0];
				} else
                {
                    return new BlogPosts.Model();
                }
                
			}
		}

        public static List<int> GetForumIDs()
        {
            using (IDbConnection con = new SqlConnection(CommonData.db))
            {
                var output = con.Query<int>("SELECT id FROM BlogPosts", new DynamicParameters());

                return output.ToList();
            }
		}

        public static List<Model> GetByDate(DateTime Start, DateTime End)
        {
            using (IDbConnection con = new SqlConnection(CommonData.db))
            {
                var output = con.Query<BlogPosts.Model>("SELECT* FROM BlogPosts WHERE PostDate BETWEEN @Start and @End", new
                {
                    Start = Start,
                    End = End
                });

                return output.ToList();
            }
        }   
	}
}