import type React from "react";
import type { UseFormRegisterReturn } from "react-hook-form";
import clsx from "clsx";

interface CheckboxProps {
  label?: string;
  checked?: boolean;
  className?: string;
  id?: string;
  onChange?: (checked: boolean) => void;
  disabled?: boolean;
  success?: boolean;
  register?: UseFormRegisterReturn;
}

const Checkbox: React.FC<CheckboxProps> = ({
  label,
  checked,
  id,
  onChange,
  className = "",
  disabled = false,
  success = false,
  register,
}) => {
  const isControlled = checked !== undefined && onChange !== undefined;

  const hasError = !success && !disabled;

  const checkboxClasses = clsx(
    "w-5 h-5 appearance-none cursor-pointer border rounded-md checked:bg-brand-500 checked:border-transparent disabled:opacity-60",
    {
      "border-error-500 focus:border-error-500 focus:ring-error-500/20":
        hasError,
      "border-gray-300 focus:border-success-300 focus:ring-success-500/20":
        success && !hasError,
      "border-gray-300 dark:border-gray-700":
        !success && !hasError && !disabled,
      "opacity-60 cursor-not-allowed": disabled,
    },
    className
  );

  return (
    <label
      htmlFor={id}
      className={clsx("flex items-center space-x-3 cursor-pointer group", {
        "cursor-not-allowed opacity-60": disabled,
      })}
    >
      <div className="relative w-5 h-5">
        <input
          id={id}
          type="checkbox"
          className={checkboxClasses}
          disabled={disabled}
          {...(register
            ? register
            : isControlled
            ? { checked, onChange: (e) => onChange(e.target.checked) }
            : {})}
        />

        <svg
          className="absolute transform -translate-x-1/2 -translate-y-1/2 pointer-events-none top-1/2 left-1/2"
          xmlns="http://www.w3.org/2000/svg"
          width="14"
          height="14"
          viewBox="0 0 14 14"
          fill="none"
        >
          <path
            d="M11.6666 3.5L5.24992 9.91667L2.33325 7"
            stroke={disabled ? "#E4E7EC" : "white"}
            strokeWidth="1.94437"
            strokeLinecap="round"
            strokeLinejoin="round"
            className={clsx({ hidden: !checked })}
          />
        </svg>
      </div>

      {label && (
        <span className="text-sm font-medium text-gray-800 dark:text-gray-200">
          {label}
        </span>
      )}
    </label>
  );
};

export default Checkbox;
