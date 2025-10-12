import { useState, useRef, useEffect, type ReactNode } from "react";

interface PopoverProps {
  trigger: ReactNode;             // Button or element that opens the popover
  children: ReactNode;            // Content inside the popover
  position?: "top" | "bottom" | "left" | "right"; // Popover placement
  className?: string;             // Optional custom classes
}

const Popover: React.FC<PopoverProps> = ({
  trigger,
  children,
  position = "bottom",
  className = "",
}) => {
  const [open, setOpen] = useState(false);
  const popoverRef = useRef<HTMLDivElement>(null);

  // Close popover on outside click
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        popoverRef.current &&
        !popoverRef.current.contains(event.target as Node)
      ) {
        setOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  // Position styles
  const positionClasses = {
    top: "bottom-full left-1/2 -translate-x-1/2 mb-2",
    bottom: "top-full left-1/2 -translate-x-1/2 mt-2",
    left: "right-full top-1/2 -translate-y-1/2 mr-2",
    right: "left-full top-1/2 -translate-y-1/2 ml-2",
  }[position];

  return (
    <div className="relative inline-block" ref={popoverRef}>
      {/* Trigger */}
      <div onClick={() => setOpen(!open)} className="cursor-pointer select-none">
        {trigger}
      </div>

      {/* Popover content */}
      {open && (
        <div
          className={`absolute z-50 min-w-[10rem] rounded-lg border border-gray-200 bg-white p-3 shadow-theme-xs dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 ${positionClasses} ${className}`}
        >
          {children}
        </div>
      )}
    </div>
  );
};

export default Popover;
