import { api } from "../../../shared/api/api";
import type { PagedResponse } from "../../../types/PagedResponse";
import type { IReplenishmentItem } from "../ui/ReplenishmentItem";

export const getReplenishments = async () => {
    const response = await api.get<PagedResponse<IReplenishmentItem>>("/replenishments");
    return response.data;
};