import type { FC } from "react";
import type { Asset } from "../model/assetTypes";
import { Button, Dialog, DialogContent, DialogTitle } from "@mui/material";
import AssetDetailsView from "./AssetDetailsView";

interface AssetDetailsDialogProps {
    open: boolean;
    asset: Asset | null;
    onClose: () => void;
    onEdit: () => void;
}


const AssetDetailsDialog: FC<AssetDetailsDialogProps> = ({ open, asset, onClose, onEdit }) => {
    
    const handleEdit = () => {
        onEdit();
    };

    if (!asset) return null;
    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>Asset Details</DialogTitle>
            <DialogContent>
                <AssetDetailsView asset={asset}></AssetDetailsView>
                <Button
                    variant="outlined"
                    onClick={handleEdit}>
                    Edit
                </Button>
            </DialogContent>
        </Dialog>
    );
}

export default AssetDetailsDialog;