export class ConversationPreviewModel {
  constructor(id, name, gender, lastMessage, lastMessageDate, seen, createdDate) {
    this.id = id;
    this.name = name;
    this.gender = gender;
    this.lastMessage = lastMessage;
    this.lastMessageDate = lastMessageDate;
    this.seen = seen;
    this.createdDate = createdDate;
  }
}