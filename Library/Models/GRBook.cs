using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{   [JsonObject]
    public class GRBook
    {
        public int books_count { get; set; }
        public int id { get; set; }
        public int original_publication_day { get; set; }
        public int original_publication_month { get; set; }
        public int original_publication_year { get; set; }
        public int ratings_count { get; set; }
        public int text_reviews_count { get; set; }
        public string average_rating { get; set; }
        public Best_Book BestBook { get; set; }


    }
    [JsonObject]
    public class Best_Book
    {
        public int id { get; set; }
        public string Title { get; set; }
        public GRAuthor Author { get; set; }
        public string Image_URL { get; set; }
        public string Small_Image_URL { get; set; }
    }
    [JsonObject]
    public class GRAuthor
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}
//<books_count type = "integer" > 1 </ books_count >
//        < id type="integer">11571577</id>
//        <original_publication_day type = "integer" > 30 </ original_publication_day >
//        < original_publication_month type="integer">4</original_publication_month>
//        <original_publication_year type = "integer" > 2010 </ original_publication_year >
//        < ratings_count type="integer">300</ratings_count>
//        <text_reviews_count type = "integer" > 16 </ text_reviews_count >
//        < average_rating > 4.17 </ average_rating >
//        < best_book type="Book">
//          <id type = "integer" > 8782597 </ id >
//          < title > Ender's Game</title>
//          <author>
//            <id type = "integer" > 2940867 </ id >
//            < name > Frederic P.Miller</name>
//          </author>
//          <image_url>
//            https://s.gr-assets.com/assets/nophoto/book/111x148-6204a98ba2aba2d1aa07b9bea87124f8.png
//          </image_url>
//          <small_image_url>
//            https://s.gr-assets.com/assets/nophoto/book/50x75-6121bf4c1f669098041843ec9650ca19.png
//          </small_image_url>
//        </best_book>