using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Models;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Repository;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Controllers
{
    public class NoteController : Controller
    {
        private INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var notes = await _noteRepository.ListAsync();

            return View(notes);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _noteRepository.AddAsync(new Note
            {
                Title = model.Title,
                Content = model.Content,
                Create = DateTime.Now
            });

            return RedirectToAction("Index");
        }
    }
}
