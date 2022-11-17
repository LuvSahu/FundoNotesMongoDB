using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        private readonly IMongoCollection<NotesModel> Notes;

        private readonly IConfiguration configuration;

        private readonly IConfiguration cloudinaryEntity;


        public NotesRL(IConfiguration configuration, IConfiguration cloudinaryEntity, IDBSetting db)
        {

            this.configuration = configuration;
            this.cloudinaryEntity = cloudinaryEntity;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);
            Notes = database.GetCollection<NotesModel>("Notes");
        }
        public NotesModel CreateNotes(NotesModel addnotes,string userid)
        {
            try
            {
                var ifExists = this.Notes.Find(x => x.NotesID == addnotes.NotesID && x.UserID == userid).SingleOrDefault();
                if (ifExists == null)
                {
                    this.Notes.InsertOne(addnotes);
                    return addnotes;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<NotesModel> GetAllNotes()
        {
            return Notes.Find(FilterDefinition<NotesModel>.Empty).ToList();
        }

        public NotesModel UpdateNotes(NotesModel editnotes, string id,string userid)
        {
            try
            {
                var ifExists = this.Notes.Find(x => x.NotesID == id && x.UserID == userid).FirstOrDefault();
                if (ifExists != null)
                {

                    this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Title, editnotes.Title)
                        .Set(x => x.Description, editnotes.Description)
                        .Set(x => x.Color, editnotes.Color)
                        .Set(x => x.Image, editnotes.Image)
                        .Set(x => x.Archive, editnotes.Archive)
                        .Set(x => x.Pin, editnotes.Pin));

                    return ifExists;
                }
                else
                {
                    this.Notes.InsertOne(editnotes);
                    return editnotes;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteNote(string id)
        {

            try
            {
                var ifExists = this.Notes.FindOneAndDelete(x => x.NotesID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public NotesModel PinnedORNot(string id,string userid)
        {
            try
            {
                var ifExists = this.Notes.Find(x => x.NotesID == id && x.UserID == userid).SingleOrDefault();
                if (ifExists.Pin == true)
                {
                    ifExists.Pin = false;
                    this.Notes.UpdateOne(x => x.NotesID == id,Builders<NotesModel>.Update.Set(x => x.Pin, false));
                    //var result = Builders<BsonDocument>.Update.Set("Pin", false);
                    //var value = this.Notes.FindOneAndUpdate(x => x.NotesID == id,result);
                    // this.Notes.UpdateOne({ _id: id},{ $Set: { Pin:false} });
                    return ifExists;
                }
                ifExists.Pin = true;
                this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Pin, true));
                //this.Notes.UpdateOne(ifExists);
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesModel Archive(string id,string userid)
        {
            try
            {
                var result = this.Notes.Find(x => x.NotesID == id && x.UserID == userid).SingleOrDefault();
                if (result.Archive == true)
                {
                    result.Archive = false;
                    this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Archive, false));
                    return result;
                }
                result.Archive = true;
                this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Archive, true));
                return null;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public NotesModel Trash(string id,string userid)
        {
            try
            {
                var result = this.Notes.Find(x => x.NotesID == id && x.UserID == userid).SingleOrDefault();
                if (result.Trash == true)
                {
                    result.Trash = false;
                    this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Trash, false));
                    return result;
                }
                result.Trash = true;
                this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Trash, true));
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UploadImage(string id, IFormFile img,string userid)
        {
            try
            {
                var result = this.Notes.Find(x => x.NotesID == id && x.UserID == userid).SingleOrDefault();
                if (result != null)
                {
                    Account cloudaccount = new Account(
                         cloudinaryEntity["CloudinarySettings:cloudName"],
                         cloudinaryEntity["CloudinarySettings:apiKey"],
                         cloudinaryEntity["CloudinarySettings:apiSecret"]
                         );

                    Cloudinary cloudinary = new Cloudinary(cloudaccount);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Image,imagePath));
                    return "Image upload SuccessFully";
                }
                return null;
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
                var result = this.Notes.Find(x => x.NotesID == id).SingleOrDefault();
                if (result != null)
                {
                    result.Color = Color;
                    this.Notes.UpdateOne(x => x.NotesID == id, Builders<NotesModel>.Update.Set(x => x.Color, Color));
                    return result;
                }
                else
                {
                    return null; ;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
