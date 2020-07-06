using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeemME.Data
{
    public class Save
    {
        [Key]
        public int SaveID { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Title { get; set; }

        [Url]
        [Display(Name = "Input URL")]
        public string URL { get; set; }
    }
}
