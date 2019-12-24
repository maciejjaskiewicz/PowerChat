export class AuthData {
  constructor(userId, token, expires) {
    this.userId = userId;
    this.token = token;
    this.expires = new Date(expires)
  }

  valid = () => {
    if(!this.token || !this.userId || !this.expires || 
        this.expires <= Date.now()) {
      return false;
    }

    return true;
  }
};