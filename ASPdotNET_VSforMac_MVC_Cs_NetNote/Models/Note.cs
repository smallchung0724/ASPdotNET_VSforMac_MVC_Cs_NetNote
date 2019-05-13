using System;
using System.ComponentModel.DataAnnotations;

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Models
{
    /// <summary>
    /// 筆記
    /// </summary>
    public class Note
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Create { get; set; }
        public int TypeId { get; set; }
        public NoteType Type { get; set; }
        public string Password { get; set; }
        public string Attachment { get; set; }
    }
}
