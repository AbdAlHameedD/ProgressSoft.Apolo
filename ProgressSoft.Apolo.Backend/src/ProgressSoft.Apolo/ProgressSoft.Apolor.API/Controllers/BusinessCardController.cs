﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProgressSoft.Apolo.Application;

namespace ProgressSoft.Apolo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessCardController : Controller
    {
        private readonly IBusinessCardService _businessCardService;
        private readonly ICommandHandler<AddBusinessCardCommand, Result<BusinessCardModel>> _addBusinessCardCommandHandler;

        public BusinessCardController(IBusinessCardService businessCardService, ICommandHandler<AddBusinessCardCommand, Result<BusinessCardModel>> addBusinessCardCommandHandler)
        {
            _businessCardService = businessCardService;
            _addBusinessCardCommandHandler = addBusinessCardCommandHandler;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody] BusinessCardFilter filter)
        {
            try
            {
                return Ok(_businessCardService.GetAll(filter));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] AddBusinessCardCommand command)
        {
            try
            {
                return Ok(_addBusinessCardCommandHandler.Handle(command));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                return Ok(_businessCardService.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] BusinessCardModel model)
        {
            try
            {
                return Ok(_businessCardService.Edit(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_businessCardService.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}