import Label from "../form/Label";
import Input from "../form/input/InputField";
import type { FieldErrors, UseFormRegister, Path } from "react-hook-form";
import type { WarehouseFormData } from "./warehouseSchema";

export interface AddressFormProps {
  title?: string;
  register: UseFormRegister<WarehouseFormData>;
  errors: FieldErrors<WarehouseFormData>;
  fieldName: "address" | "returnAddress";
}

const WarehouseAddress = ({
  title,
  register,
  errors,
  fieldName,
}: AddressFormProps) => {
  const errorPath = errors[fieldName];

  return (
    <div className="mt-6">
      <h5 className="text-lg font-medium mb-3">{title}</h5>
      <div className="grid grid-cols-1 gap-5 lg:grid-cols-2">
        <div>
          <Label>Address</Label>
          <Input
            type="text"
            placeholder="Street Address"
            register={register(
              `${fieldName}.address` as Path<WarehouseFormData>
            )}
            error={errorPath?.address?.message}
          />
        </div>
        <div>
          <Label>Address 2</Label>
          <Input
            type="text"
            placeholder="Address line 2"
            register={register(
              `${fieldName}.address2` as Path<WarehouseFormData>
            )}
            error={errorPath?.address2?.message}
          />
        </div>
        <div>
          <Label>City</Label>
          <Input
            type="text"
            placeholder="City"
            register={register(`${fieldName}.city` as Path<WarehouseFormData>)}
            error={errorPath?.city?.message}
          />
        </div>
        <div>
          <Label>State</Label>
          <Input
            type="text"
            placeholder="State"
            register={register(`${fieldName}.state` as Path<WarehouseFormData>)}
            error={errorPath?.state?.message}
          />
        </div>
        <div>
          <Label>ZIP</Label>
          <Input
            type="text"
            placeholder="ZIP Code"
            register={register(`${fieldName}.zip` as Path<WarehouseFormData>)}
            error={errorPath?.zip?.message}
          />
        </div>
        <div>
          <Label>Country</Label>
          <Input
            type="text"
            placeholder="Country"
            register={register(
              `${fieldName}.country` as Path<WarehouseFormData>
            )}
            error={errorPath?.country?.message}
          />
        </div>
        <div className="col-span-2">
          <Label>Landmark</Label>
          <Input
            type="text"
            placeholder="Landmark"
            register={register(
              `${fieldName}.landmark` as Path<WarehouseFormData>
            )}
            error={errorPath?.landmark?.message}
          />
        </div>
      </div>
    </div>
  );
};

export default WarehouseAddress;
