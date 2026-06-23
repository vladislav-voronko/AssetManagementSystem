import { api } from "../../api/axios";
import type { PagedResponse } from "../../types/PagedResponse";
import type { IReplenishmentItem } from "./ReplenishmentItem";

export const getReplenishments = async () => {
    const response = await api.get<PagedResponse<IReplenishmentItem>>("/replenishments");
    return response.data;
};