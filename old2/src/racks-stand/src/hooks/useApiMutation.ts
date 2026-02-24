import {
  useMutation,
  useQueryClient,
  type UseMutationOptions,
  type MutationFunction,
  type MutationKey,
} from "@tanstack/react-query";
import { useApiService } from "./useApiService";

type HttpMethod = "post" | "put" | "patch" | "delete";

interface MutationArgs {
  method: HttpMethod;
  url: string;
  invalidateKeys?: string[];
}

/**
 * Generic API mutation hook — handles retry, cache invalidation,
 * and global API error handling via useApiService.
 */
export function useApiMutation<
  TData = unknown,
  TError = unknown,
  TVariables = unknown,
  TContext = unknown
>(
  { method, url, invalidateKeys }: MutationArgs,
  options?: UseMutationOptions<TData, TError, TVariables, TContext>
) {
  const queryClient = useQueryClient();
  const { request } = useApiService();

  // Destructure user-defined lifecycle callbacks
  const { onSuccess, onError, onSettled, ...restOptions } = options ?? {};

  const mutationFn: MutationFunction<TData, TVariables> = async (variables) => {
    return await request<TData>({
      url,
      method,
      data: variables,
    });
  };

  return useMutation<TData, TError, TVariables, TContext>({
    mutationFn,

    async onSuccess(data, variables, context, mutation) {
      // Invalidate any dependent queries first
      if (invalidateKeys?.length) {
        await Promise.all(
          invalidateKeys.map((key) =>
            queryClient.invalidateQueries({ queryKey: [key] })
          )
        );
      }

      // Safely call user-defined callback
      if (onSuccess) {
        await onSuccess(data, variables, context, mutation);
      }
    },

    async onError(error, variables, context) {
      if (onError) {
        await onError(error, variables, context);
      }
    },

    async onSettled(data, error, variables, context) {
      if (onSettled) {
        await onSettled(data, error, variables, context);
      }
    },

    ...restOptions,
  });
}
