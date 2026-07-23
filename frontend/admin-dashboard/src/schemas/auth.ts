import z from "zod";
import { email  } from "./validators/email";
import { alphaNummbericMin6 } from "./validators/num";


export const LoginSchema = z.object({
  identifier: z.union([email, alphaNummbericMin6]),
  password: z.string().min(8, "Password must be at least 8 characters"),
});
