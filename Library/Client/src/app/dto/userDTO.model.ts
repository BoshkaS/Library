export class UserDTO {
  public id: number;
  public nickname: string;
  public lastName: string;
  public firstName: string;
  public phoneNumber: string;
  public email: string;
  public userImage: string;
  public token: string;
  public roles: string[];
  public isMember: boolean;
  public rating: string;

  constructor(
    id: number,
    firstName: string,
    lastName: string,
    phoneNumber: string,
    nickName: string,
    email: string,
    userImage: string,
    token: string,
    roles: string[],
    isMember: boolean,
    rating: string
  ) {
    this.id = id;
    this.nickname = nickName;
    this.firstName = firstName;
    this.lastName = lastName;
    this.phoneNumber = phoneNumber;
    this.email = email;
    this.userImage = userImage;
    this.isMember = isMember;
    this.token = token;
    this.roles = roles;
    this.rating = rating;
  }
}
