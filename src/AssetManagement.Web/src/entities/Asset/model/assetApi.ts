import { api } from "../../../shared/api/api";
import type { PagedResponse } from "../../../types/PagedResponse";
import type { Asset, CreateAssetRequest, EditAssetRequest } from "./assetTypes";

export const getAssets = async () => {
    const response = await api.get<PagedResponse<Asset>>(`/assets`);
    return response.data;
};

export const createAsset = async (data: CreateAssetRequest) => {
    const response = await api.post<Asset>(`/assets`, data);
    return response.data;
};

export const editAsset = async (id: string, data: EditAssetRequest) => {
    const response = await api.put<Asset>(`/assets/${id}`, data);
    return response.data;
};