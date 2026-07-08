import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { type FC } from "react";
import { AssetStatusConfig, AssetTypeConfig, type Asset } from "../model/assetTypes";
import { Chip } from "@mui/material";
import AssetActionsMenu from "./AssetActionsMenu";


interface AssetGridProps {
    assets: Asset[];
    onSelectAsset: (asset: Asset) => void;
    onEditAsset: (asset: Asset) => void;
    onStatusAssetSwitch: (asset: Asset) => void;
}


const AssetGrid: FC<AssetGridProps> = ({ assets, onSelectAsset, onEditAsset, onStatusAssetSwitch, }) => {

    const columns: GridColDef<Asset>[] = [
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
                    label={AssetTypeConfig[params.row.type].label}
                    color={AssetTypeConfig[params.row.type].color}
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
                        params.row.isActive
                            ? AssetStatusConfig.active.label
                            : AssetStatusConfig.inactive.label
                    }
                    color={
                        params.row.isActive
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
            align: "center",
            renderCell: (params) => (
                <AssetActionsMenu
                    asset={params.row}
                    onEdit={onEditAsset}
                    onStatusSwitch={onStatusAssetSwitch}
                />
            ),
        },
    ];

    return (
        <>
            <DataGrid
                rows={assets}
                columns={columns}
                disableRowSelectionOnClick
                onRowClick={(params) => onSelectAsset(params.row)} />
        </>
    );
};


export default AssetGrid;