import { useEffect, useState, type FC } from "react";
import AssetItem, { type IAssetItem } from "./AssetItem";


interface AssetListProps{
    assets: IAssetItem[]
}

const AssetList: FC<AssetListProps> = ({assets}) => {
    return (
    <div>
        {assets.map(asset => 
            <AssetItem asset={asset} key={asset.id}/>
        )}
    </div>);
}
 
export default AssetList;