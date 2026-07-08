import { Alert, Box, CircularProgress, Typography, } from "@mui/material";
import type { ReactNode } from "react";

interface PageContainerProps {
    loading?: boolean;
    error?: string | null;
    empty?: boolean;
    emptyText?: string;
    children: ReactNode;
}

const PageContainer = ({
    loading = false,
    error = null,
    empty = false,
    emptyText = "No data found",
    children,
}: PageContainerProps) => {
    if (loading) {
        return (
            <Box
                sx={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    height: "60vh",
                }}
            >
                <CircularProgress />
            </Box>
        );
    }

    if (error) {
        return (
            <Alert severity="error">
                {error}
            </Alert>
        );
    }

    return (
        <Box
            sx={{
                p: 3,
            }}
        >
            {children}

            {empty && (
                <Typography
                    variant="body1"
                    color="text.secondary"
                    sx={{
                        mt: 3,
                        textAlign: "center",
                    }}
                >
                    {emptyText}
                </Typography>
            )}
        </Box>
    );
};

export default PageContainer;