import { type FC } from "react";
import ReplenishmentItem, { type IReplenishmentItem } from "./ReplenishmentItem";


interface ReplenishmentListProps{
    replenishments: IReplenishmentItem[]
}

const ReplenishmentList: FC<ReplenishmentListProps> = ({replenishments}) => {
    return (
    <div>
        {replenishments.map(replenishment => 
            <ReplenishmentItem replenishment={replenishment} key={replenishment.id}/>
        )}
    </div>);
}
 
export default ReplenishmentList;