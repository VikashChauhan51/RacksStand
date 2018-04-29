export class SupplierModel {
    Id: string;
    CompanyId: string;
    Title: string;
    FirstName: string;
    MiddleName: string;
    LastName: string;
    Email: string;
    Company: string;
    Phone: string;
    Mobile: string;
    Fax: string;
    PanNo: string;
    Website: string;
    Bio: string;
    Status: number;
    CreatedOn: Date;
    CreatedBy: string;
    UpdatedOn: Date;
    UpdatedBy: string;
    Addresses: SupplierAddressModel[];
}
export class SupplierAddressModel {
    Id: string;
    City: string;
    Country: number;
    SupplierId: string;
    Email: string;
    Fax: string;
    Phone: string;
    Remark: string;
    State: string;
    Status: number;
    Street: string;
    ZipCode: string;

}