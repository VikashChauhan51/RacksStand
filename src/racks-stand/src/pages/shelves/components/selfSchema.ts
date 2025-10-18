import { z } from "zod";

export interface SelfList {
  id: string;
  name: string;
  code: string;
  rackId: string;
  createdAt: string;
  updatedAt: string;
  deleteAt?: string;
}

export const SelfSchema = z.object({
  name: z.string().nonempty("This name is required"),
  code: z
    .string()
    .nonempty("This code is required")
    .min(3, "Code must be at least 3 characters")
    .max(10, "Code must be at most 10 characters"),
  label: z.string().optional(),
  level: z.string().optional(),
  description: z.string().optional(),
  length: z.number().min(0, "Length must be a positive number").optional(),
  width: z.number().min(0, "Width must be a positive number").optional(),
  height: z.number().min(0, "Height must be a positive number").optional(),
  xAxis: z.number().optional(),
  yAxis: z.number().optional(),
  zAxis: z.number().optional(),
  diminssionsUnit: z.enum(["cm", "m", "in", "ft"]).optional(),
  status: z.enum(["active", "inactive"]).optional(),
});
export type SelfFormData = z.infer<typeof SelfSchema>;
