import { List, Paper } from "@mui/material";
import { type FC } from "react";
import ReplenishmentItem, { type IReplenishmentItem } from "./ReplenishmentItem";

interface ReplenishmentListProps {
  replenishments: IReplenishmentItem[];
}

const ReplenishmentList: FC<ReplenishmentListProps> = ({ replenishments }) => (
  <Paper elevation={2} sx={{ width: "100%", overflow: "hidden" }}>
    <List>
      {replenishments.map((replenishment) => (
        <ReplenishmentItem replenishment={replenishment} key={replenishment.id} />
      ))}
    </List>
  </Paper>
);

export default ReplenishmentList;