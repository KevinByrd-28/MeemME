using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace MeemME.Data
{

    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Title { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name = "Top Text")]
        public string TopText { get; set;}
        [Required]
        [Display(Name = "Bottom Text")]
        public string BottomText { get; set; }


        public virtual ICollection<ImagesInFolder> ImagesInFolder { get; set; } = new List<ImagesInFolder>();
    }
}
