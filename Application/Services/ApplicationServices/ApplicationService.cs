using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.ApplicationServices
{
    public class ApplicationService : EntityBaseRepository<ApplicationEntity, ApplicationDto>, IApplicationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public ApplicationService(DataContext context, IMapper mapper, IEmailService emailService) : base(context, mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
            _context = context;
        }


        public async Task<Result<ApplicationDto>> SubmitApplication(Guid jobSeekerId, Guid jobPostId, IFormFile resume)
        {
            var jobSeeker = await _context.Users.OfType<JobSeeker>()
                           .Include(js => js.Educations)
                           .Include(js => js.Experiences)
                           .Include(js => js.Skills)
                           .FirstOrDefaultAsync(js => js.Id == jobSeekerId);

            var jobPost = await _context.JobPosts
            .Include(j => j.Recruiter)
            .ThenInclude(r => r.Company)
                .FirstOrDefaultAsync(jp => jp.Id == jobPostId);

            if (jobSeeker == null || jobPost == null)
            {
                return Result<ApplicationDto>.Failure(ResultErrorType.NotFound, "JobSeeker or JobPost is missing!");
            }
            if (jobPost.Recruiter == null)
            {
                // Log or handle the situation where one of the objects is null
                return Result<ApplicationDto>.Failure(ResultErrorType.NotFound, "One of the objects is null!");
            }
            if (jobPost.Recruiter.Company == null)
            {
                // Log or handle the situation where one of the objects is null
                return Result<ApplicationDto>.Failure(ResultErrorType.NotFound, "One of the objects is null!");
            }
            if (resume == null)
            {
                return Result<ApplicationDto>.Failure(ResultErrorType.NotFound);
            }
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };

            var validationResult = ValidateFile(resume, allowedExtensions);
            if (!validationResult.IsSuccess)
            {
                return Result<ApplicationDto>.Failure(validationResult.ErrorType, "Validation of the resume is failed try again!");
            }


            var resumeStorageDto = new ResumeStorageDto
            {
                FileName = resume.FileName,
                FileData = await GetFileBytes(resume)
            };

            var resumeStorageEntity = _mapper.Map<ResumeStorage>(resumeStorageDto);

            if (resumeStorageEntity == null)
            {
                return Result<ApplicationDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }

            var applicationEntity = new ApplicationEntity
            {
                Status = "Submitted",
                DateSubmitted = DateTime.UtcNow,
                JobSeekerId = jobSeekerId,
                JobPostId = jobPostId,
                ResumeFileId = resumeStorageEntity.Id,
                ResumeFile = resumeStorageEntity
            };

            _context.Applications.Add(applicationEntity);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<ApplicationDto>.Failure(ResultErrorType.BadRequest, "Something went wrong while adding the application!");

            var applicationDto = _mapper.Map<ApplicationDto>(applicationEntity);

            if (applicationDto == null)
            {
                return Result<ApplicationDto>.Failure(ResultErrorType.BadRequest, "Problem while mapping from/to entity");
            }
            var sendapp = await _emailService.SendApplicationResponseSMTPAsync(jobSeeker.Email, jobPost.Recruiter.Company.Email);
            if (string.IsNullOrEmpty(sendapp))
            {
                // Log or handle the situation where the email service fails to send the email
                return Result<ApplicationDto>.Failure(ResultErrorType.BadRequest, "Failed to send application response email");
            }
            return Result<ApplicationDto>.Success(applicationDto);
        }

        private Result<string> ValidateFile(IFormFile file, string[] allowedExtensions)
        {
            if (file == null || file.Length == 0)
            {
                return Result<string>.Failure(ResultErrorType.NotFound, "File is missing!");
            }

            var fileExtension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(fileExtension.ToLower()))
            {
                return Result<string>.Failure(ResultErrorType.BadRequest, $"This type of file {fileExtension} is not allowed!");
            }

            return Result<string>.Success("File is valid!");
        }

        private async Task<byte[]> GetFileBytes(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
}