import { useQuery, type UseQueryOptions } from "@tanstack/react-query";
import axiosClient from "../api/axiosClient";
export function useApiQuery<TData>(
  key: string | unknown[],
  url: string,
  options?: UseQueryOptions<TData>
) {
  return useQuery<TData>({
    queryKey: Array.isArray(key) ? key : [key],
    queryFn: async () => {
      const { data } = await axiosClient.get<TData>(url);
      return data;
    },
    ...options,
  });
}
