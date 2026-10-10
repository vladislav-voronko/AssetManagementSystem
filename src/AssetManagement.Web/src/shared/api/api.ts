import axios from "axios";
import { userManager } from "../auth/oidc";

export const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL ?? "https://localhost:7001/api",
    headers: {
        "Content-Type": "application/json",
    },
});

api.interceptors.request.use(async (request) => {
    const user = await userManager.getUser();

    if (user && !user.expired) {
        request.headers.Authorization = `Bearer ${user.access_token}`;
    }

    return request;
});