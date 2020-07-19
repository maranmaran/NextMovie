
export class PagedResult<T> {
    page: number;
    totalResults: number;
    totalPages: number;
    results: T[];
}