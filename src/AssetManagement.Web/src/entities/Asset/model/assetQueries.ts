import { useQuery } from "@tanstack/react-query";
import { getAssets } from "./assetApi";

export const assetKeys = {
    all: ["assets"] as const,
};

export function useAssetsQuery() {
    return useQuery({
        queryKey: assetKeys.all,
        queryFn: getAssets,
    });
}