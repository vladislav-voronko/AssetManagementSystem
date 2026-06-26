import Assets from "../../pages/AssetsPage/AssetsPage";
import Trades from "../../pages/TradesPage/TradesPage";
import Replenishments from "../../pages/ReplenishmentsPage/ReplenishmentsPage";
import { Routes, Route, Navigate } from 'react-router-dom';
import React from 'react';

type RouteDef = {
  path: string;
  component: React.ComponentType<any>;
  exact?: boolean;
}

export const routes: RouteDef[] = [
  { path: '/assets', component: Assets, exact: true },
  { path: '/trades', component: Trades, exact: true },
  { path: '/replenishments', component: Replenishments, exact: true },
]

const AppRouter: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/assets" replace />} />
      {routes.map((route, idx) => {
        const Component = route.component;
        return <Route key={idx} path={route.path} element={<Component />} />
      })}
    </Routes>
  )
}

export default AppRouter;
