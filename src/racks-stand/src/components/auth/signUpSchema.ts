import { z } from "zod";

export const signUpSchema = z.object({
  firstName: z.string("Please enter first name").nonempty("This first name is required"),
  lastName: z.string("Please enter last name").nonempty("This first name is required"),
  email: z.email("Please enter a valid email address"),
  password: z.string().min(6, "Password must be at least 6 characters"),
  agreeTerms: z.boolean()
    .refine((val) => val === true, {
      message: "You must agree to the terms and conditions",
    }),
});

export type SignUpFormData = z.infer<typeof signUpSchema>;
