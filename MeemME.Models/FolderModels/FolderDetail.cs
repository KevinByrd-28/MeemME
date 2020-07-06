using MeemME.Data;
using MeemME.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeemME.Models.FolderModels
{
    public class FolderDetail
    {
        public int FolderID { get; set; }
        public string Name { get; set; }

        [Display(Name = "Images")]
        public List<ImageListItem> ImagesInFolder { get; set; }
    }
}
