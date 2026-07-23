import z from "zod";
import { email  } from "./validators/email";
import { alphaNummbericMin6 } from "./validators/num";
import { alphaMin3 } from "./validators/string";



export const userSchema = z.object({
    firstname: alphaMin3,
    secondname:alphaMin3,
    username:alphaNummbericMin6,
    email:email,
    password: z.string().min(8, "Password must be at least 8 characters"),
});
