using MeemME.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeemME.Models.FolderModels
{
    public class FolderCreate //Might not be used, depending on the Run program
    {
        public string Name { get; set; }

        [Display(Name = "Images")]
        public int?[] ImagesInFolder { get; set; }
    }
   
}
