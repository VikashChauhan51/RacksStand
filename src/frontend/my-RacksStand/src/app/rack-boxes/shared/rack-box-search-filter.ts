import { Filter } from '../../core/filter';
export interface RackBoxSearchFilter extends Filter {
    
RackId:string;
}
export interface InventorySearchFilter extends Filter {
    
RackBoxId:string;
CustomerId:string;
}