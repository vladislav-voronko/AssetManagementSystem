import { Alert, Box, CircularProgress } from "@mui/material";
import type { ReactNode } from "react";

interface PageContainerProps {
    loading?: boolean;
    error?: unknown;
    children: ReactNode;
}

const PageContainer = ({
    loading = false,
    error = null,
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
            <Box
                sx={{
                    p: 3,
                }}
            >
                <Alert severity="error">
                    {error instanceof Error
                        ? error.message
                        : "Unexpected error occurred"}
                </Alert>
            </Box>
        );
    }

    return (
        <Box
            sx={{
                p: 3,
            }}
        >
            {children}
        </Box>
    );
};

export default PageContainer;