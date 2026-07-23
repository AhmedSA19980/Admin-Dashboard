import {z} from "zod";




export const alphaMin3 = z
  .string()
  .min(3, "Must be at least 6 characters")
  .regex(/^[A-Za-z]+$/, "Only letters is allowed");

