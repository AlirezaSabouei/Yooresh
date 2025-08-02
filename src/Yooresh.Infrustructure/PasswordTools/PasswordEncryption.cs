﻿using Microsoft.AspNetCore.Identity;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;

namespace Yooresh.Infrastructure.PasswordTools;

public class PasswordEncryption<TBaseEntity>(IPasswordHasher<BaseEntity> passwordHasher) : 
    IPasswordEncryption<BaseEntity> where TBaseEntity : BaseEntity
{
    private readonly IPasswordHasher<BaseEntity> _passwordHasher = passwordHasher;

    public string HashPassword(BaseEntity baseEntity,string password)
    {
        return _passwordHasher.HashPassword(baseEntity, password);
    }

    public bool VerifyPassword(BaseEntity baseEntity,string hashedPassword, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(baseEntity, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}
