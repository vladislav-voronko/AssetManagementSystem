import type { FC } from "react";
import { AssetTypeName, type Asset } from "../model/assetTypes";
import { List, ListItemText } from "@mui/material";

interface AssetDetailsViewProps {
    asset: Asset | null;
}

const AssetDetailsView: FC<AssetDetailsViewProps> = ({ asset }) => {
    if (!asset) return null;
    return (
        <List >
            <ListItemText>
                {asset.symbol} - {asset.name}
            </ListItemText>
            <ListItemText>
                {AssetTypeName[asset.type]} - {asset.isActive ? "Active" : "Inactive"}
            </ListItemText>
        </List>
    );
}

export default AssetDetailsView;