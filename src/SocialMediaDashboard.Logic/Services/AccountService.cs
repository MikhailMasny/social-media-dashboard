﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Extensions;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.Common.Models;
using SocialMediaDashboard.Common.Resources;
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

        public async Task<bool> AccountExistAsync(int id)
        {
            var account = await _accountRepository.GetEntityAsync(m => m.Id == id);
            if (account == null)
            {
                return false;
            }

            return true;
        }

        public async Task<AccountDto> GetAccountAsync(int id)
        {
            var account = await _accountRepository.GetAllWithoutTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<bool> AddAccountByUserIdAsync(string userId, string accountName, AccountType accountType)
        {
            var canCreateAccount = await CanUserCreateAccountAsync(userId, accountName, accountType);
            if (!canCreateAccount)
            {
                return false;
            }

            var account = new Account
            {
                Name = accountName,
                Type = accountType,
                UserId = userId
            };

            await _accountRepository.AddAsync(account);
            await _accountRepository.SaveChangesAsync();

            return true;
        }

        public async Task<(AccountDto accountDto, AccountResult accountResult)> GetAccountByUserIdAsync(string userId, string userRole, int accountId)
        {
            AccountResult accountResult;

            var canUserGetAccount = await CanUserInteractWithAccountAsync(userId, userRole, accountId);
            if (!canUserGetAccount)
            {
                accountResult = new AccountResult
                {
                    Result = false,
                    Message = AccountResource.Denied,
                };

                return (null, accountResult);
            }

            var account = await _accountRepository.GetEntityAsync(account => account.Id == accountId);
            if (account is null)
            {
                accountResult = new AccountResult
                {
                    Result = false,
                    Message = AccountResource.NotFound,
                };

                return (null, accountResult);
            }

            var accountDto = _mapper.Map<AccountDto>(account);
            accountResult = new AccountResult
            {
                Result = true,
                Message = AccountResource.Successful,
            };

            return (accountDto, accountResult);
        }

        public async Task<IEnumerable<AccountDto>> GetAllUserAccountsAsync(string userId)
        {
            var accounts = await _accountRepository.GetAllWithoutTracking()
                .Where(account => account.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<AccountDto>>(accounts);
        }

        public async Task<AccountResult> DeleteAccountByUserIdAsync(string userId, string userRole, int accountId)
        {
            var canUserDeleteAccount = await CanUserInteractWithAccountAsync(userId, userRole, accountId);
            if (!canUserDeleteAccount)
            {
                return new AccountResult
                {
                    Result = false,
                    Message = AccountResource.Denied,
                };
            }

            var account = await _accountRepository.GetEntityAsync(account => account.Id == accountId);
            if (account is null)
            {
                return new AccountResult
                {
                    Result = false,
                    Message = AccountResource.NotFound,
                };
            }

            _accountRepository.Delete(account);
            await _accountRepository.SaveChangesAsync();

            return new AccountResult
            {
                Result = true,
                Message = string.Empty,
            };
        }

        public async Task<AccountResult> UpdateAccountByUserIdAsync(string userId, string userRole, int accountId, string accountName, AccountType accountType)
        {
            var canUserDeleteAccount = await CanUserInteractWithAccountAsync(userId, userRole, accountId);
            if (!canUserDeleteAccount)
            {
                return new AccountResult
                {
                    Result = false,
                    Message = AccountResource.Denied,
                };
            }

            var account = await _accountRepository.GetEntityAsync(account => account.Id == accountId);
            if (account is null)
            {
                return new AccountResult
                {
                    Result = false,
                    Message = AccountResource.NotFound,
                };
            }

            account.Name = accountName;
            account.Type = accountType;
            _accountRepository.Update(account);
            await _accountRepository.SaveChangesAsync();

            return new AccountResult
            {
                Result = true,
                Message = string.Empty,
            };
        }

        private async Task<bool> CanUserCreateAccountAsync(string userId, string name, AccountType accountType)
        {
            var selectedAccount = await _accountRepository.GetAllWithoutTracking()
                .FirstOrDefaultAsync(account => account.UserId == userId
                    && account.Name == name
                    && account.Type == accountType);

            if (selectedAccount != null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> CanUserInteractWithAccountAsync(string userId, string userRole, int accountId)
        {
            // TODO: change it
            if (RoleExtension.IsAdmin(userRole))
            {
                return true;
            }

            var selectedAccount = await _accountRepository.GetAllWithoutTracking()
                .FirstOrDefaultAsync(account => account.UserId == userId && account.Id == accountId);

            if (selectedAccount is null)
            {
                return false;
            }

            return true;
        }
    }
}
