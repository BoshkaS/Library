export class UserDTO {
  public nickname: string;
  public email: string;
  public userImage: string;
  public token: string;
  public roles: string[]

  constructor(
    nickName: string,
    email: string,
    userImage: string,
    token: string,
    roles: string[]
  ) {
    this.nickname = nickName;
    this.email = email;
    this.userImage = userImage;
    this.token = token;
    this.roles = roles;
  }
}
