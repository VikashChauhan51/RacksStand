export class StoreModel {
        public  Id :string ;
        public  Name :string ;
        public  Description:string ;
        public  CompanyId:string ; 
        public City: string;
        public  Country: number;
        public Email: string;
        public  Fax: string;
        public  Phone: string;
        public  State: string;
        public  Street: string;
        public   ZipCode: string;
        public  Status: number;
        public  CreatedOn: Date;
        public  CreatedBy: string;
        public UpdatedOn: Date;
        public SecondaryStatus:number;
        public Index:number;
        public UpdatedBy: string;
        public Rooms:RoomModel[];
}
export class RoomModel {
        public  Id :string ;
        public  Name :string ;
        public  Description:string ;
        public  StoreId:string ; 
        public  Status: number;
        public SecondaryStatus:number;
        public  CompanyId:string ; 
        public Index:number;
        public  CreatedOn: Date;
        public  CreatedBy: string;
        public UpdatedOn: Date;
        public UpdatedBy: string;
        public Racks:RackModel[];
}
export class RackModel {
        public  Id :string ;
        public  Name :string ;
        public  Description:string ;
        public  RoomId:string ; 
        public Rows:number;
        public Columns:number;
        public  Status: number;
        public BoxCapacity:number;
        public SecondaryStatus:number;
        public  CompanyId:string ; 
        public Index:number;
        public  CreatedOn: Date;
        public  CreatedBy: string;
        public UpdatedOn: Date;
        public UpdatedBy: string;
        public Boxes:RackBoxModel[];
}
export class RackBoxModel {
        public  Id :string ;
        public  Name :string ;
        public  Description:string ;
        public  RackId:string ;
        public CurrentSize:number; 
        public SecondaryStatus:number;
        public  CompanyId:string ; 
        public Index:number;
        public Row:number;
        public Column:number;
  }