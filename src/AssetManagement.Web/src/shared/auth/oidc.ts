import { UserManager, WebStorageStateStore } from "oidc-client-ts";

const authority = import.meta.env.VITE_OIDC_AUTHORITY ?? "https://localhost:7297";
const clientId = import.meta.env.VITE_OIDC_CLIENT_ID ?? "assetmanagement.web";
const scope = import.meta.env.VITE_OIDC_SCOPE ?? "openid profile email assetmanagement.api offline_access";

export const userManager = new UserManager({
  authority,
  client_id: clientId,
  redirect_uri: `${window.location.origin}/auth/callback`,
  post_logout_redirect_uri: `${window.location.origin}/`,
  response_type: "code",
  scope,
  automaticSilentRenew: true,
  userStore: new WebStorageStateStore({ store: window.sessionStorage }),
});