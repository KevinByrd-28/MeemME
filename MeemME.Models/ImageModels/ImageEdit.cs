using MeemME.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MeemME.Models.ImageModels
{
    public class ImageEdit
    {
        public int ImageID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Top")]
        public string TopText { get; set; }

        [Display(Name = "Bottom")]
        public string BottomText { get; set; }


    }
}
