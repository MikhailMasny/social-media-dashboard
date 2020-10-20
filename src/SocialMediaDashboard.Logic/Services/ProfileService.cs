using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;
using SocialMediaDashboard.Domain.Resources;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IProfileService"/>
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Profile> _profileRepository;
        private readonly AutoMapper.IMapper _mapper;

        public ProfileService(IRepository<Profile> profileRepository,
            AutoMapper.IMapper mapper)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateAsync(string userId, string name)
        {
            var profile = new Profile
            {
                Name = name,
                UserId = userId,
            };

            await _profileRepository.CreateAsync(profile);
            await _profileRepository.SaveChangesAsync();
        }

        public async Task<(ProfileDto profileDto, OperationResult operationResult)> GetByUserIdAsync(string userId)
        {
            OperationResult operationResult;

            var profile = await _profileRepository
                .GetEntityWithoutTrackingAsync(profile => profile.UserId == userId);

            if (profile is null)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = ProfileResource.NotFoundSpecified,
                };

                return (new ProfileDto(), operationResult);
            }

            var profileDto = _mapper.Map<ProfileDto>(profile);
            operationResult = new OperationResult
            {
                Result = true,
                Message = CommonResource.Successful,
            };

            return (profileDto, operationResult);
        }

        public async Task<(ProfileDto profileDto, OperationResult operationResult)> UpdateAsync(ProfileDto profileDto)
        {
            profileDto = profileDto ?? throw new ArgumentNullException(nameof(profileDto));

            OperationResult operationResult;

            var profile = await _profileRepository
                .GetEntityAsync(profile => profile.UserId == profileDto.UserId);

            if (profile is null)
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = ProfileResource.NotFoundSpecified,
                };

                return (new ProfileDto(), operationResult);
            }

            static bool CompareAndUpdate(Profile profile, ProfileDto profileDto)
            {
                bool update = false;

                if (profile.Name != profileDto.Name)
                {
                    profile.Name = profileDto.Name;
                    update = true;
                }

                if (profile.Gender != profileDto.Gender)
                {
                    profile.Gender = profileDto.Gender;
                    update = true;
                }

                if (profile.BirthDate != profileDto.BirthDate)
                {
                    profile.BirthDate = profileDto.BirthDate;
                    update = true;
                }

                if (profile.Country != profileDto.Country)
                {
                    profile.Country = profileDto.Country;
                    update = true;
                }

                if (profile.Avatar != profileDto.Avatar)
                {
                    profile.Avatar = profileDto.Avatar;
                    update = true;
                }

                return update;
            }

            if (!CompareAndUpdate(profile, profileDto))
            {
                operationResult = new OperationResult
                {
                    Result = false,
                    Message = ProfileResource.SameData,
                };

                return (new ProfileDto(), operationResult);
            }

            _profileRepository.Update(profile);
            await _profileRepository.SaveChangesAsync();

            var updatedProfileDto = _mapper.Map<ProfileDto>(profile);
            operationResult = new OperationResult
            {
                Result = true,
                Message = SubscriptionResource.Updated,
            };

            return (updatedProfileDto, operationResult);
        }
    }
}
