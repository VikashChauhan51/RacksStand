import React, { useState } from "react";
import type { FC } from "react";
import type { UseFormRegisterReturn } from "react-hook-form";
import clsx from "clsx";

interface CountryCode {
  code: string;
  label: string;
}

interface PhoneInputProps {
  countries: CountryCode[];
  placeholder?: string;
  selectPosition?: "start" | "end";
  register?: UseFormRegisterReturn;
  disabled?: boolean;
}

const PhoneInput: FC<PhoneInputProps> = ({
  countries,
  placeholder = "+1 (555) 000-0000",
  selectPosition = "start",
  register,
  disabled = false,
}) => {
  const [selectedCountry, setSelectedCountry] = useState(countries[0]?.code || "");

  const handleCountryChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const newCountry = e.target.value;
    setSelectedCountry(newCountry);
  };

  // Update input padding dynamically based on select position
  const inputPaddingClass =
    selectPosition === "start" ? "pl-[84px]" : "pr-[84px]";

  return (
    <div className="relative flex w-full">
      {/* Dropdown at Start */}
      {selectPosition === "start" && (
        <div className="absolute left-0">
          <select
            value={selectedCountry}
            onChange={handleCountryChange}
            disabled={disabled}
            className={clsx(
              "appearance-none rounded-l-lg border-0 border-r border-gray-200 bg-transparent py-3 pl-3.5 pr-8 text-gray-700 leading-tight focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-800 dark:text-gray-400",
              disabled && "cursor-not-allowed opacity-50"
            )}
          >
            {countries.map((country) => (
              <option key={country.code} value={country.code}>
                {country.code}
              </option>
            ))}
          </select>
          <div className="absolute inset-y-0 flex items-center pointer-events-none right-3 text-gray-700 dark:text-gray-400">
            <svg
              className="stroke-current"
              width="20"
              height="20"
              viewBox="0 0 20 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M4.79175 7.396L10.0001 12.6043L15.2084 7.396"
                stroke="currentColor"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </div>
        </div>
      )}

      {/* Input */}
      <input
        type="tel"
        placeholder={placeholder}
        disabled={disabled}
        className={clsx(
          "h-11 w-full rounded-lg border border-gray-300 bg-transparent py-3 px-4 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800",
          inputPaddingClass,
          disabled && "cursor-not-allowed opacity-50",
        )}
        {...register} // RHF register
      />

      {/* Dropdown at End */}
      {selectPosition === "end" && (
        <div className="absolute right-0">
          <select
            value={selectedCountry}
            onChange={handleCountryChange}
            disabled={disabled}
            className={clsx(
              "appearance-none rounded-r-lg border-0 border-l border-gray-200 bg-transparent py-3 pl-3.5 pr-8 text-gray-700 leading-tight focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-800 dark:text-gray-400",
              disabled && "cursor-not-allowed opacity-50"
            )}
          >
            {countries.map((country) => (
              <option key={country.code} value={country.code}>
                {country.code}
              </option>
            ))}
          </select>
          <div className="absolute inset-y-0 flex items-center pointer-events-none right-3 text-gray-700 dark:text-gray-400">
            <svg
              className="stroke-current"
              width="20"
              height="20"
              viewBox="0 0 20 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M4.79175 7.396L10.0001 12.6043L15.2084 7.396"
                stroke="currentColor"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </div>
        </div>
      )}
    </div>
  );
};

export default PhoneInput;
