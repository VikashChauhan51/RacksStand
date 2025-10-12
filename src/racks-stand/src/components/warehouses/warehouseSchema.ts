import { z } from "zod";


// Define Address schema separately
export const WarehouseAddressSchema = z.object({
  address: z.string().nonempty("Address is required"),
  address2: z.string().optional(),
  city: z.string().optional(),
  state: z.string().optional(),
  zip: z.string().optional(),
  country: z.string().optional(),
});


export const WarehouseSchema = z.object({
  name: z.string().nonempty("This name is required"),
  prifix: z.string().nonempty("This prifix is required").length(3, "Prifix must be 3 characters"),
  phone: z.string().optional(),
  email: z.email("Please enter a valid email address").optional(),
  manager: z.string().optional(),
  length: z.number().min(0, "Length must be a positive number").optional(),
  width: z.number().min(0, "Width must be a positive number").optional(),
  height: z.number().min(0, "Height must be a positive number").optional(),
  diminssionsUnit: z.enum(["cm", "m", "in", "ft"]).optional(),
  description: z.string().optional(),
  address: WarehouseAddressSchema,
  returnAddress: WarehouseAddressSchema,
});

export type WarehouseFormData = z.infer<typeof WarehouseSchema>;
export type WarehouseAddressFormData = z.infer<typeof WarehouseAddressSchema>;
