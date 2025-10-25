import { z } from "zod";

export const SignUpSchema = z.object({
  fullName: z
    .string("Please enter your name")
    .nonempty("This name is required"),
  email: z.email("Please enter a valid email address"),
  password: z.string().min(6, "Password must be at least 6 characters"),
  agreeTerms: z.boolean().refine((val) => val === true, {
    message: "You must agree to the terms and conditions",
  }),
});

export type SignUpFormData = z.infer<typeof SignUpSchema>;

export interface SignUpPayload {
  full_name: string;
  email: string;
  password: string;
  agreed_terms_confirmed: boolean;
  redirect_url: string;
}
