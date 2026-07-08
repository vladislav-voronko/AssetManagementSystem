import type { Components, Theme } from "@mui/material/styles";
import type { } from "@mui/x-data-grid/themeAugmentation";

export const components: Components<Theme> = {
    MuiButton: {
        styleOverrides: {
            root: {
                borderRadius: 12,
            },
        },
    },

    MuiPaper: {
        styleOverrides: {
            root: {
                //borderRadius: 12,
            },
        },
    },

    MuiDialog: {
        styleOverrides: {
            paper: {
                borderRadius: 16,
            },
        },
    },

    MuiDataGrid: {
        styleOverrides: {
            root: ({ theme }) => ({
                borderRadius: 12,
                border: "none",

                "& .MuiDataGrid-columnHeaders, & .MuiDataGrid-container--top": {
                    backgroundColor: `${theme.palette.secondary.main}`,
                },

                "& .MuiDataGrid-columnHeader": {
                    backgroundColor: `${theme.palette.secondary.main}`,
                    minHeight: 56,
                },

                "& .MuiDataGrid-columnHeaderTitle": {
                    color: `${theme.palette.secondary.contrastText}`,
                    fontWeight: 600,
                },

                "& .MuiDataGrid-row": {
                    cursor: "pointer",

                    "&:hover": {
                        backgroundColor: theme.palette.action.hover,
                    },
                },

                "& .MuiDataGrid-cell": {
                    borderBottom:
                        "1px solid rgba(0,0,0,0.08)",
                },

                "& .MuiDataGrid-cell:focus": {
                    outline: "none",
                },

                "& .MuiDataGrid-columnHeader:focus": {
                    outline: "none",
                },

                "& .MuiDataGrid-cell:focus, & .MuiDataGrid-columnHeader:focus": {
                    outline: "none",
                },
            }),
        },
    },
};