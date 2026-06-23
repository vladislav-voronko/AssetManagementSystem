import { api } from "../../api/axios";
import type { PagedResponse } from "../../types/PagedResponse";
import type { ITradeItem } from "./TradeItem";

export const getTrades = async () => {
    const response = await api.get<PagedResponse<ITradeItem>>("/trades");
    return response.data;
};