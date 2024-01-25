using Application.Core;
using MediatR;

namespace Application.Interfaces;

public interface IEmailService
{
    Task<string> SendApplicationResponseSMTPAsync(string applicantEmail,string companyEmail);
    Task<string> SendApplicationResponseSMTPMailTrapAsync(string applicantEmail, string companyEmail);
    Task AddSenderToMailjet(string email, string name);
    Task<bool> IsSenderVerified(string email);
}
