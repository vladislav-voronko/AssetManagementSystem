import {
    DataGrid,
    type GridColDef,
} from "@mui/x-data-grid";
import {
    type FC,
    useMemo,
    useState,
} from "react";

import {
    AssetStatusConfig,
    AssetTypeConfig,
    type Asset,
} from "../model/assetTypes";

import { Chip } from "@mui/material";
import AssetActionsMenu from "./AssetActionsMenu";
import AssetGridToolbar, { type AssetFilters } from "./AssetGridToolbar";


interface AssetGridProps {
    assets: Asset[];
    onSelectAsset: (asset: Asset) => void;
    onEditAsset: (asset: Asset) => void;
    onStatusAssetSwitch: (asset: Asset) => void;
    onCreateAsset: () => void;
}


const AssetGrid: FC<AssetGridProps> = ({
    assets,
    onSelectAsset,
    onEditAsset,
    onStatusAssetSwitch,
    onCreateAsset,
}) => {

    const [filters, setFilters] = useState<AssetFilters>({
        search: "",
        type: "all",
        status: "all",
    });

    const Toolbar = () => (
        <AssetGridToolbar
            onCreate={onCreateAsset}
            filters={filters}
            setFilters={setFilters}
        />
    );


    const filteredAssets = useMemo(() => {
        return assets.filter((asset) => {

            const search =
                filters.search.toLowerCase();


            const searchMatch =
                asset.name
                    .toLowerCase()
                    .includes(search)
                ||
                asset.symbol
                    .toLowerCase()
                    .includes(search);


            const typeMatch =
                filters.type === "all"
                ||
                asset.type === filters.type;


            const statusMatch =
                filters.status === "all"
                ||
                asset.isActive ===
                (filters.status === "active");


            return (
                searchMatch &&
                typeMatch &&
                statusMatch
            );
        });

    }, [assets, filters]);


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
                    label={
                        AssetTypeConfig[
                            params.row.type
                        ].label
                    }
                    color={
                        AssetTypeConfig[
                            params.row.type
                        ].color
                    }
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
        <DataGrid
            rows={filteredAssets}
            columns={columns}
            disableColumnMenu
            disableColumnResize
            disableRowSelectionOnClick

            slots={{
                toolbar: Toolbar,
            }}

            showToolbar

            onRowClick={(params) =>
                onSelectAsset(params.row)
            }

            initialState={{
                sorting: {
                    sortModel: [
                        {
                            field: "symbol",
                            sort: "asc",
                        },
                    ],
                },

                pagination: {
                    paginationModel: {
                        pageSize: 10,
                    },
                },
            }}

            pageSizeOptions={[
                10,
                25,
                50,
            ]}
        />
    );
};


export default AssetGrid;