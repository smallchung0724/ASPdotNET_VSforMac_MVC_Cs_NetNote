using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Models
{
    /// <summary>
    /// 筆記類型
    /// </summary>
    public class NoteType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public List<Note> Notes { get; set; }
    }
}
