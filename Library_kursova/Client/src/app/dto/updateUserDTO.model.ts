export class UpdateUserDTO {
    public nickname: string;
    public email: string;
    public userImage: string;

    constructor(nickName: string, email: string, userImage:string) {
        this.nickname = nickName;
        this.email = email;
        this.userImage = userImage;
    }
}