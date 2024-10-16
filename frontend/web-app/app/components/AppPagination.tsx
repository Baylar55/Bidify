"use client";
import { Pagination } from "flowbite-react";
import React, { useState } from "react";

type Props = {
  currentPage: number;
  pageCount: number;
  pageChanged: (pageNumber: number) => void;
};

export default function AppPagination({ currentPage, pageCount, pageChanged }: Props) {
  const [pageNumber, setPageNumber] = useState(currentPage);
  return (
    <Pagination
      currentPage={currentPage}
      totalPages={pageCount}
      onPageChange={(e) => setPageNumber(e)}
      layout="pagination"
      showIcons={true}
      className="text-blue-500 mb-5"
    />
  );
}
