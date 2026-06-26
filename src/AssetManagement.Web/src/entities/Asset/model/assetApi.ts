import { api } from "../../../shared/api/api";
import type { PagedResponse } from "../../../types/PagedResponse";
import type { Asset, CreateAssetRequest } from "./assetTypes";

export const getAssets = async () => {
    const response = await api.get<PagedResponse<Asset>>("/assets");
    return response.data;
};

export const createAsset = async (data: CreateAssetRequest) => {
    const response = await api.post<Asset>("/assets", data);
    return response.data;
};