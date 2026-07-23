import SignUpForm from "@/features/auth/signupform";

export default function SignUp (){
    return (
      <div className="bg">
        <div className="flex min-h-screen items-center justify-center shadow-lg">
          <SignUpForm></SignUpForm>
        </div>
      </div>
    );
}