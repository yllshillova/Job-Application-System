using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.EmailNotificationServices;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EmailNotificationsController : BaseApiController
    {
        private readonly IEmailNotificationService _service;
        public EmailNotificationsController(IEmailNotificationService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmailnotifications()
        {
            return HandleResult(await _service.GetAll());
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmailnotificationById(Guid id)
        {

            return HandleResult(await _service.GetById(id));
        }

        [Authorize(Roles = "Admin,Entrepreneur")]
        [HttpPost]
        public async Task<IActionResult> CreateEmailNotification(EmailNotificationDto emailNotificationDto)
        {

            return HandleResult(await _service.Add(emailNotificationDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> EditEmailNotification(Guid id, EmailNotificationDto emailNotificationDto)
        {
            emailNotificationDto.Id = id;
            
            return HandleResult(await _service.Update(id, emailNotificationDto));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailNotification(Guid id)
        {

            return HandleResult(await _service.Delete(id));
        }

       
    }
}