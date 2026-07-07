import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useState, type FC } from "react";
import { AssetStatusConfig, AssetTypeConfig, type Asset } from "../model/assetTypes";
import { Chip, IconButton, Menu, MenuItem } from "@mui/material";
import { MoreVert } from "@mui/icons-material";


interface AssetGridProps {
    assets: Asset[];
    onSelectAsset: (asset: Asset) => void;
    onEditAsset: (asset: Asset) => void;
    onStatusAssetSwitch: (asset: Asset) => void;
}


const AssetGrid: FC<AssetGridProps> = ({ assets, onSelectAsset, onEditAsset, onStatusAssetSwitch, }) => {

    const columns: GridColDef[] = [
        {
            field: "symbol",
            headerName: "Symbol",
            width: 120,
        },
        {
            field: "name",
            headerName: "Name",
            flex: 1,
        },
        {
            field: "type",
            headerName: "Type",
            width: 150,
            renderCell: (params) => (
                <Chip
                    label={AssetTypeConfig[params.value].label}
                    color={AssetTypeConfig[params.value].color}
                    size="small"
                />
            ),
        },
        {
            field: "isActive",
            headerName: "Status",
            width: 130,
            renderCell: (params) => (
                <Chip
                    label={
                        params.value
                            ? AssetStatusConfig.active.label
                            : AssetStatusConfig.inactive.label
                    }
                    color={
                        params.value
                            ? AssetStatusConfig.active.color
                            : AssetStatusConfig.inactive.color
                    }
                    size="small"
                />
            ),
        },
        {
            field: "actions",
            headerName: "",
            width: 60,
            sortable: false,
            filterable: false,
            disableColumnMenu: true,

            renderCell: (params) => (
                <IconButton
                    onClick={(event) => {
                        event.stopPropagation();
                        handleMenuOpen(event, params.row);
                    }}
                >
                    <MoreVert />
                </IconButton>
            ),
        },
    ];

    const [menuAnchor, setMenuAnchor] = useState<null | HTMLElement>(null);
    const [selectedMenuAsset, setSelectedMenuAsset] = useState<Asset | null>(null);

    const handleMenuOpen = (
        event: React.MouseEvent<HTMLElement>,
        asset: Asset
    ) => {
        setMenuAnchor(event.currentTarget);
        setSelectedMenuAsset(asset);
    };

    const handleMenuClose = () => {
        setMenuAnchor(null);
        setSelectedMenuAsset(null);
    };


    return (
        <>
            <DataGrid
                rows={assets}
                columns={columns}
                disableRowSelectionOnClick
                onRowClick={(params) => onSelectAsset(params.row)} />

            <Menu
                anchorEl={menuAnchor}
                open={Boolean(menuAnchor)}
                onClose={handleMenuClose}>
                <MenuItem
                    onClick={() => {
                        if (selectedMenuAsset) {
                            onEditAsset(selectedMenuAsset);
                        }
                        handleMenuClose();
                    }}>
                    Edit
                </MenuItem>

                <MenuItem
                    onClick={() => {
                        if (selectedMenuAsset) {
                            onStatusAssetSwitch(selectedMenuAsset);
                        }
                        handleMenuClose();
                    }}>
                    {
                        selectedMenuAsset?.isActive
                            ? "Deactivate"
                            : "Activate"
                    }
                </MenuItem>
            </Menu>
        </>
    );
};


export default AssetGrid;