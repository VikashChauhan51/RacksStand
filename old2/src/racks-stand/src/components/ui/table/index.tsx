import type { ReactNode } from "react";

// Props for Table
interface TableProps {
  children: ReactNode; // Table content (thead, tbody, etc.)
  className?: string; // Optional className for styling
}

// Props for TableHeader
interface TableHeaderProps {
  children: ReactNode; // Header row(s)
  className?: string; // Optional className for styling
}

// Props for TableBody
interface TableBodyProps {
  children: ReactNode; // Body row(s)
  className?: string; // Optional className for styling
}

// Props for TableRow
interface TableRowProps {
  children: ReactNode; // Cells (th or td)
  className?: string; // Optional className for styling
}

// Props for TableCell
interface TableCellProps {
  children: ReactNode; // Cell content
  isHeader?: boolean; // If true, renders as <th>, otherwise <td>
  className?: string; // Optional className for styling
}

// Generic column descriptor used by higher-level data table components
export type Column<T> = {
  key: string;
  label: string;
  render?: (item: T) => React.ReactNode;
  sortable?: boolean;
  className?: string;
};

// DataTable props shared type so reusable datatables can import it
export type DataTableProps<T> = {
  data: T[];
  columns: Column<T>[];
  perPageOptions?: number[];
  initialPerPage?: number;
  className?: string;
  keyField?: string | keyof T;
  totalCount?: number;
  serverSide?: boolean;
  onPageChange?: (page: number) => void;
  maxPageButtons?: number;
  /** Optional actions area rendered next to the search input */
  actions?: ReactNode;
  /** Simple add button config (renders a primary button) */
  addAction?: { label?: string; onClick: () => void };
  /** Enable export to CSV button (exports currently filtered data) */
  exportCsv?: boolean;
  /** File name for CSV export */
  exportFileName?: string;
};

// Table Component
const Table: React.FC<TableProps> = ({ children, className }) => {
  return <table className={`min-w-full  ${className}`}>{children}</table>;
};

// TableHeader Component
const TableHeader: React.FC<TableHeaderProps> = ({ children, className }) => {
  return <thead className={className}>{children}</thead>;
};

// TableBody Component
const TableBody: React.FC<TableBodyProps> = ({ children, className }) => {
  return <tbody className={className}>{children}</tbody>;
};

// TableRow Component
const TableRow: React.FC<TableRowProps> = ({ children, className }) => {
  return <tr className={className}>{children}</tr>;
};

// TableCell Component
const TableCell: React.FC<TableCellProps> = ({
  children,
  isHeader = false,
  className,
}) => {
  const CellTag = isHeader ? "th" : "td";
  return <CellTag className={` ${className}`}>{children}</CellTag>;
};



export { Table, TableHeader, TableBody, TableRow, TableCell };
