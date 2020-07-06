using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MeemME.Data
{
    public class Folder
    {
        [Key]
        public int FolderID { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "List Of Images In The Folder")]
        public virtual ICollection<ImagesInFolder> ImagesInFolder { get; set; } = new List<ImagesInFolder>();

    }
}
