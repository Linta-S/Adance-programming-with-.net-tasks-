using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Admin.Models.Entity
{
    public class ProductModel
    {

        public int Id { get; set; }
        public string P_name { get; set; }
        public string P_price { get; set; }
        public int P_categorie_id { get; set; }
        public int P_quantity { get; set; }
        public string P_details { get; set; }
        public string P_img { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}