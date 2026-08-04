using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.AgentDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class AgentManager : IAgentService
{
    private readonly IAgentDal _agentDal;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAgentDto> _createValidator;
    private readonly IValidator<UpdateAgentDto> _updateValidator;

    public AgentManager(IAgentDal agentDal, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, IValidator<CreateAgentDto> createValidator, IValidator<UpdateAgentDto> updateValidator)
    {
        _agentDal = agentDal;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IdentityResult> CreateAgentAsync(CreateAgentDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            throw new Exception("Bu e-posta adresiyle kayıtlı bir kullanıcı bulunuyor.");
        }
        
        var user = new AppUser
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email,
            UserName = dto.Email,
            EmailConfirmed = true
        };

        var userResult = await _userManager.CreateAsync(user, dto.Password);

        if (!userResult.Succeeded)
            return userResult;
        
        var roleResult = await _userManager.AddToRoleAsync(user, "Agent");

        if (!await _roleManager.RoleExistsAsync("Agent"))
        {
            throw new Exception("Agent rolü sistemde tanımlı değil");
        }
        
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return roleResult;
        }
        
        try
        {
            var agent = new Agent
            {
                AppUserId = user.Id,
                Title = dto.Title,
                About = dto.About,
                City = dto.City,
                District = dto.District,
                OfficeName = dto.OfficeName,
                LicenseNumber = dto.LicenseNumber,
                ImageUrl = dto.ImageUrl,
                ExperienceYear = dto.ExperienceYear,
                IsVerified = dto.IsVerified,
                IsActive = dto.IsActive,
                CreatedDate = DateTime.UtcNow
            };

            await _agentDal.InsertAsync(agent);
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            throw;
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAgentAsync(UpdateAgentDto dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var agent = await _agentDal.GetByIdAsync(dto.AgentId);
        
        if (agent == null)
        {
            throw new Exception("Danışman bulunamadı.");
        }

        var user = await _userManager.FindByIdAsync(agent.AppUserId);

        if (user == null)
        {
            throw new Exception("Danışmana bağlı kullanıcı hesabı bulunamadı.");
        }

        user.Name = dto.Name;
        user.Surname = dto.Surname;
        user.Email = dto.Email;
        user.UserName = dto.Email;

        var userResult = await _userManager.UpdateAsync(user);

        if (!userResult.Succeeded)
            return userResult;

        agent.Title = dto.Title;
        agent.About = dto.About;
        agent.City = dto.City;
        agent.District = dto.District;
        agent.OfficeName = dto.OfficeName;
        agent.LicenseNumber = dto.LicenseNumber;
        agent.ImageUrl = dto.ImageUrl;
        agent.ExperienceYear = dto.ExperienceYear;
        agent.IsVerified = dto.IsVerified;
        agent.IsActive = dto.IsActive;

        await _agentDal.UpdateAsync(agent);

        return IdentityResult.Success;

    }

    public async Task<List<ResultAgentDto>> GetAllAsync()
    {
        var agents = await _agentDal.GetAllWithUserAsync();
        return _mapper.Map<List<ResultAgentDto>>(agents);
    }

    public async Task<UpdateAgentDto> GetByIdAsync(int id)
    {
        var value = await _agentDal.GetWithUserByIdAsync(id);
        return _mapper.Map<UpdateAgentDto>(value);
    }

    public async Task<IdentityResult> DeleteAgentAsync(int id)
    {
        var agent = await _agentDal.GetByIdAsync(id);

        if (agent == null)
        {
            throw new Exception("Danışman bulunamadı.");
        }

        var user = await _userManager.FindByIdAsync(agent.AppUserId);

        if (user == null)
        {
            throw new Exception("Danışmana bağlı kullanıcı hesabı bulunamadı.");
        }

        return await _userManager.DeleteAsync(user); 
    }
}