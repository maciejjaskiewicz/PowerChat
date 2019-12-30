export class MessageModel {
  constructor(id, authorId, content, sentDate, seen, own) {
    this.id = id;
    this.authorId = authorId;
    this.content = content;
    this.sentDate = sentDate;
    this.seen = seen;
    this.own = own;
  }
}