namespace Trades.Application.Queries.Portfolio.GetCashBalance
{
    public sealed class GetCashBalanceResponse
    {
        public string Currency { get; init; } = null!;
        public decimal Balance { get; init; }
    }
}
