import type { Components, Theme } from "@mui/material/styles";

export const components: Components<Omit<Theme, "components">> = {
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
                borderRadius: 12,
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
};