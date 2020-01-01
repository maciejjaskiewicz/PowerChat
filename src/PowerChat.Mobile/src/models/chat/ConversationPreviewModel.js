export class ConversationPreviewModel {
  constructor(id, name, gender, lastMessage, lastMessageDate, seen, own, createdDate, isOnline) {
    this.id = id;
    this.name = name;
    this.gender = gender;
    this.lastMessage = lastMessage;
    this.lastMessageDate = lastMessageDate;
    this.seen = seen;
    this.own = own;
    this.createdDate = createdDate;
    this.isOnline = isOnline;
  }
}