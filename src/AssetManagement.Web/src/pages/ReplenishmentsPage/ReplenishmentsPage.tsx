import { useEffect, useState, type FC } from "react";
import ReplenishmentList from "../../entities/Replenishment/ui/ReplenishmentList";
import { getReplenishments } from "../../entities/Replenishment/model/replenishmentApi";
import type { IReplenishmentItem } from "../../entities/Replenishment/ui/ReplenishmentItem";

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