using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public class NotesManager : INotesManager
    {
        private readonly ApplicationDbContext _db;
        public NotesManager(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var _note = await _db.Notes.Where(a => a.Id == Id).SingleOrDefaultAsync();
                if (_note == null)
                    return false;
                _db.Notes.Remove(_note);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
        }

        public async Task Edit(Guid Id, NotesInputModel notes)
        {
            try
            {
                var _note = await _db.Notes.Where(a => a.Id == Id).SingleOrDefaultAsync();
                
                if(_note == null) // perform an Create operation
                {
                    _note = new Notes
                    {
                        Id = Id,
                        CreatedOn = DateTime.Now,
                        Description = notes.Description,
                        Title = notes.Title
                    };
                    _db.Notes.Add(_note);
                }
                else
                {
                    _note.Description = notes.Description;
                    _note.Title = notes.Title;
                }
                await _db.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
        }

        public async Task<List<NotesViewModel>> Get()
        {
            try
            {
                var model = new List<NotesViewModel>();
                var _note = await _db.Notes.ToListAsync();
                foreach (var item in _note)
                {
                    model.Add(new NotesViewModel
                    {
                        CreatedOn = item.CreatedOn,
                        Description = item.Description,
                        Id = item.Id,
                        Title = item.Title
                    });
                }
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<NotesViewModel> Get(Guid Id)
        {
            try
            {
                var _note = await _db.Notes.Where(a => a.Id == Id).SingleOrDefaultAsync();
                if (_note == null)
                    return null;
                var model = new NotesViewModel
                {
                    Id = _note.Id,
                    CreatedOn = _note.CreatedOn,
                    Description = _note.Description,
                    Title = _note.Title
                };
                return model;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
