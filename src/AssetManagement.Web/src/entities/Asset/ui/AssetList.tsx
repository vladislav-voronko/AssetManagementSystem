import { List, Paper } from "@mui/material";
import AssetItem from "./AssetItem";
import type { FC } from "react";
import type { Asset } from "../model/assetTypes";

interface AssetListProps {
  assets: Asset[];
  onSelectAsset?: (asset: Asset) => void;
  onStatusAssetSwitch: (asset: Asset) => void;
}

const AssetList: FC<AssetListProps> = ({ assets, onSelectAsset, onStatusAssetSwitch }) => (
  <Paper elevation={2} sx={{ width: "100%", overflow: "hidden" }}>
    <List>
      {assets.map((asset) => (
        <AssetItem
          asset={asset}
          key={asset.id}
          onSelectAsset={onSelectAsset}
          onStatusSwitch={onStatusAssetSwitch} />
      ))}
    </List>
  </Paper>
);

export default AssetList;