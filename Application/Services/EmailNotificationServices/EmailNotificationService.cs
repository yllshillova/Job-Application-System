using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Persistence;

namespace Application.Services.EmailNotificationServices
{
    public class EmailNotificationService : EntityBaseRepository<EmailNotification, EmailNotificationDto>, IEmailNotificationService
    {
        public EmailNotificationService(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}