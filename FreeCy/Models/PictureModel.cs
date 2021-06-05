using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeCy.Models
{
    public class PictureModel
    {
        //[Required]
        public HttpPostedFileWrapper Picture { get; set; }
    }
}