namespace Application.Dto.JobOffer;

public class JobOfferWithCompany
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Wage { get; set; }
    public bool HasHired { get; set; }
    public string Snippet { get; set; }
    public string BannerPicture { get; set; }
    public int CompanyId { get; set; }
    public Domain.MainModule.Entity.Company Company { get; set; }
}