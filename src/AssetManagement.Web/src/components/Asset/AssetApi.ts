
import { api } from "../../api/axios";
import type { PagedResponse } from "../../types/PagedResponse";
import type { IAssetItem } from "./AssetItem";

export const getAssets = async () => {
    const response = await api.get<PagedResponse<IAssetItem>>("/assets");
    return response.data;
};