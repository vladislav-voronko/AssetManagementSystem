import type { ChipProps } from "@mui/material";

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

type ChipConfig = {
    label: string;
    color: ChipProps["color"];
};

export const AssetTypeConfig: Record<AssetType, ChipConfig> = {
    [AssetType.Stock]: {
        label: "Stock",
        color: "primary",
    },
    [AssetType.Crypto]: {
        label: "Crypto",
        color: "warning",
    },
    [AssetType.ETF]: {
        label: "ETF",
        color: "success",
    },
    [AssetType.Bond]: {
        label: "Bond",
        color: "info",
    },
    [AssetType.Commodity]: {
        label: "Commodity",
        color: "secondary",
    },
};

type AssetStatus = "active" | "inactive";

export const AssetStatusConfig: Record<AssetStatus, ChipConfig> = {
    active: {
        label: "Active",
        color: "success",
    },
    inactive: {
        label: "Inactive",
        color: "default",
    },
};