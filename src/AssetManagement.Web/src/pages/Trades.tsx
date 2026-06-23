import { useEffect, useState, type FC } from "react";
import TradeList from "../components/Trade/TradeList";
import type { ITradeItem } from "../components/Trade/TradeItem";
import { getTrades } from "../components/Trade/TradeApi";

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