using System;
using System.ComponentModel.DataAnnotations;

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.ViewModels
{
    public class NoteModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "標題")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "內容")]
        public string Content { get; set; }

        [Display(Name = "類型")]
        public int Type { get; set; }
    }
}
