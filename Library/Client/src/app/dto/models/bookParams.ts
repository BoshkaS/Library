export class BookParams {
  type: string;
  pageNumber = 1;
  pageSize = 10;

  constructor(type: string) {
    this.type = type;
  }
}
