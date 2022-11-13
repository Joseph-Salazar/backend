using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using FluentValidation;

namespace Domain.MainModule.Validations.SavedJobOffersValidations;

public class AddSavedJobOffersValidator : AbstractValidator<SavedJobOffers>
{
    private readonly ISavedJobOfferRepository _savedJobOfferRepository;

    public AddSavedJobOffersValidator(ISavedJobOfferRepository savedJobOfferRepository)
    {
        _savedJobOfferRepository = savedJobOfferRepository;
    }
}