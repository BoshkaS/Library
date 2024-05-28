export class CommentResponseDTO {
    public nickName: string;
    public text: string;
    public userImage: string;
    public createdDate: Date;
  
    constructor(nickName: string, text: string, userImage: string, createdDate: Date) {
      this.nickName = nickName;
      this.text = text;
      this.userImage = userImage;
      this.createdDate = createdDate;
    }
  }