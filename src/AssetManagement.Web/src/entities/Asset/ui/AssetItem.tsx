import { ListItem, ListItemText, Chip, Stack, ListItemButton, IconButton, Tooltip } from "@mui/material";
import type { FC } from "react";
import { AssetTypeName, type Asset } from "../model/assetTypes";
import { AddBox, DisabledByDefault } from "@mui/icons-material";

interface IAssetItemProps {
  asset: Asset;
  onSelectAsset?: (asset: Asset) => void;
  onStatusSwitch: (asset: Asset) => void;
}

const AssetItem: FC<IAssetItemProps> = ({ asset, onSelectAsset, onStatusSwitch }) => (
  <ListItem divider >
    <Tooltip arrow title={
      asset.isActive ?
        "Deactivate asset" :
        "Activate asset"}>
      <IconButton
        size="large"
        color="primary"
        onClick={() => onStatusSwitch(asset)}>
        {
          asset.isActive ?
            <DisabledByDefault fontSize="inherit" /> :
            <AddBox fontSize="inherit" />
        }
      </IconButton>
    </Tooltip>
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