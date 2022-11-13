using Application.Dto.SavedJobOffer;

namespace Application.MainModule.Interface;

public interface ISavedJobOffersAppService
{
    Task<string> Add(AddSavedJobOffersDto savedJobOfferDto);
    Task<string> Update(SavedJobOffersDto updateSavedJobOfferDto);
    Task<List<SavedJobOffersDto>> GetByPostulant(int postulantId);

}