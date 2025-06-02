export class BorrowRequestDTO {
  requestId: number;
  decision: string;

  constructor(requestId: number, decision: string) {
    this.requestId = requestId;
    this.decision = decision;
  }
}
