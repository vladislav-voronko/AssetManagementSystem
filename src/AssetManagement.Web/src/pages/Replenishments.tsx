import { useEffect, useState, type FC } from "react";
import ReplenishmentList from "../components/Replenishments/ReplenishmentList";
import { getReplenishments } from "../components/Replenishments/ReplenishmentApi";
import type { IReplenishmentItem } from "../components/Replenishments/ReplenishmentItem";

const Replenishments = () => {
    
const [replenishments, setReplenishments] = useState<IReplenishmentItem[]>([]);

async function fetchReplenishments() {
    try {
        const response = await getReplenishments();
        setReplenishments(response.items)
    } catch (error) {

    }
}

useEffect(() => {
    fetchReplenishments();
}, [])

    return (
        <div>
            <h1>Replenishments</h1>
            <ReplenishmentList replenishments={replenishments} />
        </div>
    );
}

export default Replenishments;