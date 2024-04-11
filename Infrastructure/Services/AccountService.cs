using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Infrastructure.Services;

public class AccountService(UserManager<UserEntity> userManager, DataContext dataContext, IConfiguration configuration)
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly DataContext _dataContext = dataContext;
    private readonly IConfiguration _configuration = configuration;


    public async Task<User> GetUserAsync(ClaimsPrincipal claimsPrincipal)
    {
        var nameIdentifer = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userEntity = await _dataContext.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifer);
        if (userEntity != null)
        {
            return UserFactory.Create(userEntity!);
        }
        return null!;
    }



    public async Task<bool> UpdateBasicInfoAsync(ClaimsPrincipal claimsPrincipal, AccountBasicInfo basicInfo)
    {

        try
        {
            var nameIdentifer = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userEntity = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == nameIdentifer);
            if (userEntity != null)
            {
                userEntity.FirstName = basicInfo.FirstName;
                userEntity.LastName = basicInfo.LastName;
                userEntity.PhoneNumber = basicInfo.PhoneNumber;
                userEntity.Bio = basicInfo.Biography;

                _dataContext.Update(userEntity);
                await _dataContext.SaveChangesAsync();

                return true;
            }
        }
        catch { }
        return false;

    }



    public async Task<bool> UpdateAddressInfoAsync(ClaimsPrincipal claimsPrincipal, AccountAddressInfo adressInfo)
    {

        try
        {
            var nameIdentifer = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userEntity = await _dataContext.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifer);
            if (userEntity!.Address != null)
            {
                userEntity.Address!.AddressLine_1 = adressInfo.AddressLine_1;    
                userEntity.Address!.AddressLine_2 = adressInfo.AddressLine_2;
                userEntity.Address!.PostalCode = adressInfo.PostalCode;
                userEntity.Address!.City = adressInfo.City;
            }
            else
            {
                userEntity.Address = new AddressEntity
                {
                    AddressLine_1 = adressInfo.AddressLine_1,
                    AddressLine_2 = adressInfo.AddressLine_2,
                    PostalCode = adressInfo.PostalCode,
                    City = adressInfo.City,
                };
            }

            _dataContext.Update(userEntity);
            await _dataContext.SaveChangesAsync();

            return true;

        }
        catch { }
        return false;

    }



    public async Task<bool> UploadUserProfileImageAsync(ClaimsPrincipal userClaims, IFormFile file)
    {
        try
        {
            if (userClaims != null && file != null && file.Length != 0)
            {
                var user = await _userManager.GetUserAsync(userClaims);
                if (user != null)
                {
                    var fileName = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FilePath:ProfileUploadPath"]!, fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    user.ProfileImage = fileName;
                    _dataContext.Update(user);
                    await _dataContext.SaveChangesAsync();

                    return true;
                }
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return false;
    }
}
