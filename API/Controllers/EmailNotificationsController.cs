using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EmailNotificationsController : BaseApiController
    {
        private readonly MainService _service;
        public EmailNotificationsController(MainService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmailnotifications()
        {
            return HandleResult(await _service._emailNotificationService.GetAll());
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetEmailnotificationById(Guid id)
        {

            return Ok(await _service._emailNotificationService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmailNotification(EmailNotificationDto emailNotificationDto)
        {

            return HandleResult(await _service._emailNotificationService.Add(emailNotificationDto));
        }

        [HttpPut]
        public async Task<IActionResult> EditEmailNotification(Guid id, EmailNotificationDto emailNotificationDto)
        {
            emailNotificationDto.Id = id;
            await _service._emailNotificationService.Update(id, emailNotificationDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailNotification(Guid id)
        {

            return HandleResult(await _service._emailNotificationService.Delete(id));
        }

       
    }
}