"use client";
import { HeroSection } from "@/components/layout/herosection";
import Image from "next/image";


//flex flex-col flex-1 items-center justify-center bg-zinc-50 font-sans dark:bg-black
//*lex flex-1 w-full max-w-3xl flex-col items-center justify-between py-32 px-16 bg-white dark:bg-black sm:items-start
export default function Home() {
  return (
    <div className="bg-grey-900">
      <main className="bg-grey-900">
        <HeroSection />
      </main>
    </div>
  );
}
