namespace Shared.Application.Filters
{
    public interface IDateRangeFilter
    {
        DateTime? From { get; }
        DateTime? To { get; }
    }
}
