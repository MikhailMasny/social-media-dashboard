using SocialMediaDashboard.Application.Exceptions;
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

        public ProfileService(IRepository<Profile> profileRepository, AutoMapper.IMapper mapper)
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

        public async Task<ProfileDto> GetByUserIdAsync(string userId)
        {
            var profile = await _profileRepository
                .GetEntityWithoutTrackingAsync(profile => profile.UserId == userId);

            if (profile is null)
            {
                throw new NotFoundException(ProfileResource.NotFoundSpecified);
            }

            return _mapper.Map<ProfileDto>(profile);
        }

        public async Task<ProfileDto> UpdateAsync(ProfileDto profileDto)
        {
            profileDto = profileDto ?? throw new ArgumentNullException(nameof(profileDto));

            var profile = await _profileRepository
                .GetEntityAsync(profile => profile.UserId == profileDto.UserId);

            if (profile is null)
            {
                throw new NotFoundException(ProfileResource.NotFoundSpecified);
            }

            static bool CompareAndUpdate(Profile profile, ProfileDto profileDto)
            {
                bool toUpdate = false;

                if (profile.Name != profileDto.Name)
                {
                    profile.Name = profileDto.Name;
                    toUpdate = true;
                }

                if (profile.Gender != profileDto.Gender)
                {
                    profile.Gender = profileDto.Gender;
                    toUpdate = true;
                }

                if (profile.BirthDate != profileDto.BirthDate)
                {
                    profile.BirthDate = profileDto.BirthDate;
                    toUpdate = true;
                }

                if (profile.Country != profileDto.Country)
                {
                    profile.Country = profileDto.Country;
                    toUpdate = true;
                }

                if (profile.Avatar != profileDto.Avatar)
                {
                    profile.Avatar = profileDto.Avatar;
                    toUpdate = true;
                }

                return toUpdate;
            }

            if (!CompareAndUpdate(profile, profileDto))
            {
                throw new ConflictException(ProfileResource.SameData);
            }

            _profileRepository.Update(profile);
            await _profileRepository.SaveChangesAsync();

            return _mapper.Map<ProfileDto>(profile);
        }
    }
}
