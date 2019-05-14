using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Models;

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Repository
{
    public interface INoteTypeRepository
    {
        Task<List<NoteType>> ListAsync();
    }
}
