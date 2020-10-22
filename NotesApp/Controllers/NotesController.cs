using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotesApp.Models;
using NotesApp.Services;

namespace NotesApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ILogger<NotesController> _logger;
        private readonly INotesManager _notesManager;
        public NotesController(ILogger<NotesController> logger,
            INotesManager notesManager)
        {
            _logger = logger;
            _notesManager = notesManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _notesManager.Get();
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            var model = new NotesInputModel();
            if (id == null)
            {
                model.Id = Guid.NewGuid();
                return View(model);
            }
            var _note = await _notesManager.Get(id.Value);
            if(_note == null)
            {
                return BadRequest();
            }
            else
            {
                model.Id = _note.Id;
                model.Title = _note.Title;
                model.Description = _note.Description;
                model.CreatedOn = _note.CreatedOn;
            }
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _notesManager.Get(id);
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, NotesInputModel model)
        {
            await _notesManager.Edit(id, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var IsDeleted = await _notesManager.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
