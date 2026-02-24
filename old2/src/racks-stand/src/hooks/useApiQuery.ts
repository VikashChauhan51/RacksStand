import { useQuery, type UseQueryOptions } from "@tanstack/react-query";
import { useApiService } from "./useApiService";

export function useApiQuery<TData>(
  key: string | unknown[],
  url: string,
  options?: UseQueryOptions<TData>
) {
  const { get } = useApiService();

  return useQuery<TData>({
    queryKey: Array.isArray(key) ? key : [key],
    queryFn: async () => await get<TData>(url),
    ...options,
  });
}
