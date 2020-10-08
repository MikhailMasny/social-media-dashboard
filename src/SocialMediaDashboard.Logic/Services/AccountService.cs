using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Interfaces;
using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;
using SocialMediaDashboard.Domain.Enums;
using SocialMediaDashboard.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Infrastructure.Services
{
    /// <inheritdoc cref="IAccountService"/>
    //public class AccountService : IAccountService
    //{
    //    private readonly IRepository<Account> _accountRepository;
    //    private readonly IRepository<Subscription> _subscriptionRepository;
    //    private readonly IMapper _mapper;

    //    public AccountService(IRepository<Account> accountRepository,
    //                          IRepository<Subscription> subscriptionRepository,
    //                          IMapper mapper)
    //    {
    //        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    //        _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
    //        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    //    }

    //    public async Task<AccountDto> GetAccountAsync(int id)
    //    {
    //        var account = await _accountRepository.GetEntityAsync(account => account.Id == id);

    //        return _mapper.Map<AccountDto>(account);
    //    }

    //    public async Task<AccountResult> ValidateAccountExistAsync(int id, string userId)
    //    {
    //        var account = await _accountRepository.GetEntityAsync(account => account.Id == id && account.UserId == userId);
    //        if (account == null)
    //        {
    //            return new AccountResult
    //            {
    //                Result = false,
    //                Message = SubscriptionResource.AccountNotFound,
    //            };
    //        }

    //        return new AccountResult
    //        {
    //            Result = true,
    //            Message = string.Empty,
    //        };
    //    }

    //    public async Task<(AccountDto accountDto, AccountResult accountResult)> CreateAccountByUserIdAsync(string userId, string accountName, AccountKind accountType)
    //    {
    //        AccountResult accountResult;

    //        var canCreateAccount = await CanUserCreateOrUpdateAccountAsync(userId, accountName, accountType);
    //        if (!canCreateAccount)
    //        {
    //            accountResult = new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.Exception,
    //            };

    //            return (null, accountResult);
    //        }

    //        //var account = new Account
    //        //{
    //        //    Name = accountName,
    //        //    Type = accountType,
    //        //    UserId = userId
    //        //};

    //        //await _accountRepository.AddAsync(account);
    //        //await _accountRepository.SaveChangesAsync();

    //        //var accountDto = _mapper.Map<AccountDto>(account);
    //        accountResult = new AccountResult
    //        {
    //            Result = true,
    //            Message = AccountResource.Added,
    //        };

    //        return (accountDto, accountResult);
    //    }

    //    public async Task<(AccountDto accountDto, AccountResult accountResult)> GetAccountByUserIdAsync(string userId, int accountId)
    //    {
    //        AccountResult accountResult;

    //        var account = await _accountRepository.GetEntityAsync(account => account.UserId == userId && account.Id == accountId);
    //        if (account is null)
    //        {
    //            accountResult = new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.NotFound,
    //            };

    //            return (null, accountResult);
    //        }

    //        var accountDto = _mapper.Map<AccountDto>(account);
    //        accountResult = new AccountResult
    //        {
    //            Result = true,
    //            Message = AccountResource.Successful,
    //        };

    //        return (accountDto, accountResult);
    //    }

    //    public async Task<(IEnumerable<AccountDto> accountDtos, AccountResult accountResult)> GetAllUserAccountsAsync(string userId)
    //    {
    //        AccountResult accountResult;

    //        var accounts = await _accountRepository.GetAllWithoutTracking()
    //            .Where(account => account.UserId == userId)
    //            .ToListAsync();

    //        if (!accounts.Any())
    //        {
    //            accountResult = new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.NotFound,
    //            };

    //            return (null, accountResult);
    //        }

    //        var accountDtos = _mapper.Map<List<AccountDto>>(accounts);
    //        accountResult = new AccountResult
    //        {
    //            Result = true,
    //            Message = AccountResource.Successful,
    //        };

    //        return (accountDtos, accountResult);
    //    }

    //    public async Task<AccountResult> UpdateAccountByUserIdAsync(string userId, int accountId, string accountName, AccountKind accountType)
    //    {
    //        var canUserUpdateAccount = await CanUserInteractWithAccountAsync(userId, accountId);
    //        if (!canUserUpdateAccount)
    //        {
    //            return new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.Denied,
    //            };
    //        }

    //        var canCreateAccount = await CanUserCreateOrUpdateAccountAsync(userId, accountName, accountType);
    //        if (!canCreateAccount)
    //        {
    //            return new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.Exception,
    //            };
    //        }

    //        var account = await _accountRepository.GetEntityAsync(account => account.Id == accountId);
    //        if (account is null)
    //        {
    //            return new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.NotFound,
    //            };
    //        }

    //        account.Name = accountName;
    //        account.Type = accountType;
    //        _accountRepository.Update(account);
    //        await _accountRepository.SaveChangesAsync();

    //        return new AccountResult
    //        {
    //            Result = true,
    //            Message = string.Empty,
    //        };
    //    }

    //    public async Task<AccountResult> DeleteAccountByUserIdAsync(string userId, int accountId)
    //    {
    //        var account = await _accountRepository.GetEntityAsync(account => account.Id == accountId);
    //        if (account is null)
    //        {
    //            return new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.NotFound,
    //            };
    //        }

    //        var canUserDeleteAccount = await CanUserInteractWithAccountAsync(userId, accountId);
    //        if (!canUserDeleteAccount)
    //        {
    //            return new AccountResult
    //            {
    //                Result = false,
    //                Message = AccountResource.Denied,
    //            };
    //        }

    //        _accountRepository.Delete(account);
    //        await _accountRepository.SaveChangesAsync();

    //        return new AccountResult
    //        {
    //            Result = true,
    //            Message = string.Empty,
    //        };
    //    }

    //    private async Task<bool> CanUserCreateOrUpdateAccountAsync(string userId, string name, AccountKind accountType)
    //    {
    //        var selectedAccount = await _accountRepository.GetAllWithoutTracking()
    //            .FirstOrDefaultAsync(account => account.UserId == userId
    //                && account.Name == name
    //                && account.Type == accountType);

    //        return selectedAccount is null;
    //    }

    //    private async Task<bool> CanUserInteractWithAccountAsync(string userId, int accountId)
    //    {
    //        var selectedAccount = await _accountRepository.GetAllWithoutTracking()
    //            .FirstOrDefaultAsync(account => account.UserId == userId && account.Id == accountId);

    //        return !(selectedAccount is null);
    //    }
    //}
}
