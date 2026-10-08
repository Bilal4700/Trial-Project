using CalculatorApp.Data;
using CalculatorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApp.Services;

public class ReviewService
{
    private readonly AppDbContext _dbContext;

    public ReviewService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Review>> GetReviewsAsync()
    {
        return await _dbContext.Reviews
            .AsNoTracking()
            .OrderByDescending(review => review.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<(bool IsSuccess, string Message)> AddReviewAsync(string? name, string? comment)
    {
        var trimmedName = name?.Trim();
        var trimmedComment = comment?.Trim();

        if (string.IsNullOrWhiteSpace(trimmedName))
        {
            return (false, "Please enter your name.");
        }

        if (string.IsNullOrWhiteSpace(trimmedComment))
        {
            return (false, "Please enter a review.");
        }

        if (trimmedName.Length > 50)
        {
            return (false, "Your name must be 50 characters or fewer.");
        }

        if (trimmedComment.Length > 1000)
        {
            return (false, "Your review must be 1,000 characters or fewer.");
        }

        var review = new Review
        {
            Name = trimmedName,
            Comment = trimmedComment,
            CreatedAtUtc = DateTime.UtcNow
        };

        _dbContext.Reviews.Add(review);
        await _dbContext.SaveChangesAsync();

        return (true, "Your review was added successfully.");
    }
}
