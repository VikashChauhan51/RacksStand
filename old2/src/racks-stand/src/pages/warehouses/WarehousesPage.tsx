import PageMeta from "../../components/common/PageMeta";
import PageBreadcrumb from "../../components/common/PageBreadCrumb";
import WarehouseTable from "./components/WarehouseTable";
export default function WarehousesPage() {
  console.log("Warehouses page rendered");
  return (
    <>
      <PageMeta
        title="React.js Warehouses | TailAdmin - React.js Admin Dashboard Template"
        description="This is React.js Warehouses page for TailAdmin - React.js Tailwind CSS Admin Dashboard Template"
      />
      <PageBreadcrumb pageTitle="Warehouses" />
      <WarehouseTable />
    </>
  );
}
