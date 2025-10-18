import Badge from "../../../components/ui/badge/Badge";
import DataTable from "../../../components/tables/BasicTables/Datatable";
import type { Column } from "../../../components/ui/table";
import { PencilIcon, TrashBinIcon,DownloadIcon } from "../../../icons";
import type { WarehouseList } from "./warehouseSchema";
import WarehouseModel from "./WarehouseModel";
import { useModal } from "../../../hooks/useModal";
import { useState } from "react";

// Sample data

const warehousesData: WarehouseList[] = [
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

export default function WarehouseTable() {
  console.log("WarehouseTable component rendered");
  const { isOpen, openModal, closeModal } = useModal();
  const [editingId, setEditingId] = useState<string | undefined>(undefined);
  const [warehouses, setWarehouses] = useState<WarehouseList[]>(warehousesData);

  const handleAdd = () => {
    setEditingId(undefined);
    openModal();
  };

  function handleEdit(id: string) {
    console.log("Edit warehouse", id);
    setEditingId(id);
    openModal();
  }

  function handleDelete(id: string) {
    if (confirm("Are you sure you want to delete this warehouse?")) {
      console.log("Delete warehouse", id);
      // TODO: perform delete action
    }
  }

  function onSave(warehouse: WarehouseList) {
    const index = warehouses.findIndex((w) => w.id === warehouse.id);
    if (index !== -1) {
      setWarehouses((prev) => {
        const newWarehouses = [...prev];
        newWarehouses[index] = warehouse;
        return newWarehouses;
      });
    } else {
      setWarehouses((prev) => [...prev, warehouse]);
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
  const actions = (
    <>
      <button
        onClick={handleDownload}
        className="shadow-theme-xs flex w-full items-center justify-center gap-2 rounded-lg border border-gray-300 bg-white px-4 py-[11px] text-sm font-medium text-gray-700 sm:w-auto dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400"
      >
        Download
        <DownloadIcon />
      </button>
    </>
  );

  return (
    <>
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
      {isOpen && (
        <WarehouseModel
          isOpen={isOpen}
          closeModal={closeModal}
          id={editingId}
          onSave={onSave}
        />
      )}
    </>
  );
}
