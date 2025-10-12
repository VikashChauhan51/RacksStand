// (table primitives not used in this datatable layout)

import Badge from "../ui/badge/Badge";

import type { WarehouseList } from "./warehousesSchema";

const warehouses: WarehouseList[] = [
  {
    id: "1",
    name: "Warehouse 1",
    prifix: "WH1",
    phone: "123-456-7890",
    email: "info@example.com",
    createdAt: "2023-10-01",
    updatedAt: "2023-10-01",
    manager: "John Doe",
  },
  {
    id: "2",
    name: "Warehouse 2",
    prifix: "WH2",
    phone: "987-654-3210",
    email: "info@example.com",
    createdAt: "2023-10-02",
    updatedAt: "2023-10-02",
    manager: "Jane Smith",
  },
];

export default function WarehouseListTable() {
  return (
    <div className="space-y-5 sm:space-y-6">
      <div className="rounded-2xl border border-gray-200 bg-white dark:border-gray-800 dark:bg-white/[0.03]">
        <div className="px-5 py-4 sm:px-6 sm:py-5">
          <h3 className="text-base font-medium text-gray-800 dark:text-white/90">
            Datatable — Warehouses
          </h3>
        </div>

        <div className="border-t border-gray-100 p-5 sm:p-6 dark:border-gray-800">
          <div className="overflow-hidden rounded-xl border border-gray-200 bg-white pt-4 dark:border-gray-800 dark:bg-white/[0.03]">
            {/* Controls (static, non-interactive) */}
            <div className="mb-4 flex flex-col gap-2 px-4 sm:flex-row sm:items-center sm:justify-between">
              <div className="flex items-center gap-3">
                <span className="text-gray-500 dark:text-gray-400"> Show </span>
                <div className="relative z-20 bg-transparent">
                  <select className="dark:bg-dark-900 h-9 w-full appearance-none rounded-lg border border-gray-300 bg-transparent py-2 pr-8 pl-3 text-sm text-gray-800 placeholder:text-gray-400 focus:ring-0 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90">
                    <option value="10">10</option>
                    <option value="8">8</option>
                    <option value="5">5</option>
                  </select>
                  <span className="absolute top-1/2 right-2 -translate-y-1/2 text-gray-500 dark:text-gray-400">
                    <svg className="stroke-current" width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                      <path d="M3.8335 5.9165L8.00016 10.0832L12.1668 5.9165" strokeWidth="1.2" strokeLinecap="round" strokeLinejoin="round" />
                    </svg>
                  </span>
                </div>
                <span className="text-gray-500 dark:text-gray-400"> entries </span>
              </div>

              <div className="relative">
                <input type="text" placeholder="Search..." className="dark:bg-dark-900 shadow-theme-xs h-11 rounded-lg border border-gray-300 bg-transparent py-2.5 pr-4 pl-4 text-sm text-gray-800 xl:w-[300px] dark:border-gray-700 dark:bg-gray-900 dark:text-white/90" />
              </div>
            </div>

            {/* Table header as grid */}
            <div className="max-w-full overflow-x-auto">
              <div className="min-w-[700px]">
                <div className="grid grid-cols-12 border-t border-gray-200 dark:border-gray-800">
                  <div className="col-span-3 flex items-center border-r border-gray-200 px-4 py-3 dark:border-gray-800">
                    <p className="text-theme-xs font-medium text-gray-700 dark:text-gray-400">Prifix</p>
                  </div>
                  <div className="col-span-6 flex items-center border-r border-gray-200 px-4 py-3 dark:border-gray-800">
                    <p className="text-theme-xs font-medium text-gray-700 dark:text-gray-400">Name</p>
                  </div>
                  <div className="col-span-2 flex items-center border-r border-gray-200 px-4 py-3 dark:border-gray-800">
                    <p className="text-theme-xs font-medium text-gray-700 dark:text-gray-400">Manager</p>
                  </div>
                  <div className="col-span-1 flex items-center px-4 py-3">
                    <p className="text-theme-xs font-medium text-gray-700 dark:text-gray-400">Status</p>
                  </div>
                </div>

                {/* Table body */}
                {warehouses.map((warehouse) => (
                  <div key={warehouse.id} className="grid grid-cols-12 border-t border-gray-100 dark:border-gray-800">
                    <div className="col-span-3 flex items-center border-r border-gray-100 px-4 py-3 dark:border-gray-800">
                      <Badge size="sm">{warehouse.prifix}</Badge>
                    </div>

                    <div className="col-span-6 flex items-center border-r border-gray-100 px-4 py-3 dark:border-gray-800">
                      <div>
                        <p className="text-theme-sm block font-medium text-gray-800 dark:text-white/90">{warehouse.name}</p>
                        <span className="text-sm text-gray-500 dark:text-gray-400">{warehouse.email}</span>
                      </div>
                    </div>

                    <div className="col-span-2 flex items-center border-r border-gray-100 px-4 py-3 dark:border-gray-800">
                      <p className="text-theme-sm text-gray-700 dark:text-gray-400">{warehouse.manager ?? "—"}</p>
                    </div>

                    <div className="col-span-1 flex items-center px-4 py-3">
                      <Badge size="sm" color={warehouse.deleteAt ? "error" : "success"}>
                        {warehouse.deleteAt ? "Inactive" : "Active"}
                      </Badge>
                    </div>
                  </div>
                ))}
              </div>
            </div>

            {/* Pagination (static display) */}
            <div className="border-t border-gray-100 py-4 pr-4 pl-[18px] dark:border-gray-800">
              <div className="flex flex-col xl:flex-row xl:items-center xl:justify-between">
                <p className="border-b border-gray-100 pb-3 text-center text-sm font-medium text-gray-500 xl:border-b-0 xl:pb-0 xl:text-left dark:border-gray-800 dark:text-gray-400">
                  Showing 1 to {warehouses.length} of {warehouses.length} entries
                </p>

                <div className="flex items-center justify-center gap-0.5 pt-4 xl:justify-end xl:pt-0">
                  <button className="shadow-theme-xs mr-2.5 flex h-10 w-10 items-center justify-center rounded-lg border border-gray-300 bg-white text-gray-700 hover:bg-gray-50 disabled:opacity-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400">‹</button>
                  <button className="flex h-10 w-10 items-center justify-center rounded-lg bg-blue-500/[0.08] text-brand-500">1</button>
                  <button className="shadow-theme-xs ml-2.5 flex h-10 w-10 items-center justify-center rounded-lg border border-gray-300 bg-white text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400">›</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
