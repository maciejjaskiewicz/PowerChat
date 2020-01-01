export class ChatModel {
  constructor(id, name, interlocutor, messages, lastActive, isOnline) {
    this.id = id;
    this.name = name;
    this.interlocutor = interlocutor;
    this.messages = messages;
    this.lastActive = lastActive;
    this.isOnline = isOnline;
  }
}