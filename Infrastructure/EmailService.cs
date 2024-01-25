using System.Net;
using System.Net.Mail;
using System.Text;
using Application.Core;
using Application.Interfaces;
using Application.Services;
using Domain;
using Mailjet.Client;
using Mailjet.Client.Resources;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence;

namespace Infrastructure;

public class EmailService : IEmailService
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(DataContext context, IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _context = context;
        _logger = logger;
    }


    public async Task<string> SendApplicationResponseSMTPAsync(string applicantEmail, string companyEmail)
    {
        string companyName = await GetCompanyName(companyEmail);
        string companyEmailAddress = await GetCompanyByEmail(companyEmail);
        string smtpServer = "in-v3.mailjet.com";
        string smtpUsername = _config["Mailjet:ApiKey"];
        string smtpPassword = _config["Mailjet:ApiSecret"];
        int smtpPort = 587;
        string senderEmail = companyEmailAddress;
        try
        {
            // await AddSenderToMailjet(companyEmailAddress, companyName);
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(companyEmailAddress, companyName);
                    mailMessage.To.Add(new MailAddress(applicantEmail));
                    mailMessage.Subject = "Application Received";
                    mailMessage.Body = "Thank you for applying. We have received your application and will get back to you as soon as possible.";
                    mailMessage.IsBodyHtml = true;

                    mailMessage.Bcc.Add(senderEmail);

                    await smtpClient.SendMailAsync(mailMessage);
                    return "Email Sent successfully";
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return "Failed to send email";
        }
    }
    public async Task<string> SendApplicationResponseSMTPMailTrapAsync(string applicantEmail, string companyEmail)
    {
        string companyName = await GetCompanyName(companyEmail);
        string companyEmailAddress = await GetCompanyByEmail(companyEmail);
        string smtpServer = "sandbox.smtp.mailtrap.io";
        string smtpUsername = _config["Mailtrap:Username"];
        string smtpPassword = _config["Mailtrap:Password"];
        int smtpPort = 587;
        string senderEmail = companyEmailAddress;
        try
        {
            // await AddSenderToMailjet(companyEmailAddress, companyName);
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(companyEmailAddress, companyName);
                    mailMessage.To.Add(new MailAddress(applicantEmail));
                    mailMessage.Subject = "Application Received";
                    mailMessage.Body = "Thank you for applying. We have received your application and will get back to you as soon as possible.";
                    mailMessage.IsBodyHtml = true;

                    mailMessage.Bcc.Add(senderEmail);

                    await smtpClient.SendMailAsync(mailMessage);
                    return "Email Sent successfully";
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return "Failed to send email";
        }
    }
    public async Task AddSenderToMailjet(string email, string name)
    {
        try
        {
            // Check if the sender is already verified
            if (await IsSenderVerified(email))
            {
                _logger.LogInformation($"Sender {email} is already verified. Skipping addition.");
                return;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_config["Mailjet:ApiKey"]}:{_config["Mailjet:ApiSecret"]}")));

                var senderPayload = new
                {
                    Email = email,
                    Name = name
                };

                var content = new StringContent(JsonConvert.SerializeObject(senderPayload), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"https://api.mailjet.com/v3/REST/sender", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to add {email} to Mailjet verified senders. Error: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding sender to Mailjet: {ex.Message}");
        }
    }


    public async Task<bool> IsSenderVerified(string email)
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_config["Mailjet:ApiKey"]}:{_config["Mailjet:ApiSecret"]}")));

                var response = await httpClient.GetAsync($"https://api.mailjet.com/v3/REST/sender");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var senders = JsonConvert.DeserializeObject<JObject>(responseBody);

                    if (senders.TryGetValue("Data", out JToken dataToken) && dataToken is JArray dataArray)
                    {
                        // Check if the email is in the list of senders
                        return dataArray.Any(sender => sender["Email"].ToString() == email);
                    }

                    _logger.LogError($"Failed to retrieve verified senders. Response: {responseBody}");
                    return false;
                }

                _logger.LogError($"Failed to retrieve verified senders. Error: {response.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error checking if sender is verified: {ex.Message}");
            return false;
        }
    }


    private async Task<string> GetCompanyByEmail(string email)
    {
        var company = await _context.Companies
        .FirstOrDefaultAsync(c => c.Email.Equals(email));
        if (company == null) return "No company was found with that email";
        return company.Email;

    }
    private async Task<string> GetJobSeekerByEmail(string email)
    {
        var jobseeker = await _context.Users.OfType<JobSeeker>()
        .FirstOrDefaultAsync(j => j.Email.Equals(email));
        if (jobseeker == null) return "No company was found with that email";
        return jobseeker.Email;

    }
    private async Task<string> GetJobSeekerNameByEmail(string email)
    {
        var jobseeker = await _context.Users.OfType<JobSeeker>()
        .FirstOrDefaultAsync(j => j.Email.Equals(email));
        if (jobseeker == null) return "No company was found with that email";
        return jobseeker.UserName;

    }
    private async Task<string> GetCompanyName(string email)
    {
        var company = await _context.Companies
        .FirstOrDefaultAsync(c => c.Email.Equals(email));
        if (company == null) return "No company was found with that name";
        return company.Name;

    }

}
