export class UpdateUserDTO {
  constructor(
    public nickname: string,
    public email: string,
    public userImage: string,
    public firstName?: string,
    public lastName?: string,
    public phoneNumber?: string,
    public isMember?: boolean
  ) {}
}
