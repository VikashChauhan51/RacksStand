import { useMemo, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableHeader,
  TableRow,
} from "../../ui/table";
import type { DataTableProps } from "../../ui/table";

function Pagination({
  currentPage,
  totalPages,
  onPage,
  maxButtons = 10,
}: {
  currentPage: number;
  totalPages: number;
  onPage: (p: number) => void;
  maxButtons?: number;
}) {
  const pages = useMemo(() => {
    const groupIndex = Math.floor((currentPage - 1) / maxButtons);
    const start = groupIndex * maxButtons + 1;
    const end = Math.min(totalPages, start + maxButtons - 1);
    const arr: number[] = [];
    for (let i = start; i <= end; i++) arr.push(i);
    return { arr, start, end };
  }, [currentPage, totalPages, maxButtons]);

  const { arr, start, end } = pages;

  return (
    <div className="flex items-center justify-center gap-2">
      <button
        onClick={() => onPage(Math.max(1, currentPage - 1))}
        className="px-3 py-2 rounded-lg border"
        disabled={currentPage === 1}
      >
        Prev
      </button>

      {/* Jump back by a page group */}
      {start > 1 && (
        <button
          onClick={() => onPage(Math.max(1, start - 1))}
          className="px-3 py-2 rounded-lg border"
        >
          ...
        </button>
      )}

      {arr.map((p) => (
        <button
          key={p}
          onClick={() => onPage(p)}
          className={`px-3 py-2 rounded-lg ${currentPage === p ? "bg-blue-50 text-blue-600" : ""}`}
        >
          {p}
        </button>
      ))}

      {/* Jump forward by a page group */}
      {end < totalPages && (
        <button
          onClick={() => onPage(Math.min(totalPages, end + 1))}
          className="px-3 py-2 rounded-lg border"
        >
          ...
        </button>
      )}

      <button
        onClick={() => onPage(Math.min(totalPages, currentPage + 1))}
        className="px-3 py-2 rounded-lg border"
        disabled={currentPage === totalPages}
      >
        Next
      </button>
    </div>
  );
}

export default function DataTable<T>({
  data,
  columns,
  perPageOptions = [10, 8, 5],
  initialPerPage = 10,
  className,
  keyField = "id",
  totalCount,
  serverSide = false,
  onPageChange,
  maxPageButtons = 10,
}: DataTableProps<T>) {
  const [search, setSearch] = useState("");
  const [perPage, setPerPage] = useState(initialPerPage);
  const [currentPage, setCurrentPage] = useState(1);
  const [sortColumn, setSortColumn] = useState<string | null>(null);
  const [sortDirection, setSortDirection] = useState<"asc" | "desc">("asc");

  const filtered = useMemo(() => {
    const lower = search.toLowerCase();
    let filteredData = data.filter((item) =>
      Object.values(item as Record<string, unknown>).some((v) =>
        String(v).toLowerCase().includes(lower),
      ),
    );

    if (sortColumn) {
      filteredData = filteredData.sort((a, b) => {
  const aVal = (a as Record<string, unknown>)[sortColumn as string];
  const bVal = (b as Record<string, unknown>)[sortColumn as string];
        if (aVal == null) return -1;
        if (bVal == null) return 1;
        if (aVal < bVal) return sortDirection === "asc" ? -1 : 1;
        if (aVal > bVal) return sortDirection === "asc" ? 1 : -1;
        return 0;
      });
    }

    return filteredData;
  }, [data, search, sortColumn, sortDirection]);

  const totalEntries = typeof totalCount === "number" ? totalCount : filtered.length;
  const totalPages = Math.max(1, Math.ceil(totalEntries / perPage));

  const paginated = useMemo(() => {
    const start = (currentPage - 1) * perPage;
    // If serverSide, `data` is expected to already be the current page slice
    if (serverSide) return data;
    return filtered.slice(start, start + perPage);
  }, [filtered, currentPage, perPage, serverSide, data]);

  // Notify parent when page changes in server-side mode
  function handlePage(p: number) {
    setCurrentPage(p);
    if (serverSide && onPageChange) onPageChange(p);
  }

  function toggleSort(colKey: string, sortable?: boolean) {
    if (!sortable) return;
    if (sortColumn === colKey) {
      setSortDirection((d) => (d === "asc" ? "desc" : "asc"));
    } else {
      setSortColumn(colKey);
      setSortDirection("asc");
    }
  }

  return (
    <div className={`rounded-xl border border-gray-200 bg-white dark:border-gray-800 dark:bg-white/[0.03] ${className || ""}`}>
      <div className="px-4 py-3 sm:px-6 sm:py-4">
        <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
          <div className="flex items-center gap-3">
            <span className="text-gray-500 dark:text-gray-400">Show</span>
            <select
              value={perPage}
              onChange={(e) => { setPerPage(Number(e.target.value)); setCurrentPage(1); }}
              className="h-9 rounded-lg border border-gray-300 bg-transparent py-1 px-2 text-sm"
            >
              {perPageOptions.map((opt) => (
                <option key={opt} value={opt}>{opt}</option>
              ))}
            </select>
            <span className="text-gray-500 dark:text-gray-400">entries</span>
          </div>

          <div className="relative">
            <input
              value={search}
              onChange={(e) => { setSearch(e.target.value); setCurrentPage(1); }}
              placeholder="Search..."
              className="h-11 rounded-lg border border-gray-300 bg-transparent py-2.5 pl-3 pr-4 text-sm"
            />
          </div>
        </div>
      </div>

      <div className="max-w-full overflow-x-auto">
        <div className="min-w-[700px]">
          <Table>
            <TableHeader className="border-t border-gray-200 dark:border-gray-800">
              <TableRow>
                {columns.map((col) => (
                  <TableCell
                    key={col.key}
                    isHeader
                    className={`px-4 py-3 font-medium text-gray-500 text-start text-theme-xs dark:text-gray-400 ${col.className || ""}`}
                  >
                    <div
                      className="flex items-center justify-between cursor-pointer"
                      onClick={() => toggleSort(col.key, !!col.sortable)}
                    >
                      <span>{col.label}</span>
                      {col.sortable && <span className="ml-2 text-gray-300">⇅</span>}
                    </div>
                  </TableCell>
                ))}
              </TableRow>
            </TableHeader>

            <TableBody className="divide-y divide-gray-100 dark:divide-white/[0.05]">
              {paginated.map((row) => (
                <TableRow key={String((row as Record<string, unknown>)[String(keyField)])}>
                  {columns.map((col) => (
                        <TableCell key={col.key} className={`px-4 py-3 text-gray-500 text-start text-theme-sm ${col.className || ""}`}>
                          {col.render ? col.render(row) : String((row as Record<string, unknown>)[col.key] ?? "")}
                        </TableCell>
                      ))}
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      </div>

      <div className="border-t border-gray-100 py-4 pr-4 pl-[18px] dark:border-gray-800">
        <div className="flex flex-col xl:flex-row xl:items-center xl:justify-between">
          <p className="border-b border-gray-100 pb-3 text-center text-sm font-medium text-gray-500 xl:border-b-0 xl:pb-0 xl:text-left dark:border-gray-800 dark:text-gray-400">
            Showing {Math.min((currentPage - 1) * perPage + 1, totalEntries)} to {Math.min(currentPage * perPage, totalEntries)} of {totalEntries} entries
          </p>

            <div className="pt-3 xl:pt-0">
            <Pagination currentPage={currentPage} totalPages={totalPages} onPage={handlePage} maxButtons={maxPageButtons} />
          </div>
        </div>
      </div>
    </div>
  );
}
