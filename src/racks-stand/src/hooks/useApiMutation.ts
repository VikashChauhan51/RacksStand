import {
  useMutation,
  useQueryClient,
  type UseMutationOptions,
} from "@tanstack/react-query";
import axiosClient from "../api/axiosClient";

type HttpMethod = "post" | "put" | "patch" | "delete";

interface MutationArgs {
  method: HttpMethod;
  url: string;
  invalidateKeys?: string[];
}

/**
 * Generic mutation hook supporting POST, PUT, PATCH, DELETE
 */
export function useApiMutation<TData, TVariables>(
  { method, url, invalidateKeys }: MutationArgs,
  options?: UseMutationOptions<TData, unknown, TVariables>
) {
  const queryClient = useQueryClient();

  return useMutation<TData, unknown, TVariables>({
    mutationFn: async (variables: TVariables) => {
      const { data } = await axiosClient.request<TData>({
        url,
        method,
        data: variables,
      });
      return data;
    },
    onSuccess: () => {
      if (invalidateKeys) {
        invalidateKeys.forEach((key) =>
          queryClient.invalidateQueries({ queryKey: [key] })
        );
      }
    },
    ...options,
  });
}
