import { Box, Typography } from "@mui/material";

interface PageHeaderProps {
    title: string;
    subtitle?: string;
    children?: React.ReactNode;
}

const PageHeader = ({
    title,
    subtitle,
}: PageHeaderProps) => {
    return (
        <Box
            sx={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "flex-start",
                mb: 3,
            }}
        >
            <Box>
                <Typography
                    variant="h4">
                    {title}
                </Typography>

                {subtitle && (
                    <Typography
                        variant="body2"
                        color="text.secondary"
                        sx={{ mt: 0.5 }}
                    >
                        {subtitle}
                    </Typography>
                )}
            </Box>

        </Box>
    );
};

export default PageHeader;