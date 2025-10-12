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
    {
      key: "actions",
      label: "Actions",
      render: (w: WarehouseList) => (
        <div className="flex w-full items-center gap-2">
          <button onClick={() => handleEdit(w.id)} className="hover:text-error-500 dark:hover:text-error-500 text-gray-500 dark:text-gray-400">
            <svg className="fill-current" width="21" height="21" viewBox="0 0 21 21" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path fillRule="evenodd" clipRule="evenodd" d="M7.04142 4.29199C7.04142 3.04935 8.04878 2.04199 9.29142 2.04199H11.7081C12.9507 2.04199 13.9581 3.04935 13.9581 4.29199V4.54199H16.1252H17.166C17.5802 4.54199 17.916 4.87778 17.916 5.29199C17.916 5.70621 17.5802 6.04199 17.166 6.04199H16.8752V8.74687V13.7469V16.7087C16.8752 17.9513 15.8678 18.9587 14.6252 18.9587H6.37516C5.13252 18.9587 4.12516 17.9513 4.12516 16.7087V13.7469V8.74687V6.04199H3.8335C3.41928 6.04199 3.0835 5.70621 3.0835 5.29199C3.0835 4.87778 3.41928 4.54199 3.8335 4.54199H4.87516H7.04142V4.29199ZM15.3752 13.7469V8.74687V6.04199H13.9581H13.2081H7.79142H7.04142H5.62516V8.74687V13.7469V16.7087C5.62516 17.1229 5.96095 17.4587 6.37516 17.4587H14.6252C15.0394 17.4587 15.3752 17.1229 15.3752 16.7087V13.7469ZM8.54142 4.54199H12.4581V4.29199C12.4581 3.87778 12.1223 3.54199 11.7081 3.54199H9.29142C8.87721 3.54199 8.54142 3.87778 8.54142 4.29199V4.54199ZM8.8335 8.50033C9.24771 8.50033 9.5835 8.83611 9.5835 9.25033V14.2503C9.5835 14.6645 9.24771 15.0003 8.8335 15.0003C8.41928 15.0003 8.0835 14.6645 8.0835 14.2503V9.25033C8.0835 8.83611 8.41928 8.50033 8.8335 8.50033ZM12.9168 9.25033C12.9168 8.83611 12.581 8.50033 12.1668 8.50033C11.7526 8.50033 11.4168 8.83611 11.4168 9.25033V14.2503C11.4168 14.6645 11.7526 15.0003 12.1668 15.0003C12.581 15.0003 12.9168 14.6645 12.9168 14.2503V9.25033Z" fill="" />
            </svg>
          </button>

          <button onClick={() => handleDelete(w.id)} className="text-gray-500 hover:text-gray-800 dark:text-gray-400 dark:hover:text-white/90">
            <svg className="fill-current" width="21" height="21" viewBox="0 0 21 21" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path fillRule="evenodd" clipRule="evenodd" d="M17.0911 3.53206C16.2124 2.65338 14.7878 2.65338 13.9091 3.53206L5.6074 11.8337C5.29899 12.1421 5.08687 12.5335 4.99684 12.9603L4.26177 16.445C4.20943 16.6931 4.286 16.9508 4.46529 17.1301C4.64458 17.3094 4.90232 17.3859 5.15042 17.3336L8.63507 16.5985C9.06184 16.5085 9.45324 16.2964 9.76165 15.988L18.0633 7.68631C18.942 6.80763 18.942 5.38301 18.0633 4.50433L17.0911 3.53206ZM14.9697 4.59272C15.2626 4.29982 15.7375 4.29982 16.0304 4.59272L17.0027 5.56499C17.2956 5.85788 17.2956 6.33276 17.0027 6.62565L16.1043 7.52402L14.0714 5.49109L14.9697 4.59272ZM13.0107 6.55175L6.66806 12.8944C6.56526 12.9972 6.49455 13.1277 6.46454 13.2699L5.96704 15.6283L8.32547 15.1308C8.46772 15.1008 8.59819 15.0301 8.70099 14.9273L15.0436 8.58468L13.0107 6.55175Z" fill="" />
            </svg>
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
        .join(","),
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
      <button onClick={handleDownload} className="shadow-theme-xs flex w-full items-center justify-center gap-2 rounded-lg border border-gray-300 bg-white px-4 py-[11px] text-sm font-medium text-gray-700 sm:w-auto dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400">
        Download

        <svg className="fill-current" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path fillRule="evenodd" clipRule="evenodd" d="M10.0018 14.083C9.7866 14.083 9.59255 13.9924 9.45578 13.8472L5.61586 10.0097C5.32288 9.71688 5.32272 9.242 5.61552 8.94902C5.90832 8.65603 6.3832 8.65588 6.67618 8.94868L9.25182 11.5227L9.25182 3.33301C9.25182 2.91879 9.5876 2.58301 10.0018 2.58301C10.416 2.58301 10.7518 2.91879 10.7518 3.33301L10.7518 11.5193L13.3242 8.94866C13.6172 8.65587 14.0921 8.65604 14.3849 8.94903C14.6777 9.24203 14.6775 9.7169 14.3845 10.0097L10.5761 13.8154C10.4385 13.979 10.2323 14.083 10.0018 14.083ZM4.0835 13.333C4.0835 12.9188 3.74771 12.583 3.3335 12.583C2.91928 12.583 2.5835 12.9188 2.5835 13.333V15.1663C2.5835 16.409 3.59086 17.4163 4.8335 17.4163H15.1676C16.4102 17.4163 17.4176 16.409 17.4176 15.1663V13.333C17.4176 12.9188 17.0818 12.583 16.6676 12.583C16.2533 12.583 15.9176 12.9188 15.9176 13.333V15.1663C15.9176 15.5806 15.5818 15.9163 15.1676 15.9163H4.8335C4.41928 15.9163 4.0835 15.5806 4.0835 15.1663V13.333Z" fill="" />
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
        <h3 className="text-base font-medium text-gray-800 dark:text-white/90">Warehouses</h3>
      </div>

      <div className="border-t border-gray-100 p-5 sm:p-6 dark:border-gray-800">
  <DataTable<WarehouseList>
          data={warehouses}
          columns={columns}
          initialPerPage={10}
          perPageOptions={[10,8,5]}
          keyField={"id"}
          actions={actions}
          addAction={{ label: "Add", onClick: handleAdd }}
        />
      </div>
    </div>
  );
}
