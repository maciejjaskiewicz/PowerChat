export default class PowerChatError extends Error {
  constructor(title, message) {
    super(message);
    this.title = title;
  }
};