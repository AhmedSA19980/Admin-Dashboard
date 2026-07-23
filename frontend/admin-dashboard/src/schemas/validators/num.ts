import {z} from "zod";


export const alphaNummbericMin6 = z
  .string()
  .min(6, "Must be at least 6 characters")
  .regex(/^[A-Za-z0-9_]+$/, "Invalid input! only accept(0-9,aA-zZ,_)");


  