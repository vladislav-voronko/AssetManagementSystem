import { Alert, Box, CircularProgress, Typography } from "@mui/material";
import { useAuth } from "react-oidc-context";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const AuthCallback = () => {
  const auth = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (auth.isLoading || !auth.isAuthenticated) {
      return;
    }

    const state = auth.user?.state;
    const returnTo =
      typeof state === "object" &&
      state !== null &&
      "returnTo" in state &&
      typeof state.returnTo === "string" &&
      state.returnTo.startsWith("/") &&
      !state.returnTo.startsWith("//")
        ? state.returnTo
        : "/assets";

    navigate(returnTo, { replace: true });
  }, [auth.isAuthenticated, auth.isLoading, auth.user?.state, navigate]);

  if (auth.error) {
    return <Alert severity="error">Sign-in failed: {auth.error.message}</Alert>;
  }

  if (!auth.isLoading && !auth.isAuthenticated) {
    return <Alert severity="error">Unable to complete sign-in. Please try again.</Alert>;
  }

  return (
    <Box role="status" sx={{ display: "flex", alignItems: "center", gap: 2, p: 3 }}>
      <CircularProgress size={20} />
      <Typography>Completing sign-in...</Typography>
    </Box>
  );
};

export default AuthCallback;