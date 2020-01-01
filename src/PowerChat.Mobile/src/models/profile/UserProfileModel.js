export class UserProfileModel {
  constructor(id, name, gender, about, avatarUrl, isFriend, friends, lastActive, isOnline) {
    this.id = id;
    this.name = name;
    this.gender = gender;
    this.about = about;
    this.avatarUrl = avatarUrl;
    this.isFriend = isFriend;
    this.friends = friends;
    this.lastActive = lastActive;
    this.isOnline = isOnline;
  }

  static empty = () => {
    return new UserProfileModel(0, '', '', '', '', false, 0, null, false);
  }
}