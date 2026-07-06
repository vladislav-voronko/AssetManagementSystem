import { useEffect, useState } from "react";
import AssetList from "../../entities/Asset/ui/AssetList";
import { createAsset, editAsset, getAssets, switchAssetStatus } from "../../entities/Asset/model/assetApi";
import type { Asset, CreateAssetRequest, EditAssetRequest } from "../../entities/Asset/model/assetTypes";
import { Alert, Button, CircularProgress, Container, Typography } from "@mui/material";
import axios from "axios";
import AssetDialog from "../../entities/Asset/ui/AssetDialog";
import AssetDetailsDialog from "../../entities/Asset/ui/AssetDetailsDialog";
import AssetSwitchStatusDialog from "../../entities/Asset/ui/AssetSwitchStatusDialog";

const Assets = () => {

  const [assets, setAssets] = useState<Asset[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [creating, setCreating] = useState(false);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [detailsDialogOpen, setDetailsDialogOpen] = useState(false);
  const [selectedAsset, setSelectedAsset] = useState<Asset | null>(null);
  const [isEditMode, setIsEditMode] = useState(false);

  const [switchDialogOpen, setSwitchDialogOpen] = useState(false);

  const handleCreate = () => {
    setIsEditMode(false);
    setSelectedAsset(null);
    setDialogOpen(true);
  };

  const handleOpenDetails = (asset: Asset) => {
    setSelectedAsset(asset);
    setDetailsDialogOpen(true);
  };

  const handleOpenEdit = () => {
    setIsEditMode(true);
    setDetailsDialogOpen(false);
    setDialogOpen(true);
  };

  const handleDialogSubmit = async (data: CreateAssetRequest | EditAssetRequest) => {
    if (isEditMode) {
      await handleEditAsset(data as EditAssetRequest);
    } else {
      await handleCreateAsset(data as CreateAssetRequest);
    }
  };

  const handleClose = () => {
    setDialogOpen(false);
    setDetailsDialogOpen(false);
  };

  const handleSwitchDialogClose = () => {
    setSwitchDialogOpen(false);
  };

  const handleCreateAsset = async (data: CreateAssetRequest) => {
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
    } finally {
      setCreating(false);
    }
  };

  const handleEditAsset = async (data: EditAssetRequest) => {
    if (!selectedAsset) return;

    setCreating(true);
    try {
      const updated = await editAsset(selectedAsset.id, data);
      setAssets((prev) =>
        prev.map((asset) => (asset.id === selectedAsset.id ? updated : asset))
      );
      setDialogOpen(false);
    } catch (e: unknown) {
      if (axios.isAxiosError(e)) {
        setError(e.response?.data?.message ?? e.message);
      } else if (e instanceof Error) {
        setError(e.message);
      } else {
        setError("Unknown error");
      }
    } finally {
      setCreating(false);
    }
  };

  const handleAssetStatusSwitchDialogOpen = (asset: Asset) => {
    setSelectedAsset(asset);
    setSwitchDialogOpen(true);
  }

  const handleAssetStatusSwitch = async () => {
    if (!selectedAsset) return;
    try {
      const updated = await switchAssetStatus(selectedAsset.id, !selectedAsset.isActive);

      setSwitchDialogOpen(false);
      setSelectedAsset(null);

      setAssets((prev) =>
        prev.map((asset) => (asset.id === selectedAsset.id ? updated : asset))
      );

    } catch (e: unknown) {
      if (axios.isAxiosError(e)) {
        setError(e.response?.data?.message ?? e.message);
      } else if (e instanceof Error) {
        setError(e.message);
      } else {
        setError("Unknown error");
      }
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

  if (error) {
    return (
      <Alert severity="error">
        {error}
      </Alert>
    );
  }

  if (assets.length === 0) {
    return (
      <Container>
        <Typography>Assets</Typography>
        <Button onClick={handleCreate}>Create</Button>
        <Typography>No assets found</Typography>
      </Container>
    );
  }
  return (
    <Container>
      <Typography>Assets</Typography>
      <Button onClick={handleCreate}>Create</Button>
      <AssetList
        assets={assets}
        onSelectAsset={handleOpenDetails}
        onStatusAssetSwitch={handleAssetStatusSwitchDialogOpen} />
      <AssetDialog
        open={dialogOpen}
        onClose={handleClose}
        onSubmit={handleDialogSubmit}
        title={isEditMode ? "Edit Asset" : "Create Asset"}
        submitLabel={isEditMode ? "Save" : "Create"}
        submitting={creating}
        asset={selectedAsset} />
      <AssetDetailsDialog
        open={detailsDialogOpen}
        onClose={handleClose}
        asset={selectedAsset}
        onEdit={handleOpenEdit} />
      <AssetSwitchStatusDialog
        open={switchDialogOpen}
        onClose={handleSwitchDialogClose}
        onSwitch={handleAssetStatusSwitch}
        isActive={selectedAsset?.isActive ?? false}
      />
    </Container>
  );
}

export default Assets;