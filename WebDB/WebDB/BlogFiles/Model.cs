namespace WebDB.BlogFiles {
   public class Model {

      public int id { get; set; }

      public int ForumPostID { get; set; }
      
      public byte[] Content { get; set; }

      public string Filename { get; set; }

   }
}