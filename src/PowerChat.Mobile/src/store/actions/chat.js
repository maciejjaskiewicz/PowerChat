import Api from './../../constants/Api';
import { handleUnauthorized, authorized } from './../../utils/auth';
import { toLocal } from './../../utils/date';
import PowerChatError from './../../models/PowerChatError';
import { ConversationPreviewModel } from './../../models/chat/ConversationPreviewModel';
import { ChatModel } from './../../models/chat/ChatModel';

export const FETCH_CONVERSATIONS = 'FETCH_CONVERSATIONS'
export const GET_USER_CONVERSATION = 'GET_USER_CONVERSATION';
export const FETCH_CHAT = 'FETCH_CHAT';
export const PRESEND_MESSAGE = 'PRESEND_MESSAGE';
export const SEND_MESSAGE = 'SEND_MESSAGE';
export const RECEIVE_MESSAGE = 'RECEIVE_MESSAGE';

export const fetchConversations = () => {
  return async (dispatch, getState) => {
    const state = getState();
    if(!authorized(state.auth)) {
      return dispatch(handleUnauthorized());
    }

    const response = await fetch(`${Api.url}/chat/channels`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      }
    });

    if(!response.ok) {
      if(response.status === 401) {
        return dispatch(handleUnauthorized());
      }
      
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';
      throw new PowerChatError(title, message);
    }

    const conversations = []
    const resData = await response.json();

    resData.forEach(channel => {
      var conversationModel = new ConversationPreviewModel(
        channel.id,
        channel.name,
        channel.gender,
        channel.lastMessage,
        toLocal(channel.lastMessageDate),
        channel.seen,
        channel.own,
        toLocal(channel.createdDate),
        channel.isOnline
      );

      conversations.push(conversationModel);
    });

    dispatch({ type: FETCH_CONVERSATIONS, conversations: conversations });
  };
}

export const getUserConversation = (userId) => {
  return async (dispatch, getState) => {
    const state = getState();
    if(!authorized(state.auth)) {
      return dispatch(handleUnauthorized());
    }

    const response = await fetch(`${Api.url}/chat/channels/user/${userId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      }
    });

    if(!response.ok) {
      if(response.status === 401) {
        return dispatch(handleUnauthorized());
      }
      
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';
      throw new PowerChatError(title, message);
    }

    const resData = await response.json();
    var conversationModel = new ConversationPreviewModel(
      resData.id,
      resData.name,
      resData.gender,
      resData.lastMessage,
      toLocal(resData.lastMessageDate),
      resData.seen,
      resData.own,
      toLocal(resData.createdDate),
      resData.isOnline
    );

    dispatch({ type: GET_USER_CONVERSATION, conversation: conversationModel });
    return conversationModel;
  };
}

export const fetchChat = channelId => {
  return async (dispatch, getState) => {
    const state = getState();
    if(!authorized(state.auth)) {
      return dispatch(handleUnauthorized());
    }
    const response = await fetch(`${Api.url}/chat/channels/${channelId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      }
    });

    if(!response.ok) {
      if(response.status === 401) {
        return dispatch(handleUnauthorized());
      }
      
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';
      throw new PowerChatError(title, message);
    }

    const resData = await response.json();
    const chatModel = new ChatModel(
      resData.id,
      resData.name,
      resData.interlocutor,
      resData.messages,
      toLocal(resData.lastActive),
      resData.isOnline
    );

    dispatch({ type: FETCH_CHAT, chat: chatModel });
    return chatModel;
  }
}

export const sendMessage = (channelId, message) => {
  return async (dispatch, getState) => {
    const state = getState();
    if(!authorized(state.auth)) {
      return dispatch(handleUnauthorized());
    }

    const presendId = new Date().getUTCMilliseconds();
    message.id = presendId;

    dispatch({type: PRESEND_MESSAGE, channelId: channelId, message: message});

    const response = await fetch(`${Api.url}/chat/channels/${channelId}/messages`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      },
      body: JSON.stringify({
        content: message.content
      })
    });

    if(!response.ok) {
      if(response.status === 401) {
        return dispatch(handleUnauthorized());
      }
      
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';
      throw new PowerChatError(title, message);
    }

    message.id = await response.json();
    message.sentDate = new Date();

    dispatch({
      type: SEND_MESSAGE, 
      channelId: channelId,
      presendId: presendId, 
      message: message
    });
  }
}

export const registerSignalRChatCommands = (store, connection) => {
  connection.on('ReceiveMessage', data => {
    store.dispatch({
      type: RECEIVE_MESSAGE, 
      channelId: data.channelId, 
      message: data.message 
    });
  })
}