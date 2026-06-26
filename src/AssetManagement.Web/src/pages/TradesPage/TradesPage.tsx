import { useEffect, useState, type FC } from "react";
import TradeList from "../../entities/Trade/ui/TradeList";
import type { ITradeItem } from "../../entities/Trade/ui/TradeItem";
import { getTrades } from "../../entities/Trade/model/tradeApi";

const Trades = () => {
    
const [trades, setTrades] = useState<ITradeItem[]>([]);

async function fetchTrades() {
    try {
        const response = await getTrades();
        setTrades(response.items)
    } catch (error) {

    }
}

useEffect(() => {
    fetchTrades();
}, [])

    return (
        <div>
            <h1>Trades</h1>
            <TradeList trades={trades} />
        </div>
    );
}

export default Trades;