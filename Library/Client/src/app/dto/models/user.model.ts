export class User {
  public firstName: string;
  public lastName: string;
  public userName: string;
  public phoneNumber: string;
  public email: string;
  public userImage: string;
  public password: string;
  public isMember: boolean;

  constructor(
    firstName: string,
    lastName: string,
    userName: string,
    phoneNumber: string,
    email: string,
    userImage: string,
    password: string,
    isMember: boolean
  ) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.userName = userName;
    this.phoneNumber = phoneNumber;
    this.email = email;
    this.userImage = userImage;
    this.password = password;
    this.isMember = isMember;
  }
}
