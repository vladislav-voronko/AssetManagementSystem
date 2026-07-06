import { Button, Dialog, DialogContent, DialogTitle, MenuItem, TextField } from "@mui/material";
import { useEffect, useState, type FC } from "react";
import { AssetType, AssetTypeName, type Asset, type CreateAssetRequest, type EditAssetRequest } from "../model/assetTypes";

interface AssetDialogProps {
    open: boolean;
    onClose: () => void;
    onSubmit: (data: CreateAssetRequest | EditAssetRequest) => void | Promise<void>;
    submitting?: boolean;
    title?: string;
    submitLabel?: string;
    asset?: Asset | null;
}

const AssetDialog: FC<AssetDialogProps> = ({ open, onClose, onSubmit, submitting = false, asset = null, title, submitLabel }) => {
    const isEditMode = Boolean(asset);

    const [symbol, setSymbol] = useState("");
    const [name, setName] = useState("");
    const [type, setType] = useState<AssetType | "">("");

    const assetTypeValues = Object.values(AssetType) as AssetType[];

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (type === "") {
            return;
        }
        if (isEditMode) {
            await onSubmit({ symbol, name, type: type as AssetType } as EditAssetRequest);
        } else {
            await onSubmit({ symbol, name, type: type as AssetType } as CreateAssetRequest);
        }
        onClose();
    };

    useEffect(() => {
        if (asset) {
            setSymbol(asset.symbol);
            setName(asset.name);
            setType(asset.type);
        } else {
            setSymbol("");
            setName("");
            setType("");
        }
    }, [asset, open]);

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <form onSubmit={handleSubmit}>
                    <TextField
                        autoFocus
                        required
                        value={symbol}
                        onChange={(e) => setSymbol(e.target.value)}
                        margin="dense"
                        id="symbol"
                        name="symbol"
                        label="Symbol"
                        type="text"
                        fullWidth
                        variant="standard"
                    />
                    <TextField
                        required
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        margin="dense"
                        id="name"
                        name="name"
                        label="Name"
                        type="text"
                        fullWidth
                        variant="standard"
                    />
                    <TextField
                        select
                        required
                        onChange={(e) => setType(Number(e.target.value) as AssetType)}
                        margin="dense"
                        id="type"
                        name="type"
                        value={type}
                        fullWidth
                        variant="standard"
                        label="Type"
                    >
                        <MenuItem value="">
                            <em>Select type</em>
                        </MenuItem>
                        {assetTypeValues.map((v) => (
                            <MenuItem key={v} value={v}>
                                {AssetTypeName[v]}
                            </MenuItem>
                        ))}
                    </TextField>
                    <Button
                        type="submit"
                        variant="contained"
                        disabled={submitting}>
                        {submitLabel}
                    </Button>
                </form>
            </DialogContent>
        </Dialog>
    );
}

export default AssetDialog;