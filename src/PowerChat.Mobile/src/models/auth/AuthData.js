export class AuthData {
  constructor(userId, token, expires) {
    this.userId = userId;
    this.token = token;

    let expiresDate = new Date();
    expiresDate.setSeconds(expiresDate.getSeconds() + expires);
    this.expires = expiresDate
  }

  valid = () => {
    if(!this.token || !this.userId || !this.expires || 
        this.expires <= Date.now()) {
      return false;
    }

    return true;
  }
};