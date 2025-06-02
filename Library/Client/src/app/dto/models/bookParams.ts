export class BookParams {
  type: string;
  pageNumber = 1;
  pageSize = 12;

  constructor(type: string) {
    this.type = type;
  }
}
