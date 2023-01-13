using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.DocPosts {
   public class Access {

      public static List<DocPosts.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<DocPosts.Model>("select * from DocPosts", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(DocPosts.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into DocPosts (Title, Body, CatagoryID) values (@Title, @Body, @CatagoryID)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new DocPosts.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM DocPosts WHERE id = @id", model);



         }


      }

      public static void ReplaceById(DocPosts.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO DocPosts (id, Title, Body, CatagoryID) VALUES (@id, @Title, @Body, @CatagoryID)", model);



         }


      }

   }
}