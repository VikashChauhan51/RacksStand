import Label from "../../../components/form/Label";
import Input from "../../../components/form/input/InputField";
import Select from "../../../components/form/Select";
import { EnvelopeIcon, CopyIcon } from "../../../icons";
import PhoneInput from "../../../components/form/group-input/PhoneInput";
import Form from "../../../components/form/Form";
import Button from "../../../components/ui/button/Button";
import { WarehouseSchema } from "./warehouseSchema";
import type { WarehouseFormData } from "./warehouseSchema";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Modal } from "../../../components/ui/modal";
import type { Option } from "../../../components/form/Select";
import TextArea from "../../../components/form/input/TextArea";
import { InfoIcon } from "../../../icons";
import Popover from "../../../components/ui/popover/Popover";
import { useEffect } from "react";
import type { WarehouseList } from "./warehouseSchema";
import WarehouseAddress from "./WarehouseAddress";
import Spinner from "../../../components/ui/spinner/Spinner";
export interface WarehouseModalProps {
  isOpen: boolean;
  id?: string;
  closeModal: () => void;
  onSave?: (warehouse: WarehouseList) => void;
}
export default function WarehouseModal(warehouseProps: WarehouseModalProps) {
  console.log("Warehouse component rendered with id:", warehouseProps.id);
  const isEditMode = !!warehouseProps.id;

  const popupTitle = isEditMode
    ? "Edit Warehouse Information"
    : "Add Warehouse Information";

  const popupDescription = isEditMode
    ? "Edit Warehouse details to keep it up to date."
    : "Add Warehouse details.";

  const countries = [
    { code: "US", label: "+1" },
    { code: "GB", label: "+44" },
    { code: "CA", label: "+1" },
    { code: "AU", label: "+61" },
  ];

  const diminssionsUnits: Option[] = [
    { value: "cm", label: "Centimeters (cm)" },
    { value: "m", label: "Meters (m)" },
    { value: "in", label: "Inches (in)" },
    { value: "ft", label: "Feet (ft)" },
  ];

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
    watch,
  } = useForm<WarehouseFormData>({
    resolver: zodResolver(WarehouseSchema),
  });

  const onSubmit = async (data: WarehouseFormData) => {
    console.log("Add Warehouse:", data);

    const savedData: WarehouseList = {
      id: warehouseProps.id || Math.random().toString(36).substring(2, 9),
      name: data.name,
      prifix: data.prifix,
      email: data.email,
      phone: data.phone,
      manager: data.manager,
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString(),
    };
    warehouseProps.onSave?.(savedData);
    warehouseProps.closeModal();
  };

  const replicateAddress = () => {
    const address = watch("address");
    if (address) {
      reset((prev) => ({ ...prev, returnAddress: { ...address } }));
    }
  };

  useEffect(() => {
    console.log("isEditMode:", isEditMode);
    console.log("warehouseProps.id:", warehouseProps.id);
    if (isEditMode && warehouseProps.id) {
      (async () => {
        try {
          const data = await fetch(
            `https://api.example.com/warehouses/${warehouseProps.id}`
          ).then((res) => res.json());
          // Assuming the API returns data in the correct format
          reset(data); // Populate form with fetched data
        } catch (error) {
          console.error("Error loading warehouse:", error);
        }
      })();
    } else {
      reset({
        name: undefined,
        prifix: undefined,
        email: undefined,
        phone: undefined,
        manager: undefined,
        length: 0,
        width: 0,
        height: 0,
        diminssionsUnit: undefined,
        description: undefined,
        address: undefined,
        returnAddress: undefined,
      }); // Clear form for adding new warehouse
    }
  }, [isEditMode, warehouseProps.id, reset]);

  return (
    <Modal
      isOpen={warehouseProps.isOpen}
      showCloseButton={!isSubmitting}
      onClose={warehouseProps.closeModal}
      className="max-w-[700px] m-4"
    >
      <div className="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
        <div className="px-2 pr-14">
          <h4 className="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
            {popupTitle}
          </h4>
          <p className="mb-6 text-sm text-gray-500 dark:text-gray-400 lg:mb-7">
            {popupDescription}
          </p>
        </div>
        <Form className="flex flex-col" onSubmit={handleSubmit(onSubmit)}>
          <div className="custom-scrollbar h-[450px] overflow-y-auto px-2 pb-3">
            <div>
              <h5 className="mb-5 text-lg font-medium text-gray-800 dark:text-white/90 lg:mb-6">
                General Information
              </h5>

              <div className="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
                <div>
                  <Label>
                    Prifix{" "}
                    <Popover
                      trigger={<InfoIcon className="w-4 h-4" />}
                      position="right"
                    >
                      <div className="text-sm">
                        <p className="font-medium text-gray-800 dark:text-white">
                          Warehouse Info
                        </p>
                        <p className="text-gray-500 dark:text-gray-400 text-xs mt-1">
                          The prifix should be three characters long. For
                          example,
                          <strong>MK1"</strong>" is a valid prifix for{" "}
                          <strong>"Mk House"</strong>.
                        </p>
                      </div>
                    </Popover>
                  </Label>
                  <Input
                    type="text"
                    placeholder="mk1"
                    register={register("prifix")}
                    error={errors.prifix?.message}
                  />
                </div>

                <div>
                  <Label>Name</Label>
                  <Input
                    type="text"
                    placeholder="Mk House"
                    register={register("name")}
                    error={errors.name?.message}
                  />
                </div>

                <div>
                  <Label>Email</Label>
                  <div className="relative">
                    <Input
                      type="text"
                      className="pl-[62px]"
                      placeholder="info@gmail.com"
                      register={register("email")}
                      error={errors.email?.message}
                    />
                    <span className="absolute left-0 top-1/2 -translate-y-1/2 border-r border-gray-200 px-3.5 py-3 text-gray-500 dark:border-gray-800 dark:text-gray-400">
                      <EnvelopeIcon className="size-6" />
                    </span>
                  </div>
                </div>

                <div>
                  <Label>Phone</Label>
                  <div className="relative">
                    <PhoneInput
                      selectPosition="start"
                      countries={countries}
                      placeholder="+1 (555) 000-0000"
                      register={register("phone")}
                      error={errors.phone?.message}
                    />
                  </div>
                </div>
              </div>
            </div>
            <div className="mt-7">
              <h5 className="mb-5 text-lg font-medium text-gray-800 dark:text-white/90 lg:mb-6">
                Diminssion Information
              </h5>

              <div className="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
                <div className="col-span-2 lg:col-span-1">
                  <Label>Length</Label>
                  <Input
                    type="number"
                    placeholder="Length of Warehouse"
                    register={register("length", { valueAsNumber: true })}
                    error={errors.length?.message}
                  />
                </div>

                <div className="col-span-2 lg:col-span-1">
                  <Label>Width</Label>
                  <Input
                    type="number"
                    placeholder="Width of Warehouse"
                    register={register("width", { valueAsNumber: true })}
                    error={errors.width?.message}
                  />
                </div>

                <div className="col-span-2 lg:col-span-1">
                  <Label>Height</Label>
                  <Input
                    type="number"
                    placeholder="Height of Warehouse"
                    register={register("height", { valueAsNumber: true })}
                    error={errors.height?.message}
                  />
                </div>

                <div className="col-span-2 lg:col-span-1">
                  <Label>Diminssions Unit</Label>
                  <Select
                    options={diminssionsUnits}
                    placeholder="Select dimensions unit"
                    register={register("diminssionsUnit")}
                    error={errors.diminssionsUnit?.message}
                  />
                </div>

                <div className="col-span-2">
                  <Label>Description</Label>
                  <TextArea
                    register={register("description")}
                    error={errors.description?.message}
                  />
                </div>
              </div>
            </div>
            {/* Address */}
            <WarehouseAddress
              title="Warehouse Address"
              register={register}
              errors={errors}
              fieldName="address"
            />
            <div className="col-span-2 flex items-center gap-2 mt-6 lg:justify-end">
              <Label className="mb-0">
                Return address is same as warehouse address
              </Label>
              <button
                type="button"
                onClick={replicateAddress}
                className="p-1 rounded hover:bg-gray-100 dark:hover:bg-gray-800"
              >
                <CopyIcon className="w-5 h-5 text-gray-500 hover:text-gray-800 dark:text-gray-400 dark:hover:text-white/90" />
              </button>
            </div>

            {/* Return Address */}
            <WarehouseAddress
              title="Return Address"
              register={register}
              errors={errors}
              fieldName="returnAddress"
            />
          </div>
          <div className="flex items-center gap-3 px-2 mt-6 lg:justify-end">
            <Button
              size="sm"
              variant="outline"
              disabled={isSubmitting}
              onClick={warehouseProps.closeModal}
            >
              Close
            </Button>
            <Button size="sm" type="submit" disabled={isSubmitting}>
              {isSubmitting ? (
                <div className="flex items-center">
                  <Spinner size={16} className="mr-2" /> Saving...
                </div>
              ) : (
                "Save Changes"
              )}
            </Button>
          </div>
        </Form>
      </div>
    </Modal>
  );
}
