import { useEffect, useState } from "react";

export default function Preloader() {
  const [loaded, setLoaded] = useState(true);

  useEffect(() => {
    function onLoad() {
      // mimic Alpine: wait 500ms after DOMContentLoaded then hide
      setTimeout(() => setLoaded(false), 500);
    }

    if (document.readyState === "complete" || document.readyState === "interactive") {
      onLoad();
    } else {
      window.addEventListener("DOMContentLoaded", onLoad);
      return () => window.removeEventListener("DOMContentLoaded", onLoad);
    }
  }, []);

  if (!loaded) return null;

  return (
    <div className="fixed left-0 top-0 z-999999 flex h-screen w-screen items-center justify-center bg-white dark:bg-black">
      <div className="h-16 w-16 animate-spin rounded-full border-4 border-solid border-brand-500 border-t-transparent" />
    </div>
  );
}
