import { ListItem, ListItemText, Chip, Stack, ListItemButton } from "@mui/material";
import type { FC } from "react";
import { AssetTypeName, type Asset } from "../model/assetTypes";

interface IAssetItemProps {
  asset: Asset;
  onSelectAsset?: (asset: Asset) => void 
}

const AssetItem: FC<IAssetItemProps> = ({ asset, onSelectAsset }) => (
  <ListItem divider >
    <ListItemButton onClick={() => onSelectAsset?.(asset)}>
      <ListItemText
        primary={`${asset.symbol} — ${asset.name}`}
        secondary={`Type: ${AssetTypeName[asset.type]}`}
      />
      <Stack direction="row" spacing={1}>
        <Chip label={AssetTypeName[asset.type]} color="primary" size="small" />
        <Chip label={asset.isActive ? "Active" : "Inactive"} size="small" />
      </Stack>
    </ListItemButton>
  </ListItem>
);

export default AssetItem;