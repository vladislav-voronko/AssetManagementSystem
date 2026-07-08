import {
    Box,
    Button,
    TextField,
    InputAdornment,
    IconButton,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
} from "@mui/material";

import {
    Add,
    Search,
    Clear,
} from "@mui/icons-material";

import {
    AssetType,
    AssetTypeConfig,
} from "../model/assetTypes";


export interface AssetFilters {
    search: string;
    type: AssetType | "all";
    status: "all" | "active" | "inactive";
}


interface AssetGridToolbarProps {
    onCreate: () => void;

    filters: AssetFilters;

    setFilters: React.Dispatch<
        React.SetStateAction<AssetFilters>
    >;
}


const AssetGridToolbar = ({
    onCreate,
    filters,
    setFilters,
}: AssetGridToolbarProps) => {


    return (
        <Box
            sx={{
                width: "100%",
                display: "flex",
                alignItems: "center",
                gap: 2,
                p: 1,
            }}
        >

            <TextField
                size="small"
                placeholder="Search assets..."

                value={filters.search}

                onChange={(e) =>
                    setFilters((prev) => ({
                        ...prev,
                        search: e.target.value,
                    }))
                }

                sx={{
                    minWidth: 280,
                }}

                slotProps={{
                    input: {

                        startAdornment: (
                            <InputAdornment position="start">
                                <Search fontSize="small" />
                            </InputAdornment>
                        ),


                        endAdornment:
                            filters.search ? (
                                <InputAdornment position="end">

                                    <IconButton
                                        size="small"
                                        onClick={() =>
                                            setFilters(
                                                (prev) => ({
                                                    ...prev,
                                                    search: "",
                                                })
                                            )
                                        }
                                    >
                                        <Clear fontSize="small" />
                                    </IconButton>

                                </InputAdornment>
                            )
                                : undefined,
                    },
                }}
            />


            <FormControl
                size="small"
                sx={{
                    minWidth: 140,
                }}
            >

                <InputLabel>
                    Type
                </InputLabel>


                <Select
                    value={filters.type}
                    label="Type"

                    onChange={(e) =>
                        setFilters((prev) => ({
                            ...prev,

                            type:
                                e.target.value === "all"
                                    ? "all"
                                    : Number(
                                        e.target.value
                                    ) as AssetType,
                        }))
                    }
                >

                    <MenuItem value="all">
                        All
                    </MenuItem>


                    {Object.entries(
                        AssetTypeConfig
                    ).map(([key, config]) => (

                        <MenuItem
                            key={key}
                            value={Number(key)}
                        >
                            {config.label}
                        </MenuItem>

                    ))}

                </Select>

            </FormControl>


            <FormControl
                size="small"
                sx={{
                    minWidth: 140,
                }}
            >

                <InputLabel>
                    Status
                </InputLabel>


                <Select
                    value={filters.status}
                    label="Status"

                    onChange={(e) =>
                        setFilters((prev) => ({
                            ...prev,

                            status:
                                e.target.value as
                                AssetFilters["status"],
                        }))
                    }
                >

                    <MenuItem value="all">
                        All
                    </MenuItem>

                    <MenuItem value="active">
                        Active
                    </MenuItem>

                    <MenuItem value="inactive">
                        Inactive
                    </MenuItem>

                </Select>

            </FormControl>


            <Box
                sx={{
                    ml: "auto",
                }}
            >

                <Button
                    variant="contained"
                    startIcon={<Add />}
                    onClick={onCreate}
                >
                    Add Asset
                </Button>

            </Box>

        </Box>
    );
};


export default AssetGridToolbar;