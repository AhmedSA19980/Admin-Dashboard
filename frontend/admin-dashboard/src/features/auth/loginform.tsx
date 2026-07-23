"use client"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import {
  Card,
  CardAction,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
  
} from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { LoginSchema } from "@/schemas/auth";
import z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

import { FieldErrors, useForm} from "react-hook-form";

type formData = z.infer<typeof LoginSchema>

export default function LoginForm()  {

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<formData>({
    
    resolver: zodResolver(LoginSchema),
  mode:"onSubmit"
    
  });

  const onSubmit = (data:formData) => {
    
    //console.log(data);
  }
  const onError = (errors: FieldErrors<formData>) => {
   // console.log(errors);
  };

    return (
      <Card className="w-full max-w-sm">
        <CardHeader>
          <CardTitle>Login To your Account</CardTitle>
          <CardDescription>
            Enter your email | username below to login to your account
          </CardDescription>
          <CardAction>
            <Button className="text-lg" variant="link">
              Sign Up
            </Button>
          </CardAction>
        </CardHeader>
        <CardContent>
          <form
            onSubmit={handleSubmit(onSubmit, onError)}
            method="POST"
            noValidate
          >
            <div className="flex flex-col gap-6">
              <div className="grid gap-2">
                <Label className="text-lg" htmlFor="identifier">
                  Email | username
                </Label>
                <Input
                  {...register("identifier")}
                  className="text-lg font-semibold"
                  id="identifier"
                  type="text"
                  placeholder="example@gmail.com | admin_123"
                ></Input>
                <p className="mt-1 font-bold text-sm text-red-700 text-shadow-2xs">
                  {errors.identifier?.message}
                </p>
              </div>
              <div className="grid gap-2">
                <div className="flex items-center">
                  <Label className="text-lg" htmlFor="password">
                    Password
                  </Label>
                  <a
                    href="#"
                    className="ml-auto inline-block text-sm underline-offset-4 hover:underline"
                  >
                    Forgot your password?
                  </a>
                </div>
                <Input
                  {...register("password")}
                  className="text-lg font-semibold"
                  id="password"
                  type="password"
                />
                <p className="mt-1 font-bold text-sm text-red-700 text-shadow-2xs">
                  {errors.password?.message}
                </p>
              </div>
            </div>
            <CardFooter className="flex-col gap-2">
              <Button type="submit" className="w-full">
                Login
              </Button>
              {/*<Button variant="outline" className="w-full">
                signup with Google
              </Button>*/}
            </CardFooter>
          </form>
        </CardContent>
      </Card>
    );
}
