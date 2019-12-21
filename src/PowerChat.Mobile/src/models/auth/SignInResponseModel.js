export class SignInResponseModel {
  constructor(userId, token, expires) {
    this.userId = userId;
    this.token = token;
    this.expires = new Date(expires)
  }
};