import Assets from "../../pages/AssetsPage/AssetsPage";
import Trades from "../../pages/TradesPage/TradesPage";
import Replenishments from "../../pages/ReplenishmentsPage/ReplenishmentsPage";
import { Navigate, Route, Routes } from "react-router-dom";
import AuthCallback from "../../shared/auth/AuthCallback";
import RequireAuth from "../../shared/auth/RequireAuth";

const AppRouter = () => {
  return (
    <Routes>
      <Route path="/auth/callback" element={<AuthCallback />} />
      <Route element={<RequireAuth />}>
        <Route path="/" element={<Navigate to="/assets" replace />} />
        <Route path="/assets" element={<Assets />} />
        <Route path="/trades" element={<Trades />} />
        <Route path="/replenishments" element={<Replenishments />} />
      </Route>
      <Route path="*" element={<Navigate to="/assets" replace />} />
    </Routes>
  )
}

export default AppRouter;
