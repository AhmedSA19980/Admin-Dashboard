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




export default function LoginForm()  {


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
          <form method="POST">
            <div className="flex flex-col gap-6">
              <div className="grid gap-2">
                <Label className="text-lg" htmlFor="identifier">
                  Email | username
                </Label>
                <Input
                  className="text-lg font-semibold"
                  id="identifier"
                  type="text"
                  placeholder="example@gmail.com | admin_123"
                ></Input>
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
                  className="text-lg font-semibold"
                  id="password"
                  type="password"
                  required
                />
              </div>
            </div>
          </form>
        </CardContent>
        <CardFooter className="flex-col gap-2">
          <Button type="submit" className="w-full">
            Login
          </Button>
          <Button variant="outline" className="w-full">
            Login with Google
          </Button>
        </CardFooter>
      </Card>
    );
}