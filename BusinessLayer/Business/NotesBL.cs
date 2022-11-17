using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;

        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        public NotesModel CreateNotes(NotesModel addnotes, string userid)
        {
            try
            {
                return notesRL.CreateNotes(addnotes,userid);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NotesModel> GetAllNotes()
        {
            try
            {
                return notesRL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesModel UpdateNotes(NotesModel editnotes,string id, string userid)
        {
            try
            {
                return this.notesRL.UpdateNotes(editnotes,id,userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(string id)
        {
            try
            {
                return this.notesRL.DeleteNote(id);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public NotesModel PinnedORNot(string id, string userid)
        {
            try
            {
                return this.notesRL.PinnedORNot(id,userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesModel Archive(string id, string userid)
        {
            try
            {
                return this.notesRL.Archive(id,userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesModel Trash(string id, string userid)
        {
            try
            {
                return this.notesRL.Trash(id,userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UploadImage(string id, IFormFile img, string userid)
        {
            try
            {
                return this.notesRL.UploadImage(id, img,userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesModel Color(string Color, string id)
        {
            try
            {
                return notesRL.Color(Color, id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
