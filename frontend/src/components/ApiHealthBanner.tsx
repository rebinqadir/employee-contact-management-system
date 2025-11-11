interface Props {
  isApiUp: boolean;
}

export default function ApiHealthBanner({ isApiUp }: Props) {
  if (isApiUp) return null;

  return (
    <div className="bg-danger text-white text-center py-2 fw-bold">
      ⚠️ API is not available
    </div>
  );
}
