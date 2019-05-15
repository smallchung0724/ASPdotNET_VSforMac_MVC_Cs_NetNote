using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Models;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Repository;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly INoteTypeRepository _noteTypeRepository;

        public NoteController(INoteRepository noteRepository, INoteTypeRepository noteTypeReopsitory)
        {
            _noteRepository = noteRepository;
            _noteTypeRepository = noteTypeReopsitory;
        }

        // GET: /<controller>/
        public IActionResult Index(int pageindex = 1)
        {
            var pagesize = 1;

            var notes = _noteRepository.PageList(pageindex, pagesize);

            ViewBag.PageCount = notes.Item2;

            ViewBag.PageIndex = pageindex;

            return View(notes.Item1);
        }

        public async Task<IActionResult> Add()
        {
            var types = await _noteTypeRepository.ListAsync();

            ViewBag.Types = types.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromServices]IHostingEnvironment env, NoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string filename = string.Empty;
            if (model.Attachment != null)
            {
                filename = Path.Combine("file", Guid.NewGuid().ToString() + Path.GetExtension(model.Attachment.FileName));
                using (var stream = new FileStream(Path.Combine(env.WebRootPath, filename), FileMode.CreateNew))
                {
                    model.Attachment.CopyTo(stream);
                }
            }
            await _noteRepository.AddAsync(new Note
            {
                Title = model.Title,
                Content = model.Content,
                Create = DateTime.Now,
                TypeId = model.Type,
                Password = model.Password,
                Attachment = filename
            });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (!string.IsNullOrEmpty(note.Password))
                return View();
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int id, string password)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (!note.Password.Equals(password))
                return BadRequest("密碼錯誤,返回重新輸入");
            return View(note);
        }
    }
}
