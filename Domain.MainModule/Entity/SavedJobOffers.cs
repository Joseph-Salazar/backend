using Domain.Core.Entity;

namespace Domain.MainModule.Entity;

public class SavedJobOffers : Entity<int>
{
    public JobOffer JobOffer { get; set; }
    public Postulant Postulant { get; set; }
    public int JobOfferId { get; set; }
    public int PostulantId { get; set; }
}