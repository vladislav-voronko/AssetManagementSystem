import { api } from "../../../shared/api/api";
import type { PagedResponse } from "../../../types/PagedResponse";
import type { ITradeItem } from "../ui/TradeItem";

export const getTrades = async () => {
    const response = await api.get<PagedResponse<ITradeItem>>("/trades");
    return response.data;
};