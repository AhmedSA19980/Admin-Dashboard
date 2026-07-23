import Link from "next/link";


export function Navbar(){

    return (
      <header className="h-16 border-b bg-white shadow-sm">
        <div className="mx-auto flex h-full max-w-7xl items-center justify-between px-6">
          <Link href="/" className="text-xl font-bold text-slate-800">
            Admin Dashboard
          </Link>

          <nav className="flex items-center gap-6">
            <Link
              href="/dashboard"
              className="text-sm font-medium text-slate-600 hover:text-blue-600"
            >
              Dashboard
            </Link>

            <Link
              href="/users"
              className="text-sm font-medium text-slate-600 hover:text-blue-600"
            >
              Users
            </Link>

            <Link
              href="/settings"
              className="text-sm font-medium text-slate-600 hover:text-blue-600"
            >
              Settings
            </Link>
          </nav>
        </div>
      </header>
    );
}