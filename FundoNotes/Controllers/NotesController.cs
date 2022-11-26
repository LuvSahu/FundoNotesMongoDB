using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;


        public NotesController(INotesBL notesBL ) // Constructor
        {
            this.notesBL = notesBL;
            

        }
        [Authorize]
        [HttpPost] 
        [Route("CreateNotes")]
        public IActionResult CreationNotes(NotesModel createnotes)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = notesBL.CreateNotes(createnotes,userid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Created Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Notes creation failed" });
                }
            }
            catch (System.Exception )
            {
                //_logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetallNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                IEnumerable<NotesModel> ifExists = notesBL.GetAllNotes();
                if (ifExists != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Retrived Successfully", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Notes not Found" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpPut("UpdateNotes")]
        public IActionResult updateNotes(NotesModel addnote, string id)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = notesBL.UpdateNotes(addnote,id,userid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Updated Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to Update note"
                    });
                }
            }
            catch (Exception )
            {
                //_logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpDelete("DeleteNotes")]

        public IActionResult DeleteNotes(string id)
        {
            try
            {
                if (notesBL.DeleteNote(id))
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Deleted Successfully"
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to delete note"
                    });
                }
            }
            catch (Exception)
            {
                //_logger.LogError(ex.ToString());
                throw;
            }
        }



        [Authorize]
        [HttpPut("Pin")]
        public IActionResult pinnedornot(string id)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = notesBL.PinnedORNot(id,userid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Notes Pinned ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Notes Unpinned "
                    });
                }
            }
            catch (Exception ex)
            {
                //        _logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpPut("Archive")]
        public IActionResult Archive(string id)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = notesBL.Archive(id,userid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Note Archived Successfully ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Note Unarchived"
                    });
                }
            }
            catch (Exception)
            {
               // _logger.LogError(ex.ToString());
                throw;
            }
        }


        [Authorize]
        [HttpPut("Trash")]
        public IActionResult Trash(string id)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = notesBL.Trash(id,userid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Note Trash Successfully ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Note UnTrashed"
                    });
                }
            }
            catch (Exception)
            {
                //_logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(string id, IFormFile img)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = notesBL.UploadImage(id, img,userid);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {
                //_logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpPut("Color")]
        public ActionResult Color(string id, string Color)
        {
            try
            {
                //long ID = Convert.ToInt32(User.Claims.All(x => x.Type == "UserId"));
                var result = notesBL.Color(Color,id);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Color Changed Successfully", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Unable to Change color" });
                }
            }
            catch (Exception)
            {
                //_logger.LogError(ex.ToString());
                throw;
            }

        }

        //[Authorize]
        //[HttpGet("redis")]
        //public async Task<IActionResult> GetAllNotesUsingRedisCache()
        //{
        //    var cacheKey = "NotesList";
        //    string serializedNotesList;
        //    var NotesList = new List<NotesModel>();
        //    var redisNotesList = await distributedCache.GetAsync(cacheKey);
        //    if (redisNotesList != null)
        //    {
        //        serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
        //        NotesList = JsonConvert.DeserializeObject<List<NotesModel>>(serializedNotesList);
        //    }
        //    else
        //    {
        //        NotesList = await fundoContext.NotesTable.ToListAsync();
        //        serializedNotesList = JsonConvert.SerializeObject(NotesList);
        //        redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
        //        var options = new DistributedCacheEntryOptions()
        //            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
        //            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        //        await distributedCache.SetAsync(cacheKey, redisNotesList, options);
        //    }
        //    return Ok(NotesList);
        //}

    }
}
