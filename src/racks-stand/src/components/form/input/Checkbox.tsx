import type React from "react";
import type { UseFormRegisterReturn } from "react-hook-form";

interface CheckboxProps {
  label?: string;
  checked?: boolean;
  className?: string;
  id?: string;
  onChange?: (checked: boolean) => void;
  disabled?: boolean;
  register?: UseFormRegisterReturn; // ✅ react-hook-form register support
}

const Checkbox: React.FC<CheckboxProps> = ({
  label,
  checked,
  id,
  onChange,
  className = "",
  disabled = false,
  register,
}) => {
  const isControlled = checked !== undefined && onChange !== undefined;

  return (
    <label
      htmlFor={id}
      className={`flex items-center space-x-3 cursor-pointer group ${
        disabled ? "cursor-not-allowed opacity-60" : ""
      }`}
    >
      <div className="relative w-5 h-5">
        <input
          id={id}
          type="checkbox"
          className={`w-5 h-5 appearance-none cursor-pointer border border-gray-300 dark:border-gray-700 rounded-md checked:bg-brand-500 checked:border-transparent disabled:opacity-60 ${className}`}
          disabled={disabled}
          {...(isControlled
            ? {
                checked,
                onChange: (e: React.ChangeEvent<HTMLInputElement>) =>
                  onChange?.(e.target.checked),
              }
            : register)}
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
            className={checked ? "block" : "hidden"}
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
