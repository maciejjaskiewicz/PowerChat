import { 
  FETCH_CONVERSATIONS, 
  GET_USER_CONVERSATION,
  FETCH_CHAT,
  PRESEND_MESSAGE,
  SEND_MESSAGE,
  RECEIVE_MESSAGE
} from './../actions/chat'

import { ChatModel }  from './../../models/chat/ChatModel';
import { ConversationPreviewModel } from '../../models/chat/ConversationPreviewModel';

const initialState = {
  conversations: [],
  chats: []
}

const fetchConversationsHandler = (state = initialState, action) => {
  return {
    ...initialState,
    conversations: action.conversations
  };
}

const getUserConversationHandler = (state = initialState, action) => {
  const conversationExists = state.conversations.find(x => x.id === action.conversation.id);

  if(conversationExists) {
    return state;
  }

  return {
    ...state,
    conversations: [action.conversation, ...state.conversations]
  };
}

const fetchChatHandler = (state = initialState, action) => {
  return {
    ...state,
    chats: [...state.chats, action.chat]
  };
}

const presendMessageHandler = (state = initialState, action) => {
  return addMessage(state, action.channelId, action.message);
};

const sendMessageHandler = (state = initialState, action) => {
  const channelId = action.channelId;
  const presendMessageId = action.presendId;
  const existingChannelIdx = state.chats.findIndex(x => x.id === channelId);

  if(existingChannel === -1) {
    return state;
  }

  const existingChannel = state.chats[existingChannelIdx];
  const existingMessageIdx = existingChannel.messages.findIndex(x => x.id === presendMessageId);

  const updatedMessages = [...existingChannel.messages];
  updatedMessages[existingMessageIdx] = action.message;

  const updatedChannel = new ChatModel(
    existingChannel.id,
    existingChannel.name,
    existingChannel.interlocutor,
    updatedMessages
  );

  const updatedChats = [...state.chats];
  updatedChats[existingChannelIdx] = updatedChannel;

  return {
    ...state,
    chats: updatedChats,
    conversations: updateConversations(state, channelId, action.message)
  }
};

const receiveMessageHandler = (state = initialState, action) => {
  return addMessage(state, action.channelId, action.message);
}

const addMessage = (state, channelId, message) => {
  const existingChannelIdx = state.chats.findIndex(x => x.id === channelId);

  if(existingChannelIdx === -1) {
    return state;
  }

  const existingChannel = state.chats[existingChannelIdx];
  const updatedChannel = new ChatModel(
    existingChannel.id,
    existingChannel.name,
    existingChannel.interlocutor,
    [...existingChannel.messages, message]
  );

  const updatedChats = [...state.chats];
  updatedChats[existingChannelIdx] = updatedChannel;

  return {
    ...state,
    chats: updatedChats,
    conversations: updateConversations(state, channelId, message)
  }
}

const updateConversations = (state, channelId, message) => {
  const existingConversationIdx = state.conversations.findIndex(x => x.id == channelId);
  const existingConversation = state.conversations[existingConversationIdx];

  const updatedConversation = new ConversationPreviewModel(
    existingConversation.id,
    existingConversation.name,
    existingConversation.gender,
    message.content,
    message.sentDate,
    message.seen,
    existingConversation.createdDate
  );

  const updatedConversations = [...state.conversations];
  updatedConversations[existingConversationIdx] = updatedConversation;

  updatedConversations.sort((a, b) => {
    let x = 0;

    if(a.lastMessageDate && !b.lastMessageDate) 
      x = new Date(a.lastMessageDate) - new Date(b.createdDate);
    else if(!a.lastMessageDate && b.lastMessageDate)
      x = new Date(a.createdDate) - new Date(b.lastMessageDate);
    else if (a.lastMessageDate && b.lastMessageDate) {
      x = new Date(a.lastMessageDate) - new Date(b.lastMessageDate);
    }

    return x == 0 ? new Date(a.createdDate) - new Date(b.createdDate) : -x;
  });

  return updatedConversations;
}

export default (state = initialState, action) => {
  switch(action.type) {
    case FETCH_CONVERSATIONS: return fetchConversationsHandler(state, action);
    case GET_USER_CONVERSATION: return getUserConversationHandler(state, action);
    case FETCH_CHAT: return fetchChatHandler(state, action);
    case PRESEND_MESSAGE: return presendMessageHandler(state, action);
    case SEND_MESSAGE: return sendMessageHandler(state, action);
    case RECEIVE_MESSAGE: return receiveMessageHandler(state, action);
  }

  return state;
};