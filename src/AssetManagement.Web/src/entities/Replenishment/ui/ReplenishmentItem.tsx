import { ListItem, ListItemText, Chip, Stack } from "@mui/material";
import { type FC } from "react";

export interface IReplenishmentItem {
  id: string;
  amount: number;
  currency: string;
  replenishedAt: string;
  note: string;
}

interface IReplenishmentItemProps {
  replenishment: IReplenishmentItem;
}

const ReplenishmentItem: FC<IReplenishmentItemProps> = ({ replenishment }) => (
  <ListItem divider>
    <ListItemText
      primary={`${replenishment.amount} ${replenishment.currency}`}
      secondary={`Replenished: ${new Date(replenishment.replenishedAt).toLocaleString()}${replenishment.note ? ` · ${replenishment.note}` : ""}`}
    />
    <Stack direction="row" spacing={1}>
      <Chip label="Replenishment" size="small" color="secondary" />
    </Stack>
  </ListItem>
);

export default ReplenishmentItem;