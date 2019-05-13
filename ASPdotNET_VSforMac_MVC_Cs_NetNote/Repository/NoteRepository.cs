using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Repository
{
    public class NoteRepository : INoteRepository
    {
        private NoteContext context;

        public NoteRepository(NoteContext _context)
        {
            context = _context;
        }

        public Task AddAsync(Note note)
        {
            context.Notes.Add(note);
            return context.SaveChangesAsync();
        }

        public Task<Note> GetByIdAsync(int id)
        {
            return context.Notes.FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<Note>> ListAsync()
        {
            return context.Notes.ToListAsync();
        }

        public Task UpdateAsync(Note note)
        {
            context.Entry(note).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
    }
}
