using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeemME.Data
{
    public class ImagesInFolder
    {
        [Key]
        public int ImagesInFolderID { get; set; }

        [ForeignKey("Image")]
        public int ImageID { get; set; }
        public virtual Image Image { get; set; }

        [ForeignKey("Folder")]
        public int FolderID { get; set; }
        public virtual Folder Folder { get; set; }
    }
}
