using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Models;
using SocialMediaDashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Logic.Services
{
    /// <inheritdoc cref="IAccountService"/>
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IRepository<Account> accountRepository,
                            IMapper mapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<bool> AddAccountAsync(string userId, string name, AccountType accountType)
        {
            var canCreateAccount = await CanUserCreateAccount(userId, name, accountType);
            if (!canCreateAccount)
            {
                return false;
            }

            var account = new Account
            {
                Name = name,
                Type = accountType,
                UserId = userId
            };

            await _accountRepository.AddAsync(account);
            await _accountRepository.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AccountDto>> GetAllUserAccountsAsync(string userId)
        {
            var account = await _accountRepository.GetAllWithoutTracking()
                .Where(a => a.UserId == userId)
                .ToListAsync();

            var mediaDto = _mapper.Map<List<AccountDto>>(account);

            return mediaDto;
        }

        /// <inheritdoc/>
        public async Task<AccountDto> GetAccountAsync(int id)
        {
            var account = await _accountRepository.GetAllWithoutTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            var accountDto = _mapper.Map<AccountDto>(account);

            return accountDto;
        }

        /// <inheritdoc/>
        public async Task<bool> AccountExistAsync(int id)
        {
            var account = await _accountRepository.GetEntityAsync(m => m.Id == id);
            if (account == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> CanUserCreateAccount(string userId, string name, AccountType accountType)
        {
            var selectedAccount = await _accountRepository.GetAllWithoutTracking()
                .Where(a => a.UserId == userId)
                .FirstOrDefaultAsync(a => a.Name == name && a.Type == accountType);

            if (selectedAccount != null)
            {
                return false;
            }

            return true;
        }
    }
}
