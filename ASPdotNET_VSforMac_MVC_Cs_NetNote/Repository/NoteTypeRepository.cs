using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Repository
{
    public class NoteTypeRepository : INoteTypeRepository
    {
        private NoteContext context;

        public NoteTypeRepository(NoteContext _context)
        {
            context = _context;
        }

        public Task<List<NoteType>> ListAsync()
        {
            return context.NoteTypes.ToListAsync();
        }
    }
}
