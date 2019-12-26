export class UserProfileModel {
  constructor(id, name, gender, about, avatarUrl, isFriend, friends) {
    this.id = id;
    this.name = name;
    this.gender = gender;
    this.about = about;
    this.avatarUrl = avatarUrl;
    this.isFriend = isFriend;
    this.friends = friends;
  }

  static empty = () => {
    return new UserProfileModel(0, '', '', '', '', false, 0);
  }
}