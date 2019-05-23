using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Repository;
using Microsoft.AspNetCore.Mvc;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.Models;
using ASPdotNET_VSforMac_MVC_Cs_NetNote.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPdotNET_VSforMac_MVC_Cs_NetNote.API
{
    [Route("api/note")]
    public class NoteAPIController : Controller
    {
        private INoteRepository _noteRepository;
        private INoteTypeRepository _noteTypeRepository;

        public NoteAPIController(INoteRepository noteRepository, INoteTypeRepository noteTypeRepository)
        {
            _noteRepository = noteRepository;
            _noteTypeRepository = noteTypeRepository;
        }

        // GET: api/note
        [HttpGet]
        public IActionResult Get(int pageindex = 1)
        {
            var pagesize = 1;
            var notes = _noteRepository.PageList(pageindex, pagesize);
            ViewBag.PageCount = notes.Item2;
            ViewBag.PageIndex = pageindex;
            var result = notes.Item1.Select(r => new NoteViewModel
            {
                Id = r.Id,
                Title = string.IsNullOrEmpty(r.Password) ? r.Title : "加密内容",
                Content = string.IsNullOrEmpty(r.Password) ? r.Content : "",
                Attachment = string.IsNullOrEmpty(r.Password) ? r.Attachment : "",
                Type = r.Type.Name
            });
            return Ok(result);
        }

        // GET api/note/5
        // GET api/note/5?password=123
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id, string password)
        {
            var note = await _noteRepository.GetByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(note.Password) && !note.Password.Equals(password))
            {
                return Unauthorized();
            }

            var result = new NoteViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                Attachment = note.Attachment,
                Type = note.Type.Name
            };

            return Ok(result);
        }

        // POST api/note
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string filename = string.Empty;

            await _noteRepository.AddAsync(new Note
            {
                Title = model.Title,
                Content = model.Content,
                Create = DateTime.Now,
                TypeId = model.Type,
                Password = model.Password,
                Attachment = filename
            });

            return CreatedAtAction("Index", "");
        } }

        /*
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
