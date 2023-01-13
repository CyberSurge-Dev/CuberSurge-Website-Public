using Microsoft.Data.SqlClient; using System; using Dapper; using System.Data;
namespace WebDB.DocCatagories {
   public class Access {

      public static List<DocCatagories.Model> GetItems() {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var output = con.Query<DocCatagories.Model>("select * from DocCatagories", new DynamicParameters());



            return output.ToList();



         }


      }

      public static void Add(DocCatagories.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("insert into DocCatagories (CatagoryName) values (@CatagoryName)", model);



         }


      }

      public static void RemoveById(int ItemId) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            var model = new DocCatagories.Model();



            model.id = ItemId;



            con.Execute("DELETE FROM DocCatagories WHERE id = @id", model);



         }


      }

      public static void ReplaceById(DocCatagories.Model model) {


         using (IDbConnection con = new SqlConnection(CommonData.db)) {



            con.Execute("REPLACE INTO DocCatagories (id, CatagoryName) VALUES (@id, @CatagoryName)", model);



         }


      }

   }
}