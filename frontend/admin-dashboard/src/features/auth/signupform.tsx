"use client"
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
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
import { useForm } from "react-hook-form";
import z from "zod";
import { userSchema } from "@/schemas/user";
import { zodResolver } from "@hookform/resolvers/zod";



type formData = z.infer<typeof userSchema>

export default function SignUpForm() {

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<formData>({
    resolver: zodResolver(userSchema),
    mode: "onSubmit",
  });
  
    const onSubmit = (data: formData) => {
      console.log(data);
    };
    const onError = (errors: any) => {
      console.log(errors);
    };
    
  return (
    <Card className="w-full max-w-md">
      <CardHeader>
        <CardTitle>Register A New Account</CardTitle>
        <CardAction>
          <Button className="text-lg" variant="link">
            Login
          </Button>
        </CardAction>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit(onSubmit, onError)} method="POST">
          <div className="flex flex-col gap-6">
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="first_name">
                first name
              </Label>
              <Input
                {...register("firstname")}
                className="text-lg font-semibold"
                id="first_name"
                type="text"
                placeholder="Mohammed"
              ></Input>
              <p className="mt-1 font-bold text-sm text-red-700 text-shadow-2xs">
                {errors.firstname?.message}
              </p>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="second_name">
                second name
              </Label>
              <Input
                {...register("secondname")}
                className="text-lg font-semibold"
                id="second_name"
                type="text"
                placeholder="Ibaraham"
              ></Input>
              <p className="mt-1 font-bold text-sm text-red-700 text-shadow-2xs">
                {errors.secondname?.message}
              </p>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="username">
                username
              </Label>
              <Input
                {...register("username")}
                className="text-lg font-semibold"
                id="username"
                type="text"
                placeholder="admin_123"
              ></Input>
              <p className="mt-1 font-bold text-sm text-red-700 text-shadow-2xs">
                {errors.username?.message}
              </p>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="email">
                Email
              </Label>
              <Input
                {...register("email")}
                className="text-lg font-semibold"
                id="email"
                type="email"
                placeholder="example@gmail.com "
              ></Input>
              <p className="mt-1 font-bold text-sm text-red-700 text-shadow-2xs">
                {errors.email?.message}
              </p>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="password">
                Password
              </Label>
              <Input
                {...register("password")}
                className="text-lg font-semibold"
                id="password"
                type="password"
                required
              />
              <p className="mt-1 font-bold text-sm text-red-700 text-shadow-2xs">
                {errors.password?.message}
              </p>
            </div>
          </div>
          <CardFooter className="flex-col gap-2">
            <Button type="submit" className="w-full ">
              Register
            </Button>
            {/*<Button variant="outline" className="w-full">
              Login with Google
            </Button>
            */}
          </CardFooter>
        </form>
      </CardContent>
    </Card>
  );
}
