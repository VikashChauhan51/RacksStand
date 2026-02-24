import type { FC } from "react";
import type { UseFormRegisterReturn } from "react-hook-form";
import clsx from "clsx";

interface RadioSmProps {
  id: string;
  name: string;
  value: string;
  label: string;
  disabled?: boolean;
  className?: string;
  register?: UseFormRegisterReturn;
}

const RadioSm: FC<RadioSmProps> = ({ id, name, value, label, disabled = false, className = "", register }) => {
  return (
    <label
      htmlFor={id}
      className={clsx(
        "flex cursor-pointer select-none items-center text-sm text-gray-500 dark:text-gray-400",
        disabled && "cursor-not-allowed opacity-50",
        className
      )}
    >
      <span className="relative">
        {/* Hidden Input */}
        <input
          type="radio"
          id={id}
          name={name}
          value={value}
          disabled={disabled}
          className="sr-only"
          {...register}
        />

        {/* Styled Radio Circle */}
        <span
          className={clsx(
            "mr-2 flex h-4 w-4 items-center justify-center rounded-full border",
            "bg-transparent border-gray-300 dark:border-gray-700",
            disabled && "bg-gray-100 dark:bg-gray-700 border-gray-200 dark:border-gray-700"
          )}
        >
          {/* Inner Dot */}
          <span
            className="h-1.5 w-1.5 rounded-full bg-white"
          ></span>
        </span>
      </span>
      {label}
    </label>
  );
};

export default RadioSm;
