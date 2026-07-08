import { MoreVert } from "@mui/icons-material";
import { IconButton, Menu, MenuItem } from "@mui/material";
import { useState, type FC, type MouseEvent } from "react";
import type { Asset } from "../model/assetTypes";

interface AssetActionsMenuProps {
    asset: Asset;
    onEdit: (asset: Asset) => void;
    onStatusSwitch: (asset: Asset) => void;
}

const AssetActionsMenu: FC<AssetActionsMenuProps> = ({ asset, onEdit, onStatusSwitch, }) => {
    const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);

    const open = Boolean(anchorEl);

    const handleOpen = (event: MouseEvent<HTMLElement>) => {
        event.stopPropagation();
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleEdit = () => {
        handleClose();
        onEdit(asset);
    };

    const handleStatusSwitch = () => {
        handleClose();
        onStatusSwitch(asset);
    };

    return (
        <>
            <IconButton
                size="small"
                aria-label="asset actions"
                onClick={handleOpen}
            >
                <MoreVert />
            </IconButton>

            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                onClick={(event) => event.stopPropagation()}
            >
                <MenuItem onClick={handleEdit}>
                    Edit
                </MenuItem>

                <MenuItem onClick={handleStatusSwitch}>
                    {asset.isActive ? "Deactivate" : "Activate"}
                </MenuItem>
            </Menu>
        </>
    );
};

export default AssetActionsMenu;