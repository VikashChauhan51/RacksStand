import PageMeta from "../../components/common/PageMeta";
import PageBreadcrumb from "../../components/common/PageBreadCrumb";
import WarehouseListTable from "../../components/warehouses/WarehouseListTable";
export default function Warehouses() {
  return (
    <>
      <PageMeta
        title="React.js Warehouses | TailAdmin - React.js Admin Dashboard Template"
        description="This is React.js Warehouses page for TailAdmin - React.js Tailwind CSS Admin Dashboard Template"
      />
      <PageBreadcrumb pageTitle="Warehouses" />
      <WarehouseListTable />
    </>
  );
}
