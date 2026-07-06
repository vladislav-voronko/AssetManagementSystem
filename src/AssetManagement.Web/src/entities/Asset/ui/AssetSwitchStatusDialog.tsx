import { Button, Dialog, DialogContent, DialogTitle, Typography } from "@mui/material";
import type { FC } from "react";

interface AssetSwitchStatusDialogProps {
    open: boolean;
    onClose: () => void;
    onSwitch: () => void;
    isActive?: boolean;
}

const AssetSwitchStatusDialog: FC<AssetSwitchStatusDialogProps> = ({ open, onClose, onSwitch, isActive }) => {

    const title = isActive
        ? "Deactivate Asset"
        : "Activate Asset";

    const submitLabel = isActive
        ? "Deactivate"
        : "Activate";

    const dialogBody = isActive
        ? "This asset will become inactive. Existing trades will remain unchanged. You can activate it again later."
        : "This asset will become active and can be used to create new trades.";

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <Typography>{dialogBody}</Typography>
                <Button onClick={onClose}>Cancel</Button>
                <Button onClick={onSwitch}>{submitLabel}</Button>
            </DialogContent>
        </Dialog>);
}

export default AssetSwitchStatusDialog;