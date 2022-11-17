using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesModel CreateNotes(NotesModel addnotes, string userid);

        public IEnumerable<NotesModel> GetAllNotes();

        public NotesModel UpdateNotes(NotesModel editnotes, string id, string userid);

        public bool DeleteNote(string id);

        public NotesModel PinnedORNot(string id, string userid);

        public NotesModel Archive(string id, string userid);

        public NotesModel Trash(string id, string userid);

        public string UploadImage(string id, IFormFile img, string userid);

        public NotesModel Color(string Color, string id);


    }
}
