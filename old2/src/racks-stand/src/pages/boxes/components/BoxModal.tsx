import { useEffect } from "react";
import Label from "../../../components/form/Label";
import Input from "../../../components/form/input/InputField";
import Select from "../../../components/form/Select";
import Form from "../../../components/form/Form";
import Button from "../../../components/ui/button/Button";
import { BoxSchema } from "./boxSchema";
import type { BoxFormData, BoxList } from "./boxSchema";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import type { Option } from "../../../components/form/Select";
import TextArea from "../../../components/form/input/TextArea";
import { Modal } from "../../../components/ui/modal";
import { InfoIcon } from "../../../icons";
import Spinner from "../../../components/ui/spinner/Spinner";
import Popover from "../../../components/ui/popover/Popover";

export interface BoxModelProps {
  isOpen: boolean;
  id?: string;
  closeModal: () => void;
  onSave?: (box: BoxList) => void;
}

export default function BoxModel({
  isOpen,
  id,
  closeModal,
  onSave,
}: BoxModelProps) {
  const isEditMode = !!id;

  const popupTitle = isEditMode
    ? "Edit Box Information"
    : "Add Box Information";

  const popupDescription = isEditMode
    ? "Edit box details to keep it up to date."
    : "Add new box details.";

  const dimensionsUnits: Option[] = [
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
  } = useForm<BoxFormData>({
    resolver: zodResolver(BoxSchema),
  });

  const onSubmit = async (data: BoxFormData) => {
    console.log("Saving box:", data);

    const savedData: BoxList = {
      id: id || Math.random().toString(36).substring(2, 9),
      name: data.name,
      code: data.code,
      selfId: "self-123", // TODO: Replace with actual context value
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString(),
    };

    onSave?.(savedData);
    closeModal();
  };

  useEffect(() => {
    if (isEditMode && id) {
      (async () => {
        try {
          const data = await fetch(`https://api.example.com/boxes/${id}`).then(
            (res) => res.json()
          );
          reset(data);
        } catch (error) {
          console.error("Error loading box:", error);
        }
      })();
    } else {
      reset({
        name: undefined,
        code: undefined,
        length: 0,
        width: 0,
        height: 0,
        diminssionsUnit: undefined,
        description: undefined,
      });
    }
  }, [isEditMode, id, reset]);

  return (
    <Modal
      isOpen={isOpen}
      showCloseButton={!isSubmitting}
      onClose={closeModal}
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
                    Code{" "}
                    <Popover
                      trigger={<InfoIcon className="w-4 h-4" />}
                      position="right"
                    >
                      <div className="text-sm">
                        <p className="font-medium text-gray-800 dark:text-white">
                          Box Info
                        </p>
                        <p className="text-gray-500 dark:text-gray-400 text-xs mt-1">
                          The code should be three characters long. For example,
                          <strong>"MK1"</strong> is a valid prefix for{" "}
                          <strong>"Mk House"</strong>.
                        </p>
                      </div>
                    </Popover>
                  </Label>
                  <Input
                    type="text"
                    placeholder="mk1"
                    register={register("code")}
                    error={errors.code?.message}
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
                  <Label>Label</Label>
                  <Input
                    type="text"
                    placeholder="Label"
                    register={register("label")}
                    error={errors.label?.message}
                  />
                </div>

                <div>
                  <Label>Level</Label>
                  <Input
                    type="text"
                    placeholder="Level"
                    register={register("level")}
                    error={errors.level?.message}
                  />
                </div>
              </div>
            </div>

            <div className="mt-7">
              <h5 className="mb-5 text-lg font-medium text-gray-800 dark:text-white/90 lg:mb-6">
                Dimension Information
              </h5>

              <div className="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
                <div>
                  <Label>Length</Label>
                  <Input
                    type="number"
                    placeholder="Length of box"
                    register={register("length", { valueAsNumber: true })}
                    error={errors.length?.message}
                  />
                </div>

                <div>
                  <Label>Width</Label>
                  <Input
                    type="number"
                    placeholder="Width of box"
                    register={register("width", { valueAsNumber: true })}
                    error={errors.width?.message}
                  />
                </div>

                <div>
                  <Label>Height</Label>
                  <Input
                    type="number"
                    placeholder="Height of box"
                    register={register("height", { valueAsNumber: true })}
                    error={errors.height?.message}
                  />
                </div>

                <div>
                  <Label>Dimensions Unit</Label>
                  <Select
                    options={dimensionsUnits}
                    placeholder="Select unit"
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
          </div>

          <div className="flex items-center gap-3 px-2 mt-6 lg:justify-end">
            <Button
              size="sm"
              variant="outline"
              disabled={isSubmitting}
              onClick={closeModal}
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
