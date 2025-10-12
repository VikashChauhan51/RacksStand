// (table primitives not used in this datatable layout)

import Badge from "../ui/badge/Badge";
import DataTable from "../tables/BasicTables/Datatable";
import type { Column } from "../ui/table";
import { PencilIcon, TrashBinIcon } from "../../icons";

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
          <p className="text-theme-sm block font-medium text-gray-800 dark:text-white/90">
            {w.name}
          </p>
          <span className="text-sm text-gray-500 dark:text-gray-400">
            {w.email}
          </span>
        </div>
      ),
      sortable: true,
    },
    {
      key: "manager",
      label: "Manager",
      render: (w: WarehouseList) => (
        <span className="text-theme-sm text-gray-700 dark:text-gray-400">
          {w.manager ?? "—"}
        </span>
      ),
      sortable: true,
    },
    {
      key: "status",
      label: "Status",
      render: (w: WarehouseList) => (
        <Badge size="sm" color={w.deleteAt ? "error" : "success"}>
          {w.deleteAt ? "Inactive" : "Active"}
        </Badge>
      ),
    },
    {
      key: "actions",
      label: "Actions",
      render: (w: WarehouseList) => (
        <div className="flex w-full items-center gap-2">
          <button
            onClick={() => handleEdit(w.id)}
            className="text-gray-500 hover:text-gray-800 dark:text-gray-400 dark:hover:text-white/90"
          >
            <PencilIcon className="fill-current w-5 h-5" />
          </button>

          <button
            onClick={() => handleDelete(w.id)}
            className="hover:text-error-500 dark:hover:text-error-500 text-gray-500 dark:text-gray-400"
          >
            <TrashBinIcon className="fill-current w-5 h-5" />
          </button>
        </div>
      ),
    },
  ];

  function handleEdit(id: string) {
    // placeholder - open edit modal or navigate
    console.log("Edit warehouse", id);
  }

  function handleDelete(id: string) {
    if (confirm("Are you sure you want to delete this warehouse?")) {
      console.log("Delete warehouse", id);
      // TODO: perform delete action
    }
  }

  // CSV export helper (uses columns keys)
  function toCsv(rows: WarehouseList[]) {
    const keys = columns.map((c) => c.key);
    const header = keys.map((k) => `"${k.replace(/"/g, '""')}"`).join(",");
    const lines = rows.map((r) =>
      keys
        .map((k) => {
          const v = String((r as unknown as Record<string, unknown>)[k] ?? "");
          return `"${v.replace(/"/g, '""')}"`;
        })
        .join(",")
    );
    return [header, ...lines].join("\n");
  }

  function handleDownload() {
    const csv = toCsv(warehouses);
    const blob = new Blob([csv], { type: "text/csv;charset=utf-8;" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = "warehouses.csv";
    a.click();
    URL.revokeObjectURL(url);
  }

  const actions = (
    <>
      <button
        onClick={handleDownload}
        className="shadow-theme-xs flex w-full items-center justify-center gap-2 rounded-lg border border-gray-300 bg-white px-4 py-[11px] text-sm font-medium text-gray-700 sm:w-auto dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400"
      >
        Download
        <svg
          className="fill-current"
          width="20"
          height="20"
          viewBox="0 0 20 20"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            fillRule="evenodd"
            clipRule="evenodd"
            d="M10.0018 14.083C9.7866 14.083 9.59255 13.9924 9.45578 13.8472L5.61586 10.0097C5.32288 9.71688 5.32272 9.242 5.61552 8.94902C5.90832 8.65603 6.3832 8.65588 6.67618 8.94868L9.25182 11.5227L9.25182 3.33301C9.25182 2.91879 9.5876 2.58301 10.0018 2.58301C10.416 2.58301 10.7518 2.91879 10.7518 3.33301L10.7518 11.5193L13.3242 8.94866C13.6172 8.65587 14.0921 8.65604 14.3849 8.94903C14.6777 9.24203 14.6775 9.7169 14.3845 10.0097L10.5761 13.8154C10.4385 13.979 10.2323 14.083 10.0018 14.083ZM4.0835 13.333C4.0835 12.9188 3.74771 12.583 3.3335 12.583C2.91928 12.583 2.5835 12.9188 2.5835 13.333V15.1663C2.5835 16.409 3.59086 17.4163 4.8335 17.4163H15.1676C16.4102 17.4163 17.4176 16.409 17.4176 15.1663V13.333C17.4176 12.9188 17.0818 12.583 16.6676 12.583C16.2533 12.583 15.9176 12.9188 15.9176 13.333V15.1663C15.9176 15.5806 15.5818 15.9163 15.1676 15.9163H4.8335C4.41928 15.9163 4.0835 15.5806 4.0835 15.1663V13.333Z"
            fill=""
          />
        </svg>
      </button>
    </>
  );

  const handleAdd = () => {
    // placeholder: open create modal or navigate
    // Implement as needed in your app
    console.log("Add Warehouse clicked");
  };

  return (
    <div className="rounded-2xl border border-gray-200 bg-white dark:border-gray-800 dark:bg-white/[0.03]">
      <div className="px-5 py-4 sm:px-6 sm:py-5">
        <h3 className="text-base font-medium text-gray-800 dark:text-white/90">
          Warehouses
        </h3>
      </div>

      <div className="border-t border-gray-100 p-5 sm:p-6 dark:border-gray-800">
        <DataTable<WarehouseList>
          data={warehouses}
          columns={columns}
          initialPerPage={10}
          perPageOptions={[10, 8, 5]}
          keyField={"id"}
          actions={actions}
          addAction={{ label: "Add", onClick: handleAdd }}
        />
      </div>
    </div>
  );
}
