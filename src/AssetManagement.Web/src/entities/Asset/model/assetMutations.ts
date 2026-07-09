import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createAsset, editAsset, switchAssetStatus } from "./assetApi";
import type { CreateAssetRequest, EditAssetRequest } from "./assetTypes";
import { assetKeys } from "./assetQueries";

export const useCreateAssetMutation = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (data: CreateAssetRequest) =>
            createAsset(data),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: assetKeys.all,
            });
        },
    });
};

export const useEditAssetMutation = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({
            id,
            data,
        }: {
            id: string;
            data: EditAssetRequest;
        }) => editAsset(id, data),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: assetKeys.all,
            });
        },
    });
};

export const useSwitchAssetStatusMutation = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({
            id,
            isActive,
        }: {
            id: string;
            isActive: boolean;
        }) =>
            switchAssetStatus(
                id,
                isActive
            ),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: assetKeys.all,
            });
        },
    });
};