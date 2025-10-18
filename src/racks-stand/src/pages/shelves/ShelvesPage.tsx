import PageMeta from "../../components/common/PageMeta";
import PageBreadcrumb from "../../components/common/PageBreadCrumb";
import ShelfTable from "./components/ShelfTable";
export default function RacksPage() {
  console.log("Shelves page rendered");
  return (
    <>
      <PageMeta
        title="React.js Shelves | TailAdmin - React.js Admin Dashboard Template"
        description="This is React.js Racks page for TailAdmin - React.js Tailwind CSS Admin Dashboard Template"
      />
      <PageBreadcrumb pageTitle="Shelves" />
      <ShelfTable />
    </>
  );
}
