import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from "@mui/material";
import { type FC } from "react";
import type { ITradeItem } from "./TradeItem";
import TradeItem from "./TradeItem";

interface TradeListProps {
  trades: ITradeItem[];
}

const TradeList: FC<TradeListProps> = ({ trades }) => (
  <TableContainer component={Paper} elevation={2}>
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Asset</TableCell>
          <TableCell>Type</TableCell>
          <TableCell>Quantity</TableCell>
          <TableCell>Price</TableCell>
          <TableCell>Date</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {trades.map((trade) => (
          <TradeItem trade={trade} key={trade.id} />
        ))}
      </TableBody>
    </Table>
  </TableContainer>
);

export default TradeList;