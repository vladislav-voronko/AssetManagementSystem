import { Alert, Box, Button, CircularProgress, Typography } from "@mui/material";
import { useAuth } from "react-oidc-context";
import { Outlet, useLocation } from "react-router-dom";

const RequireAuth = () => {
  const auth = useAuth();
  const location = useLocation();

  if (auth.isLoading) {
    return (
      <Box role="status" sx={{ display: "flex", alignItems: "center", gap: 2, p: 3 }}>
        <CircularProgress size={20} />
        <Typography>Checking sign-in status...</Typography>
      </Box>
    );
  }

  if (auth.error) {
    return <Alert severity="error">Authentication failed: {auth.error.message}</Alert>;
  }

  if (!auth.isAuthenticated) {
    const returnTo = `${location.pathname}${location.search}${location.hash}`;

    return (
      <Alert
        severity="info"
        action={
          <Button
            color="inherit"
            size="small"
            onClick={() => void auth.signinRedirect({ state: { returnTo } })}
          >
            Sign in
          </Button>
        }
      >
        Sign in to continue.
      </Alert>
    );
  }

  return <Outlet />;
};

export default RequireAuth;