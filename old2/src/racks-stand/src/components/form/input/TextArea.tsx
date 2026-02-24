import type { FC } from "react";
import type { UseFormRegisterReturn } from "react-hook-form";
import clsx from "clsx";

interface TextAreaProps {
  placeholder?: string;
  rows?: number;
  className?: string;
  disabled?: boolean;
  error?: string;
  hint?: string;
  register?: UseFormRegisterReturn;
}

const TextArea: FC<TextAreaProps> = ({
  placeholder = "Enter your message",
  rows = 3,
  className,
  disabled = false,
  error,
  hint,
  register,
}) => {
  const textareaClasses = clsx(
    "w-full rounded-lg border px-4 py-2.5 text-sm shadow-theme-xs focus:outline-hidden",
    {
      "bg-gray-100 opacity-50 text-gray-500 border-gray-300 cursor-not-allowed dark:bg-gray-800 dark:text-gray-400 dark:border-gray-700": disabled,
      "bg-transparent border-gray-300 focus:border-error-300 focus:ring-3 focus:ring-error-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-error-800": !disabled && error,
      "bg-transparent text-gray-900 dark:text-gray-300 border-gray-300 focus:border-brand-300 focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800": !disabled && !error,
    },
    className
  );

  return (
    <div className="relative">
      <textarea
        placeholder={placeholder}
        rows={rows}
        disabled={disabled}
        className={textareaClasses}
        {...register}
      />
      {(error || hint) && (
        <p
          className={clsx(
            "mt-2 text-sm",
            error ? "text-error-500" : "text-gray-500 dark:text-gray-400"
          )}
        >
          {error ?? hint}
        </p>
      )}
    </div>
  );
};

export default TextArea;
