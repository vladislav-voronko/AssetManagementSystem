import {  type FC } from "react";
import type { ITradeItem } from "./TradeItem";
import TradeItem from "./TradeItem";


interface TradeListProps{
    trades: ITradeItem[]
}

const TradeList: FC<TradeListProps> = ({trades}) => {
    return (
    <div>
        {trades.map(trade => 
            <TradeItem trade={trade} key={trade.id}/>
        )}
    </div>);
}
 
export default TradeList;