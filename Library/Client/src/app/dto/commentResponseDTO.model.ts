export class CommentResponseDTO {
  public nickName: string;
  public text: string;
  public userImage: string;
  public createdDate: Date;
  public userId: number;

  constructor(
    nickName: string,
    text: string,
    userImage: string,
    createdDate: Date,
    userId: number
  ) {
    this.nickName = nickName;
    this.text = text;
    this.userImage = userImage;
    this.createdDate = createdDate;
    this.userId = userId;
  }
}
