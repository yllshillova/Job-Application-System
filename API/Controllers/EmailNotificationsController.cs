using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.EmailNotificationServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EmailNotificationsController : BaseApiController
    {
        private readonly EmailNotificationService _service;
        public EmailNotificationsController(EmailNotificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmailnotifications()
        {
            return HandleResult(await _service.GetAll());
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetEmailnotificationById(Guid id)
        {

            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmailNotification(EmailNotificationDto emailNotificationDto)
        {

            return HandleResult(await _service.Add(emailNotificationDto));
        }

        [HttpPut]
        public async Task<IActionResult> EditEmailNotification(Guid id, EmailNotificationDto emailNotificationDto)
        {
            emailNotificationDto.Id = id;
            await _service.Update(id, emailNotificationDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailNotification(Guid id)
        {

            return HandleResult(await _service.Delete(id));
        }

       
    }
}