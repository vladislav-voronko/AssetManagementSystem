import { type FC } from "react"
import type { TradeType } from "./TradeType";
import type { IAssetItem } from "../Asset/AssetItem";

export interface ITradeItem{
    id: string;
    assetId: string;
    type: TradeType;
    quantity: number;
    price: number;
    currency: string;
    executedAt: string;
    asset: IAssetItem;
}

interface ITradeItemProps{
    trade: ITradeItem
}

const TradeItem: FC<ITradeItemProps> = ({trade}) => {
    return (
        <div>
            <div>{trade.type} Symbol: {trade.asset.symbol} Executed At: {trade.executedAt} Price: {trade.price} Quantity: {trade.quantity}</div>
        </div>
    )
}

export default TradeItem