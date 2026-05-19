using Campanhas.Service.Microservice.Domain.Enums;

namespace Campanhas.Service.Microservice.Domain.Entities;

public class Campanha
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public decimal FinancialGoal { get; private set; }

    public decimal TotalRaised { get; private set; }

    public CampanhaStatus Status { get; private set; }

    protected Campanha()
    {
    }

    public Campanha(
        string title,
        string description,
        DateTime startDate,
        DateTime endDate,
        decimal financialGoal)
    {
        if (endDate < DateTime.UtcNow)
            throw new Exception("Invalid end date");

        if (financialGoal <= 0)
            throw new Exception("Financial goal invalid");

        Id = Guid.NewGuid();

        Title = title;

        Description = description;

        StartDate = startDate;

        EndDate = endDate;

        FinancialGoal = financialGoal;

        Status = CampanhaStatus.Active;
    }

    public void AddDonation(decimal amount)
    {
        if (Status != CampanhaStatus.Active)
            throw new Exception("Campaign inactive");

        TotalRaised += amount;
    }

    public void Finish()
    {
        Status = CampanhaStatus.Completed;
    }

    public void Cancel()
    {
        Status = CampanhaStatus.Cancelled;
    }

    public void Update(
    string title,
    string description,
    DateTime startDate,
    DateTime endDate,
    decimal financialGoal,
    CampanhaStatus status)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Title is required.");

        if (financialGoal <= 0)
            throw new Exception("Financial goal must be greater than zero.");

        if (endDate <= startDate)
            throw new Exception("End date must be greater than start date.");

        Title = title;

        Description = description;

        StartDate = startDate;

        EndDate = endDate;

        FinancialGoal = financialGoal;

        Status = status;
    }
}