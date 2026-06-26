import { useEffect, useState } from "react";
import AssetList from "../../entities/Asset/ui/AssetList";
import { createAsset, getAssets } from "../../entities/Asset/model/assetApi";
import type { Asset, CreateAssetRequest } from "../../entities/Asset/model/assetTypes";
import { Alert, Button, CircularProgress, Container, Typography } from "@mui/material";
import axios from "axios";
import AssetDialog from "../../entities/Asset/ui/AssetDialog";
import AssetDetailsDialog from "../../entities/Asset/ui/AssetDetailsDialog";

const Assets = () => {

  const [assets, setAssets] = useState<Asset[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [creating, setCreating] = useState(false);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [detailsDialogOpen, setDetailsDialogOpen] = useState(false);
  const [selectedAsset, setSelectedAsset] = useState<Asset | null>(null);

  const handleCreate = () => {
    setSelectedAsset(null);
    setDialogOpen(true);
  };

  const handleOpenDetails = (asset: Asset) => {
    setSelectedAsset(asset);
    setDetailsDialogOpen(true);
    //setDialogOpen(true);
  };

  const handleClose = () => {
    setDialogOpen(false);
    setDetailsDialogOpen(false);
    setSelectedAsset(null);
  };

  const handleSubmit = async (data: CreateAssetRequest) => {
    setCreating(true);
    try {
      const created = await createAsset(data);
      setAssets((prev) => [created, ...prev]);
      setDialogOpen(false);
    } catch (e: unknown) {
      if (axios.isAxiosError(e)) {
        setError(e.response?.data?.message ?? e.message);
      } else if (e instanceof Error) {
        setError(e.message);
      } else {
        setError("Unknown error");
      }
    }
    finally {
      setCreating(false);
    }
  };

  async function fetchAssets() {
    try {
      const response = await getAssets();
      setAssets(response.items);
    } catch (e: unknown) {
      if (axios.isAxiosError(e)) {
        setError(e.response?.data?.message ?? e.message);
      } else if (e instanceof Error) {
        setError(e.message);
      } else {
        setError("Unknown error");
      }
    }
    finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchAssets();
  }, [])


  if (loading) {
    return <CircularProgress />;
  }

  // if (error) {
  //   return (
  //     <Alert severity="error">
  //       {error}
  //     </Alert>
  //   );
  // }

  if (assets.length === 0) {
    return (
      <Container>
        <Typography>Assets</Typography>
        <Button onClick={handleCreate}>Create</Button>
        <Typography>No assets found</Typography>
        <AssetDialog
          open={dialogOpen}
          onClose={handleClose}
          onSubmit={handleSubmit}
          submitting={creating}
        />
      </Container>
    );
  }
  return (
    <Container>
      <Typography>Assets</Typography>
      <Button onClick={handleCreate}>Create</Button>
      <AssetList assets={assets} onSelectAsset={handleOpenDetails} />
      <AssetDialog
        open={dialogOpen}
        onClose={handleClose}
        onSubmit={handleSubmit}
        submitting={creating} />
      <AssetDetailsDialog
        open={detailsDialogOpen}
        onClose={handleClose}
        asset={selectedAsset} />
    </Container>
  );
}

export default Assets;