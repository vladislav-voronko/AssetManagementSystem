import { useQuery } from "@tanstack/react-query";
import { getAsset, getAssets } from "./assetApi";

export const assetKeys = {
    all: ["assets"] as const,
    lists: () => [...assetKeys.all, "list"] as const,
    details: () => [...assetKeys.all, "detail"] as const,
    detail: (id: string) => [...assetKeys.details(), id] as const,
};

export function useAssetsQuery() {
    return useQuery({
        queryKey: assetKeys.lists(),
        queryFn: getAssets,
    });
}

export function useAssetQuery(id: string) {
    return useQuery({
        queryKey: assetKeys.detail(id),
        queryFn: () => getAsset(id),
        enabled: !!id,
    });
}