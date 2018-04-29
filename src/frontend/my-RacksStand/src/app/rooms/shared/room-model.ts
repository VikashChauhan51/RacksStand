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
        public SecondaryStatus:number;
        public  CompanyId:string ; 
        public Index:number;
        public BoxCapacity:number;
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
        public Row:number;
        public Column:number;
        public CurrentSize:number; 
        public SecondaryStatus:number;
        public  CompanyId:string ; 
        public Index:number;
  }