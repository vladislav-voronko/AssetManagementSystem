import { TableRow, TableCell } from "@mui/material";
import { type FC } from "react";
import { TradeTypeName, type TradeType } from "../model/tradeTypes";
import type { IAssetItem } from "../../Asset/ui/AssetItem";

export interface ITradeItem {
  id: string;
  assetId: string;
  type: TradeType;
  quantity: number;
  price: number;
  currency: string;
  executedAt: string;
  asset: IAssetItem;
}

interface ITradeItemProps {
  trade: ITradeItem;
}

const TradeItem: FC<ITradeItemProps> = ({ trade }) => (
  <TableRow>
    <TableCell>{trade.asset.symbol}</TableCell>
    <TableCell>{TradeTypeName[trade.type]}</TableCell>
    <TableCell>{trade.quantity}</TableCell>
    <TableCell>{`${trade.price} ${trade.currency}`}</TableCell>
    <TableCell>{new Date(trade.executedAt).toLocaleDateString()}</TableCell>
  </TableRow>
);

export default TradeItem;