export class ChatModel {
  constructor(id, name, interlocutor, messages) {
    this.id = id;
    this.name = name;
    this.interlocutor = interlocutor;
    this.messages = messages;
  }
}