using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Domain;
using Domain.DTOs;

namespace Application.Services.EmailNotificationServices
{
    public interface IEmailNotificationService: IEntityBaseRepository<EmailNotification,EmailNotificationDto>
    {
        
    }
}