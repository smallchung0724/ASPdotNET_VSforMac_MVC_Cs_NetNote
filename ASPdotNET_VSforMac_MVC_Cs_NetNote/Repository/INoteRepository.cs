using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Models;

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Repository
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(int id);
        Task<List<Note>> ListAsync();
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
    }
}
