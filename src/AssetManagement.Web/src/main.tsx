import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './app/App.tsx'
import { CssBaseline, ThemeProvider } from '@mui/material'
import { theme } from './theme/theme.ts'
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { AuthProvider } from "react-oidc-context";
import { userManager } from "./shared/auth/oidc.ts";

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AuthProvider
      userManager={userManager}
      onSigninCallback={() => {
        window.history.replaceState({}, document.title, window.location.pathname);
      }}
    >
      <QueryClientProvider client={queryClient}>
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <App />
        </ThemeProvider>
      </QueryClientProvider>
    </AuthProvider>
  </StrictMode>,
)
