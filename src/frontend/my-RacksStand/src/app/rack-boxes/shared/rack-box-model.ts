
export class RackBoxModel {
  public Id: string;
  public Name: string;
  public Description: string;
  public RackId: string;
  public Row: number;
  public Column: number;
  public SecondaryStatus: number;
  public CompanyId: string;
  public Index: number;
  public CurrentSize: number;
}
export class InventoryModel {
  public Id: string;
  public Name: string;
  public Description: string;
  public AccessCode: string;
  public Status: number;
  public CompanyId: string;
  public CustomerId: string;
  public RackBoxId: string;
  public Index: number;
  public CreatedOn: Date;
  public CreatedBy: string;
  public UpdatedOn: Date;
  public UpdatedBy: string;
}