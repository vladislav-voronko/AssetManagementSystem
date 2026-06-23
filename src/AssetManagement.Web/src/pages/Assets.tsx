import { useEffect, useState, type FC } from "react";
import AssetList from "../components/Asset/AssetList";
import { getAssets } from "../components/Asset/AssetApi";
import type { IAssetItem } from "../components/Asset/AssetItem";

const Assets = () => {
    
const [assets, setAssets] = useState<IAssetItem[]>([]);

async function fetchAssets() {
    try {
      const response = await getAssets();
      setAssets(response.items)
    } catch (error) {

    }
  }

  useEffect(() => {
      fetchAssets();
    }, [])

    return (
    <div>
        <h1>Assets</h1>
        <AssetList assets={assets} />
    </div>
    );
}

export default Assets;