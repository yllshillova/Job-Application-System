using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.ApplicationServices;
using Application.Services.CompanyServices;
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

        public MainService(IApplicationService applicationService,IJobPostService jobPostService,
         ICompanyService companyService, IJobSeekerService jobSeekerService, IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
            _applicationService = applicationService;
            _companyService =companyService;
            _jobPostService = jobPostService;
            _jobSeekerService = jobSeekerService;
        }
        
    }
}