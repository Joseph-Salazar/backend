using Application.Core;
using Application.Dto.Postulation;
using Application.Dto.SavedJobOffer;
using Application.MainModule.Interface;
using AutoMapper.QueryableExtensions;
using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using Domain.MainModule.Validations.PostulationValidations;
using Domain.MainModule.Validations.SavedJobOffersValidations;
using Infrastructure.CrossCutting.Constants;
using Infrastructure.CrossCutting.CustomExections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.MainModule;

public class SavedJobOffersAppService : BaseAppService, ISavedJobOffersAppService
{
    private readonly ISavedJobOfferRepository _savedJobOfferRepository;

    public SavedJobOffersAppService(
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _savedJobOfferRepository = serviceProvider.GetService<ISavedJobOfferRepository>() ??
                                 throw new InvalidOperationException();
    }

    public async Task<List<SavedJobOffersDto>> GetByPostulant(int postulantId)
    {
        if (postulantId == 0)
            throw new WarningException(MessageConst.InvalidSelection);

        var savedDto = await _savedJobOfferRepository
            .Find(p => p.PostulantId == postulantId)
            .ProjectTo<SavedJobOffersDto>(Mapper.ConfigurationProvider)
            .ToListAsync();

        if (savedDto is null)
            throw new WarningException(MessageConst.InvalidSelection);

        return savedDto;
    }

    public async Task<string> Add(AddSavedJobOffersDto savedDto)
    {
        var newPostulation = Mapper.Map<SavedJobOffers>(savedDto);
        await _savedJobOfferRepository.AddAsync(newPostulation, new AddSavedJobOffersValidator(_savedJobOfferRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<string> Update(SavedJobOffersDto postulationDto)
    {
        
        var postulationDomain = await _savedJobOfferRepository.GetAsync(postulationDto.Id);

        if (postulationDomain is null)
            throw new WarningException(MessageConst.InvalidSelection);

        Mapper.Map(postulationDto, postulationDomain);

        await _savedJobOfferRepository.UpdateAsync(postulationDomain,
            new AddSavedJobOffersValidator(_savedJobOfferRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }
}