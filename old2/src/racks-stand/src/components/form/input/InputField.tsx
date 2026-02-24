import type { FC } from "react";
import type { UseFormRegisterReturn } from "react-hook-form";

interface InputProps {
  type?: "text" | "number" | "email" | "password" | "date" | "time" | string;
  id?: string;
  placeholder?: string;
  disabled?: boolean;
  success?: boolean;
  error?: string;
  hint?: string;
  className?: string;
  register?: UseFormRegisterReturn;
}

const Input: FC<InputProps> = ({
  type = "text",
  id,
  placeholder,
  disabled = false,
  success = false,
  error,
  hint,
  className = "",
  register,
}) => {
  let inputClasses = `h-11 w-full rounded-lg border px-4 py-2.5 text-sm shadow-theme-xs placeholder:text-gray-400 focus:outline-hidden focus:ring-3 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 ${className}`;

  if (disabled) {
    inputClasses += ` text-gray-500 border-gray-300 opacity-40 bg-gray-100 cursor-not-allowed`;
  } else if (error) {
    inputClasses += ` border-error-500 focus:border-error-300 focus:ring-error-500/20`;
  } else if (success) {
    inputClasses += ` border-success-500 focus:border-success-300 focus:ring-success-500/20`;
  } else {
    inputClasses += ` border-gray-300 focus:border-brand-300 focus:ring-brand-500/20`;
  }

  return (
    <div className="relative">
      <input
        type={type}
        id={id}
        disabled={disabled}
        placeholder={placeholder}
        className={inputClasses}
        {...register} // ✅ Bind react-hook-form
      />

      {(error || hint) && (
        <p
          className={`mt-1.5 text-xs ${
            error
              ? "text-error-500"
              : success
              ? "text-success-500"
              : "text-gray-500"
          }`}
        >
          {error ?? hint}
        </p>
      )}
    </div>
  );
};

export default Input;
