using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface INotesManager
    { 
        Task Edit(Guid Id, NotesInputModel notes);
        Task<bool> Delete(Guid Id);
        Task<List<NotesViewModel>> Get();
        Task<NotesViewModel> Get(Guid Id);
    }
}
