import MenuIcon from "@mui/icons-material/Menu";
import {
  AppBar,
  Box,
  Button,
  Drawer,
  IconButton,
  List,
  ListItemButton,
  ListItemText,
  Toolbar,
  Typography,
  useMediaQuery,
  useTheme,
} from "@mui/material";
import { NavLink, useLocation } from "react-router-dom";
import { useState } from "react";
import { useAuth } from "react-oidc-context";

const navItems = [
  { label: "Assets", path: "/assets" },
  { label: "Trades", path: "/trades" },
  { label: "Replenishments", path: "/replenishments" },
];

const Navbar = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));
  const [drawerOpen, setDrawerOpen] = useState(false);
  const auth = useAuth();
  const location = useLocation();

  const handleDrawerToggle = () => {
    setDrawerOpen((prev) => !prev);
  };

  return (
    <>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Asset Management
          </Typography>

          {!auth.isLoading && (
            <Button
              color="inherit"
              onClick={() =>
                void (auth.isAuthenticated
                  ? auth.signoutRedirect()
                  : auth.signinRedirect({
                      state: {
                        returnTo: `${location.pathname}${location.search}${location.hash}`,
                      },
                    }))
              }
            >
              {auth.isAuthenticated ? "Sign out" : "Sign in"}
            </Button>
          )}

          {isMobile ? (
            <IconButton color="inherit" edge="end" onClick={handleDrawerToggle}>
              <MenuIcon />
            </IconButton>
          ) : (
            <Box sx={{ display: "flex", gap: 1 }}>
              {navItems.map((item) => (
                <Button
                  key={item.path}
                  color="inherit"
                  component={NavLink}
                  to={item.path}
                  sx={{
                    textTransform: "none",
                    "&.active": {
                      color: theme.palette.warning.light,
                    },
                  }}
                >
                  {item.label}
                </Button>
              ))}
            </Box>
          )}
        </Toolbar>
      </AppBar>

      <Drawer anchor="left" open={drawerOpen} onClose={handleDrawerToggle}>
        <Box sx={{ width: 240 }} role="presentation" onClick={handleDrawerToggle}>
          <List>
            {navItems.map((item) => (
              <ListItemButton key={item.path} component={NavLink} to={item.path}>
                <ListItemText primary={item.label} />
              </ListItemButton>
            ))}
          </List>
        </Box>
      </Drawer>
    </>
  );
};

export default Navbar;