export interface Asset {
  id: string;
  symbol: string;
  name: string;
  type: AssetType;
  isActive: boolean;
}

export interface CreateAssetRequest {
  symbol: string;
  name: string;
  type: AssetType;
}

export interface EditAssetRequest {
  symbol: string;
  name: string;
  type: AssetType;
  isActive: boolean;
}

export const AssetType = {
    Stock: 1,
    Crypto: 2,
    ETF: 3,
    Bond: 4,
    Commodity: 5
} as const;

export type AssetType =
    typeof AssetType[keyof typeof AssetType];

export const AssetTypeName = {
    [AssetType.Stock]: "Stock",
    [AssetType.Crypto]: "Crypto",
    [AssetType.ETF]: "ETF",
    [AssetType.Bond]: "Bond",
    [AssetType.Commodity]: "Commodity",
} as const;