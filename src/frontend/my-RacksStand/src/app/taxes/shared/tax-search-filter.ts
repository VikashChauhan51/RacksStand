import { Filter } from '../../core/filter';
export interface TaxSearchFilter extends Filter {
    Active?: boolean;
    IsCompound?:boolean;
}