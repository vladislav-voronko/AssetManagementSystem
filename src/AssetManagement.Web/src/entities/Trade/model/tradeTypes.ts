export const TradeType = {
    Buy: 0,
    Sell: 1
} as const;

export type TradeType =
    typeof TradeType[keyof typeof TradeType];

export const TradeTypeName = {
    [TradeType.Buy]: "Buy",
    [TradeType.Sell]: "Sell",
} as const;