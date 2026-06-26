import type { FC } from "react";
import type { Asset } from "../model/assetTypes";
import { Dialog, DialogContent, DialogTitle } from "@mui/material";
import AssetDetailsView from "./AssetDetailsView";

interface AssetDetailsDialogProps {
    open: boolean;
    asset: Asset | null;
    onClose: () => void;
}

const AssetDetailsDialog: FC<AssetDetailsDialogProps> = ({ open, asset, onClose }) => {
    if (!asset) return null;
    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>Asset Details</DialogTitle>
            <DialogContent>
                <AssetDetailsView asset={asset}></AssetDetailsView>
            </DialogContent>
        </Dialog>
    );
}

export default AssetDetailsDialog;