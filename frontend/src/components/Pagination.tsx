interface Props {
  page: number;
  totalPages: number;
  onChange: (value: number) => void;
}

export default function PaginationComponent({
  page,
  totalPages,
  onChange,
}: Props) {
  return (
    <div
      className="d-flex justify-content-center align-items-center gap-4"
      style={{ marginTop: "20px" }}
    >
      {/* Prev */}
      <button
        className="btn btn-outline-primary"
        disabled={page <= 1}
        onClick={() => onChange(page - 1)}
      >
        Prev
      </button>

      {/* Page indicator */}
      <span>
        Page {page} / {totalPages}
      </span>

      {/* Next */}
      <button
        className="btn btn-outline-primary"
        disabled={page >= totalPages}
        onClick={() => onChange(page + 1)}
      >
        Next
      </button>
    </div>
  );
}
