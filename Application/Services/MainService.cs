using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.ApplicationServices;
using Application.Services.CompanyServices;
using Application.Services.JobPostServices;
using Application.Services.UserServices;

namespace Application.Services
{
    public class MainService
    {
        public readonly IUserService _userService;
        public readonly IJobPostService _jobPostService;
        public readonly ICompanyService _companyService;
        public readonly IApplicationService _applicationService;


        public MainService(IApplicationService applicationService,IJobPostService jobPostService,
         ICompanyService companyService, IUserService userService)
        {
            _applicationService = applicationService;
            _companyService =companyService;
            _jobPostService = jobPostService;
            _userService = userService;
        }
        
    }
}