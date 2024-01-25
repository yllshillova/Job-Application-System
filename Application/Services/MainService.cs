using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services.AccountServices;
using Application.Services.ApplicationServices;
using Application.Services.CompanyServices;
using Application.Services.EmailNotificationServices;
using Application.Services.EntrepreneurServices;
using Application.Services.JobPostServices;
using Application.Services.JobSeekerServices;
using Application.Services.RecruiterServices;

namespace Application.Services
{
    public class MainService
    {
        public readonly IJobSeekerService _jobSeekerService;
        public readonly IJobPostService _jobPostService;
        public readonly ICompanyService _companyService;
        public readonly IApplicationService _applicationService;
        public readonly IRecruiterService _recruiterService;
        public readonly IEntrepreneurService _entrepreneurService;
        public readonly IEmailNotificationService _emailNotificationService;
        public readonly IAccountService _accountService;
        public readonly IEmailService _emailService;
        public MainService(IApplicationService applicationService,IJobPostService jobPostService,
         ICompanyService companyService, IJobSeekerService jobSeekerService, IRecruiterService recruiterService,
         IEntrepreneurService entrepreneurService, IEmailNotificationService emailNotificationService, IAccountService accountService,
         IEmailService emailService)
        {
            _emailNotificationService = emailNotificationService;
            _entrepreneurService = entrepreneurService;
            _recruiterService = recruiterService;
            _applicationService = applicationService;
            _companyService =companyService;
            _jobPostService = jobPostService;
            _jobSeekerService = jobSeekerService;
            _accountService = accountService;
            _emailService = emailService;
        }
        
    }
}