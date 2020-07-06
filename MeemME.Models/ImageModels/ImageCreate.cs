using MeemME.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace MeemME.Models.ImageModels
{
    public class ImageCreate
    {
        public string Title { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }//might not need
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name = "Top Text")]
        public string TopText { get; set; }
        [Required]
        [Display(Name = "Bottom Text")]
        public string BottomText { get; set; }
    }

    
}
