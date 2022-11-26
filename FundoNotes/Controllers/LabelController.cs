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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;

        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;

        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public ActionResult AddLabel(LabelModel addlabel)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = this.labelBL.AddLabel(addlabel,userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Added Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Added failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateLabel")]
        public ActionResult UpdateLabel(LabelModel editlabel, string id)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = labelBL.UpdateLabel(editlabel, id,userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Update Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Update failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteLabel")]
        public ActionResult DeleteLabel(string id)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = labelBL.DeleteLabel(id);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Label deleted Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label deleted failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetallLabel")]
        public IActionResult GetAllBook()
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                IEnumerable<LabelModel> ifExists = this.labelBL.GetAllLabel();
                if (ifExists != null)
                {
                    return this.Ok(new { Status = true, Message = "label Retrived Successfully", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label not Found" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        
    }
}
