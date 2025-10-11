import type { FC } from "react";
import type { UseFormRegisterReturn } from "react-hook-form";
import clsx from "clsx";

interface RadioProps {
  id: string;
  name: string;
  value: string;
  label: string;
  disabled?: boolean;
  className?: string;
  register?: UseFormRegisterReturn;
}

const Radio: FC<RadioProps> = ({ id, name, value, label, disabled = false, className = "", register }) => {
  return (
    <label
      htmlFor={id}
      className={clsx(
        "relative flex cursor-pointer select-none items-center gap-3 text-sm font-medium",
        disabled ? "text-gray-300 dark:text-gray-600 cursor-not-allowed" : "text-gray-700 dark:text-gray-400",
        className
      )}
    >
      <input
        id={id}
        name={name}
        type="radio"
        value={value}
        disabled={disabled}
        className="sr-only"
        {...register}
      />

      <span
        className={clsx(
          "flex h-5 w-5 items-center justify-center rounded-full border-[1.25px]",
          "bg-transparent border-gray-300 dark:border-gray-700",
          disabled && "bg-gray-100 dark:bg-gray-700 border-gray-200 dark:border-gray-700"
        )}
      >
        <span
          className="h-2 w-2 rounded-full bg-white"
        ></span>
      </span>

      {label}
    </label>
  );
};

export default Radio;
