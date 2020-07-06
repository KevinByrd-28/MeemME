using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeemME.Models.SaveModels
{
    public class SaveCreate
    {
        public string Title { get; set; }

        [Url]
        [Display(Name = "Input URL")]
        public string URL { get; set; }
    }
}
