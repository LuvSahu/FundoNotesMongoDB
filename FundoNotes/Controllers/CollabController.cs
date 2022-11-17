using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;

        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;

        }

        [Authorize]
        [HttpPost]
        [Route("AddCollab")]
        public ActionResult AddCollab(CollabModel addcollab)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = this.collabBL.AddCollab(addcollab);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collab Added Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collab Added failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCollab")]
        public ActionResult DeleteCollab(string id)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = collabBL.DeleteCollab(id);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Collab deleted Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collab deleted failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetallCollab")]
        public IActionResult GetAllBook()
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                IEnumerable<CollabModel> ifExists = this.collabBL.GetAllCollab();
                if (ifExists != null)
                {
                    return this.Ok(new { Status = true, Message = "Collab Retrived Successfully", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collab not Found" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
