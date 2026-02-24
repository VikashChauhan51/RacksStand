import PageMeta from "../../components/common/PageMeta";
import PageBreadcrumb from "../../components/common/PageBreadCrumb";
import BoxTable from "./components/BoxTable";
export default function RacksPage() {
  console.log("Boxes page rendered");
  return (
    <>
      <PageMeta
        title="React.js Boxes | TailAdmin - React.js Admin Dashboard Template"
        description="This is React.js Boxes page for TailAdmin - React.js Tailwind CSS Admin Dashboard Template"
      />
      <PageBreadcrumb pageTitle="Boxes" />
      <BoxTable />
    </>
  );
}
