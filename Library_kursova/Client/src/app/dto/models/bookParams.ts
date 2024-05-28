export class BookParams {
    type: string;
    pageNumber = 1;
    pageSize = 8;

    constructor(type: string) {
        this.type = type;
    }
}