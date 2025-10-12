import {
  Table,
  TableBody,
  TableCell,
  TableHeader,
  TableRow,
  type Column,
} from "../../ui/table";

import Badge from "../../ui/badge/Badge";

interface Order {
  id: number;
  user: {
    image: string;
    name: string;
    role: string;
  };
  projectName: string;
  team: {
    images: string[];
  };
  status: string;
  budget: string;
}

// Define the table data using the interface
const tableData: Order[] = [
  {
    id: 1,
    user: {
      image: "/images/user/user-17.jpg",
      name: "Lindsey Curtis",
      role: "Web Designer",
    },
    projectName: "Agency Website",
    team: {
      images: [
        "/images/user/user-22.jpg",
        "/images/user/user-23.jpg",
        "/images/user/user-24.jpg",
      ],
    },
    budget: "3.9K",
    status: "Active",
  },
  {
    id: 2,
    user: {
      image: "/images/user/user-18.jpg",
      name: "Kaiya George",
      role: "Project Manager",
    },
    projectName: "Technology",
    team: {
      images: ["/images/user/user-25.jpg", "/images/user/user-26.jpg"],
    },
    budget: "24.9K",
    status: "Pending",
  },
  {
    id: 3,
    user: {
      image: "/images/user/user-17.jpg",
      name: "Zain Geidt",
      role: "Content Writing",
    },
    projectName: "Blog Writing",
    team: {
      images: ["/images/user/user-27.jpg"],
    },
    budget: "12.7K",
    status: "Active",
  },
  {
    id: 4,
    user: {
      image: "/images/user/user-20.jpg",
      name: "Abram Schleifer",
      role: "Digital Marketer",
    },
    projectName: "Social Media",
    team: {
      images: [
        "/images/user/user-28.jpg",
        "/images/user/user-29.jpg",
        "/images/user/user-30.jpg",
      ],
    },
    budget: "2.8K",
    status: "Cancel",
  },
  {
    id: 5,
    user: {
      image: "/images/user/user-21.jpg",
      name: "Carla George",
      role: "Front-end Developer",
    },
    projectName: "Website",
    team: {
      images: [
        "/images/user/user-31.jpg",
        "/images/user/user-32.jpg",
        "/images/user/user-33.jpg",
      ],
    },
    budget: "4.5K",
    status: "Active",
  },
];

export default function BasicTableOne<T extends Record<string, unknown>>({
  data,
  columns,
  keyField = "id",
  className = "",
}: {
  data?: T[];
  columns?: Column<T>[];
  keyField?: string;
  className?: string;
}) {
  // Default columns and data for the example (backwards compatible)
  const defaultColumns: Column<Order>[] = [
    {
      key: "user",
      label: "User",
      render: (o: Order) => (
        <div className="flex items-center gap-3">
          <div className="w-10 h-10 overflow-hidden rounded-full">
            <img width={40} height={40} src={o.user.image} alt={o.user.name} />
          </div>
          <div>
            <span className="block font-medium text-gray-800 text-theme-sm dark:text-white/90">
              {o.user.name}
            </span>
            <span className="block text-gray-500 text-theme-xs dark:text-gray-400">
              {o.user.role}
            </span>
          </div>
        </div>
      ),
    },
    {
      key: "projectName",
      label: "Project Name",
      render: (o: Order) => o.projectName,
    },
    {
      key: "team",
      label: "Team",
      render: (o: Order) => (
        <div className="flex -space-x-2">
          {o.team.images.map((teamImage: string, index: number) => (
            <div key={index} className="w-6 h-6 overflow-hidden border-2 border-white rounded-full dark:border-gray-900">
              <img width={24} height={24} src={teamImage} alt={`Team member ${index + 1}`} className="w-full size-6" />
            </div>
          ))}
        </div>
      ),
    },
    {
      key: "status",
      label: "Status",
      render: (o: Order) => (
        <Badge
          size="sm"
          color={o.status === "Active" ? "success" : o.status === "Pending" ? "warning" : "error"}
        >
          {o.status}
        </Badge>
      ),
    },
    {
      key: "budget",
      label: "Budget",
      render: (o: Order) => o.budget,
    },
  ];

  const usedColumns = (columns as Column<T>[] | undefined) ?? (defaultColumns as unknown as Column<T>[]);
  const usedData = (data && data.length ? data : (tableData as unknown as T[])) as T[];

  return (
    <div className={`overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-white/[0.05] dark:bg-white/[0.03] ${className}`}>
      <div className="max-w-full overflow-x-auto">
        <Table>
          <TableHeader className="border-b border-gray-100 dark:border-white/[0.05]">
            <TableRow>
              {usedColumns.map((col) => (
                <TableCell key={col.key} isHeader className="px-5 py-3 font-medium text-gray-500 text-start text-theme-xs dark:text-gray-400">
                  {col.label}
                </TableCell>
              ))}
            </TableRow>
          </TableHeader>

          <TableBody className="divide-y divide-gray-100 dark:divide-white/[0.05]">
            {usedData.map((row, i) => (
              <TableRow key={String((row as unknown as Record<string, unknown>)[keyField] ?? i)}>
                {usedColumns.map((col) => (
                  <TableCell key={col.key} className="px-4 py-3 text-gray-500 text-start text-theme-sm dark:text-gray-400">
                    {col.render ? col.render(row) : String((row as unknown as Record<string, unknown>)[col.key] ?? "")}
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </div>
  );
}
