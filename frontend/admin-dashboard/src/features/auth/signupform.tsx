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

export default function SignUpForm() {
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
        <form method="POST">
          <div className="flex flex-col gap-6">
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="first_name">
                first name
              </Label>
              <Input
                className="text-lg font-semibold"
                id="first_name"
                type="text"
                placeholder="Mohammed"
              ></Input>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="second_name">
                second name
              </Label>
              <Input
                className="text-lg font-semibold"
                id="second_name"
                type="text"
                placeholder="Ibaraham"
              ></Input>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="username">
                username
              </Label>
              <Input
                className="text-lg font-semibold"
                id="username"
                type="text"
                placeholder="admin_123"
              ></Input>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="email">
                Email
              </Label>
              <Input
                className="text-lg font-semibold"
                id="email"
                type="email"
                placeholder="example@gmail.com "
              ></Input>
            </div>
            <div className="grid gap-2">
              <Label className="text-lg" htmlFor="password">
                Password
              </Label>
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
          Register
        </Button>
        <Button variant="outline" className="w-full">
          Login with Google
        </Button>
      </CardFooter>
    </Card>
  );
}
