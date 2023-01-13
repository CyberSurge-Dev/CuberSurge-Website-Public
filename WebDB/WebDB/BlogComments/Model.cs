namespace WebDB.BlogComments {
   public class Model {

      public int id { get; set; }

      public int BlogPostID { get; set; }

      public int UserID { get; set; }

      public string Comment { get; set; }

   }
}