import { type FC } from "react"

export interface IReplenishmentItem{
    id: string;
    amount: number;
    currency: string;
    replenishedAt: string;
    note: string;

}

interface IReplenishmentItemProps{
    replenishment: IReplenishmentItem
}

const ReplenishmentItem: FC<IReplenishmentItemProps> = ({replenishment}) => {
    return (
        <div>
            <div>Currency: {replenishment.currency} Amount: {replenishment.amount}, Replenishment at:{replenishment.replenishedAt}, Note: {replenishment.note}</div>
        </div>
    )
}

export default ReplenishmentItem