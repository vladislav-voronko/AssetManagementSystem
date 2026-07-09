import { useState } from "react";
import type { Asset, CreateAssetRequest, EditAssetRequest } from "../../entities/Asset/model/assetTypes";
import { Typography } from "@mui/material";
import AssetDialog from "../../entities/Asset/ui/AssetDialog";
import AssetDetailsDialog from "../../entities/Asset/ui/AssetDetailsDialog";
import AssetSwitchStatusDialog from "../../entities/Asset/ui/AssetSwitchStatusDialog";
import AssetGrid from "../../entities/Asset/ui/AssetGrid";
import PageHeader from "../../shared/ui/PageHeader";
import PageContainer from "../../shared/ui/PageContainer";
import { useAssetsQuery } from "../../entities/Asset/model/assetQueries";
import { useCreateAssetMutation, useEditAssetMutation, useSwitchAssetStatusMutation } from "../../entities/Asset/model/assetMutations";

const Assets = () => {
  const [dialogOpen, setDialogOpen] = useState(false);
  const [detailsDialogOpen, setDetailsDialogOpen] = useState(false);
  const [selectedAsset, setSelectedAsset] = useState<Asset | null>(null);
  const [switchDialogOpen, setSwitchDialogOpen] = useState(false);

  const { data, isLoading, error } = useAssetsQuery();
  const createMutation = useCreateAssetMutation();
  const editMutation = useEditAssetMutation();
  const switchStatusMutation = useSwitchAssetStatusMutation();

  const assets = data?.items ?? [];

  const handleCreate = () => {
    setSelectedAsset(null);
    setDialogOpen(true);
  };

  const handleOpenDetails = (asset: Asset) => {
    setSelectedAsset(asset);
    setDetailsDialogOpen(true);
  };

  const handleOpenEdit = (asset: Asset) => {
    setSelectedAsset(asset);
    setDetailsDialogOpen(false);
    setDialogOpen(true);
  };

  const handleDialogSubmit = async (data: CreateAssetRequest | EditAssetRequest) => {
    if (selectedAsset) {
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
    await createMutation.mutateAsync(data);
    setDialogOpen(false);
    setSelectedAsset(null);
  };

  const handleEditAsset = async (data: EditAssetRequest) => {
    if (!selectedAsset) {
      return;
    }
    await editMutation.mutateAsync({ id: selectedAsset.id, data });
    setDialogOpen(false);
    setSelectedAsset(null);
  };

  const handleAssetStatusSwitchDialogOpen = (asset: Asset) => {
    setSelectedAsset(asset);
    setSwitchDialogOpen(true);
  }

  const handleAssetStatusSwitch = async () => {
    if (!selectedAsset) {
      return;
    }

    await switchStatusMutation.mutateAsync({ id: selectedAsset.id, isActive: !selectedAsset.isActive });

    setSwitchDialogOpen(false);
    setSelectedAsset(null);
  };

  return (
    <PageContainer
      loading={isLoading}
      error={error}
    >
      <PageHeader
        title="Assets"
        subtitle="Manage your investment assets" />

      {assets.length === 0 ? (
        <Typography
          variant="body1"
          color="text.secondary"
          sx={{
            textAlign: "center",
            mt: 6,
          }}
        >
          No assets found.
        </Typography>
      ) : (
        <AssetGrid
          assets={assets}
          onSelectAsset={handleOpenDetails}
          onEditAsset={handleOpenEdit}
          onStatusAssetSwitch={handleAssetStatusSwitchDialogOpen}
          onCreateAsset={handleCreate}
        />
      )}

      <AssetDialog
        open={dialogOpen}
        onClose={handleClose}
        onSubmit={handleDialogSubmit}
        title={selectedAsset ? "Edit Asset" : "Create Asset"}
        submitLabel={selectedAsset ? "Save" : "Create"}
        submitting={createMutation.isPending || editMutation.isPending}
        asset={selectedAsset}
      />

      <AssetDetailsDialog
        open={detailsDialogOpen}
        onClose={handleClose}
        asset={selectedAsset}
        onEdit={handleOpenEdit}
      />

      <AssetSwitchStatusDialog
        open={switchDialogOpen}
        onClose={handleSwitchDialogClose}
        onSwitch={handleAssetStatusSwitch}
        isActive={selectedAsset?.isActive ?? false}
      />
    </PageContainer>
  );
}

export default Assets;