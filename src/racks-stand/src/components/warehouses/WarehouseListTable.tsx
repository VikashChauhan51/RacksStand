// (table primitives not used in this datatable layout)

import Badge from "../ui/badge/Badge";
import DataTable from "../tables/BasicTables/Datatable";
import type { Column } from "../ui/table";

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
  const columns: Column<WarehouseList>[] = [
    {
      key: "prifix",
      label: "Prifix",
      render: (w: WarehouseList) => <Badge size="sm">{w.prifix}</Badge>,
      sortable: true,
    },
    {
      key: "name",
      label: "Name",
      render: (w: WarehouseList) => (
        <div>
          <p className="text-theme-sm block font-medium text-gray-800 dark:text-white/90">{w.name}</p>
          <span className="text-sm text-gray-500 dark:text-gray-400">{w.email}</span>
        </div>
      ),
      sortable: true,
    },
    {
      key: "manager",
      label: "Manager",
      render: (w: WarehouseList) => <span className="text-theme-sm text-gray-700 dark:text-gray-400">{w.manager ?? "—"}</span>,
      sortable: true,
    },
    {
      key: "status",
      label: "Status",
      render: (w: WarehouseList) => (
        <Badge size="sm" color={w.deleteAt ? "error" : "success"}>{w.deleteAt ? "Inactive" : "Active"}</Badge>
      ),
    },
  ];

  return (
    <div className="space-y-5 sm:space-y-6">
      <div className="px-5 py-4 sm:px-6 sm:py-5">
        <h3 className="text-base font-medium text-gray-800 dark:text-white/90">Warehouses</h3>
      </div>

      <div className="border-t border-gray-100 p-5 sm:p-6 dark:border-gray-800">
  <DataTable<WarehouseList> data={warehouses} columns={columns} initialPerPage={10} perPageOptions={[10,8,5]} keyField={"id"} />
      </div>
    </div>
  );
}
