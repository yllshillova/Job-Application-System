using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base;
using Application.Core;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.ApplicationServices
{
    public class ApplicationService : EntityBaseRepository<ApplicationEntity, ApplicationDto>, IApplicationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ApplicationService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<Result<ApplicationDto>> SubmitApplication(Guid jobSeekerId, Guid jobPostId, IFormFile resume, Guid emailNotificationId)
        {
            var jobSeeker = await _context.JobSeekers
                           .Include(js => js.Educations)
                           .Include(js => js.Experiences)
                           .Include(js => js.Skills)
                           .FirstOrDefaultAsync(js => js.Id == jobSeekerId);

            var jobPost = await _context.JobPosts
                .FirstOrDefaultAsync(jp => jp.Id == jobPostId);

            if (jobSeeker == null || jobPost == null)
            {
                return Result<ApplicationDto>.Failure("Job seeker or job post not found.");
            }

            if (resume == null)
            {
                return Result<ApplicationDto>.Failure("Invalid file");
            }
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };

            var validationResult = ValidateFile(resume, allowedExtensions);
            if (!validationResult.IsSuccess)
            {
                return Result<ApplicationDto>.Failure(validationResult.Error);
            }


            var resumeStorageDto = new ResumeStorageDto
            {
                FileName = resume.FileName,
                FileData = await GetFileBytes(resume)
            };

            var resumeStorageEntity = _mapper.Map<ResumeStorage>(resumeStorageDto);

            var applicationEntity = new ApplicationEntity
            {
                Status = "Submitted",
                DateSubmitted = DateTime.UtcNow,
                JobSeekerId = jobSeekerId,
                JobPostId = jobPostId,
                ResumeFileId = resumeStorageEntity.Id,
                ResumeFile = resumeStorageEntity,
                EmailNotificationId = emailNotificationId
            };
            _context.Applications.Add(applicationEntity);
            await _context.SaveChangesAsync();

            // Map to DTO and return the result
            var applicationDto = _mapper.Map<ApplicationDto>(applicationEntity);
            return Result<ApplicationDto>.Success(applicationDto);
        }

        private Result<string> ValidateFile(IFormFile file, string[] allowedExtensions)
        {
            if (file == null || file.Length == 0)
            {
                return Result<string>.Failure("Invalid file");
            }

            var fileExtension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(fileExtension.ToLower()))
            {
                return Result<string>.Failure("Invalid file type");
            }

            return Result<string>.Success("File is valid");
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