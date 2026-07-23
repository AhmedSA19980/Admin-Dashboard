import LoginForm from "@/features/auth/loginform";

export default function Login (){
    return (
      <div className="bg">
        <div className="flex min-h-screen items-center justify-center">
          <LoginForm></LoginForm>
        </div>
      </div>
    );
}