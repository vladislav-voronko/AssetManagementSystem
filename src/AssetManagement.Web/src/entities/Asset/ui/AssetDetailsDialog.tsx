import type { FC } from "react";
import type { Asset } from "../model/assetTypes";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";
import AssetDetailsView from "./AssetDetailsView";

interface AssetDetailsDialogProps {
    open: boolean;
    asset: Asset | null;
    onClose: () => void;
    onEdit: (asset: Asset) => void;
}


const AssetDetailsDialog: FC<AssetDetailsDialogProps> = ({ open, asset, onClose, onEdit }) => {

    const handleEdit = () => {
        if (asset) {
            onEdit(asset);
        }
    };

    if (!asset) return null;
    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>Asset Details</DialogTitle>
            <DialogContent>
                <AssetDetailsView asset={asset}></AssetDetailsView>
            </DialogContent>
            <DialogActions>
                <Button
                    color="secondary"
                    variant="contained"
                    onClick={onClose}>
                    Close
                </Button>
                <Button
                    color="primary"
                    variant="contained"
                    onClick={handleEdit}>
                    Edit
                </Button>
            </DialogActions>
        </Dialog>
    );
}

export default AssetDetailsDialog;