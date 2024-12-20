"use client";

import { useParamsStore } from "@/hooks/useParamsStore";
import { usePathname, useRouter } from "next/navigation";
import React from "react";
import { AiOutlineCar } from "react-icons/ai";

export default function Logo() {
  const router = useRouter();
  const pathName = usePathname();

  function doReset() {
    if (pathName !== "/") router.push("/");
    reset();
  }

  const reset = useParamsStore((state) => state.reset);

  return (
    <div
      onClick={doReset}
      className="cursor-pointer flex items-center gap-2 text-3xl font-semibold text-red-500"
    >
      <AiOutlineCar size={34} />
      <div>Bidify Auctions</div>
    </div>
  );
}
