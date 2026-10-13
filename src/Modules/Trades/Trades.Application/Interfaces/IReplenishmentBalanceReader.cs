namespace Trades.Application.Interfaces
{
    public interface IReplenishmentBalanceReader
    {
        Task<decimal> GetAmountByCurrencyAsync(string currency, CancellationToken cancellationToken);
        Task<IReadOnlyDictionary<string, decimal>> GetAmountsByCurrencyAsync(CancellationToken cancellationToken);
    }
}
