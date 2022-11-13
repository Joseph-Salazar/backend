﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Core;
using Application.Dto.Company;
using Application.Dto.Postulant;
using Application.MainModule.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using Domain.MainModule.Validations.CompanyValidations;
using Hangfire.MemoryStorage.Dto;
using Infrastructure.CrossCutting.Constants;
using Infrastructure.CrossCutting.CustomExections;
using Infrastructure.CrossCutting.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application.MainModule;

public class CompanyAppService : BaseAppService, ICompanyAppService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IConfiguration _configuration;

    public CompanyAppService(
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _companyRepository = serviceProvider.GetService<ICompanyRepository>() ?? throw new InvalidOperationException();
        _configuration = serviceProvider.GetService<IConfiguration>() ?? throw new InvalidOperationException();
    }

    public async Task<CompanyDto> GetById(int companyId)
    {
        if (companyId == 0)
            throw new WarningException(MessageConst.InvalidSelection);

        var companyDto = await _companyRepository
            .Find(p => p.Id == companyId)
            .ProjectTo<CompanyDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (companyDto is null)
            throw new WarningException(MessageConst.InvalidSelection);

        return companyDto;
    }

    public async Task<string> Add(AddCompanyDto companyDto)
    {
        var newCompany = Mapper.Map<Company>(companyDto);
        await _companyRepository.AddAsync(newCompany, new AddCompanyValidator(_companyRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<string> Update(UpdateCompanyDto companyDto)
    {
        var companyDomain = await _companyRepository.GetAsync(companyDto.Id);

        if (companyDomain is null)
            throw new WarningException(MessageConst.InvalidSelection);

        Mapper.Map(companyDto, companyDomain);

        await _companyRepository.UpdateAsync(companyDomain, new AddCompanyValidator(_companyRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<CompanyDto> Register(RegisterCompanyDto registerPostulantDto)
    {
        Company postulantToDomain = new Company();

        if (_companyRepository.GetAll().Any(x => x.Email == registerPostulantDto.Email))
        {
            throw new WarningException(MessageConst.EmailAlreadyExists);
        }

        CreatePasswordHash(registerPostulantDto.Password,
            out byte[] passwordHash,
            out byte[] passwordSalt);

        var postulantRegister = new CompanyDto()
        {
            Email = registerPostulantDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RoleId = registerPostulantDto.RoleId

        };

        postulantToDomain = Mapper.Map<CompanyDto, Company>(postulantRegister);

        postulantToDomain.BannerPicture = "";
        postulantToDomain.CompanyName = "";
        postulantToDomain.Description = "";
        postulantToDomain.ProfilePicture = "";
        postulantToDomain.WebsiteUrl = "";

        await _companyRepository.AddAsync(postulantToDomain);
        await UnitOfWork.SaveChangesAsync();

        return postulantRegister;
    }

    public async Task<LoginCompanyResponseDto> Login(LoginCompanyDto registerCompanyDto)
    {
        var loginCompany =
            await _companyRepository.GetAll().FirstOrDefaultAsync(u => u.Email == registerCompanyDto.Email);

        if (loginCompany is null)
        {
            throw new WarningException(MessageConst.InvalidLogin);
        }

        if (!VerifyPasswordHash(registerCompanyDto.Password, loginCompany.PasswordHash, loginCompany.PasswordSalt))
        {
            throw new WarningException(MessageConst.InvalidLogin);
        }

        LoginCompanyResponseDto response = new LoginCompanyResponseDto();

        string token = CreateToken(loginCompany);

        response.Company = Mapper.Map<CompanyDto>(loginCompany);
        response.Token = token;

        return response;
    }

    private string CreateToken(Company postulant)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, postulant.Email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash
                (System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash
                (System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}