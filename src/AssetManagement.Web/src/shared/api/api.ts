import axios from "axios";

export const api = axios.create({
    baseURL: "https://localhost:7001/api",
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwMTllNmYxOC0yNDQ2LTdjZjgtYmRkMC1lZGU1OTM3MGU4M2EiLCJlbWFpbCI6ImFAZW1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTc4MjQ5ODI2MywiaXNzIjoiQXNzZXRNYW5hZ2VtZW50IiwiYXVkIjoiQXNzZXRNYW5hZ2VtZW50Q2xpZW50In0.5gcQ6jO9XahseeXVqjhjAqh57HTGNJ4_pgZG3nY-GK8"
    }
});