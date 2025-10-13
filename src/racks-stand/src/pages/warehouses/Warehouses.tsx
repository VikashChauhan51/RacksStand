import PageMeta from "../../components/common/PageMeta";
import PageBreadcrumb from "../../components/common/PageBreadCrumb";
import WarehouseTable from "../../components/warehouses/WarehouseTable";
export default function Warehouses() {
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
