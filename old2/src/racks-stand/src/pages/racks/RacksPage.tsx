import PageMeta from "../../components/common/PageMeta";
import PageBreadcrumb from "../../components/common/PageBreadCrumb";
import RacksTable from "./components/RackTable";
export default function RacksPage() {
  console.log("Racks page rendered");
  return (
    <>
      <PageMeta
        title="React.js Racks | TailAdmin - React.js Admin Dashboard Template"
        description="This is React.js Racks page for TailAdmin - React.js Tailwind CSS Admin Dashboard Template"
      />
      <PageBreadcrumb pageTitle="Racks" />
      <RacksTable />
    </>
  );
}
