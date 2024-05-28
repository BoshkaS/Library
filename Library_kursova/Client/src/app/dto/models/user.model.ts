export class User {
    public userName: string;
    public email: string;
    public userImage: string;
    public password: string

    constructor(userName: string, email: string, userImage:string, password: string) {
        this.userName = userName;
        this.email = email;
        this.userImage = userImage;
        this.password = password;
    }
}