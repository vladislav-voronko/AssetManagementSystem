import { type FC } from "react"
import { AssetTypeName, type AssetType } from "./AssetType";

export interface IAssetItem{
    id: string;
    symbol: string;
    name: string;
    type: AssetType;
    isActive: boolean;
}

interface IAssetItemProps{
    asset: IAssetItem
}

const AssetItem: FC<IAssetItemProps> = ({asset}) => {
    return (
        <div>
            <div>{asset.symbol} AssetType: {AssetTypeName[asset.type]}, Name:{asset.name}, IsActive: {asset.isActive ? "Yes" : "No"}</div>
        </div>
    )
}

export default AssetItem