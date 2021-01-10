import Api from './../../constants/Api';
import { handleUnauthorized, authorized } from './../../utils/auth';
import PowerChatError from './../../models/PowerChatError';
import UserPriviewModel from '../../models/UserPreviewModel';

export const FETCH_FRIENDS = 'FETCH_FRIENDS';
export const ADD_FRIEND = 'ADD_FRIEND';

export const fetchFriends = () => {
  return async (dispatch, getState) => {
    const state = getState();
    if(!authorized(state.auth)) {
      return dispatch(handleUnauthorized());
    }

    const response = await fetch(`${Api.url}/users/friends`, {
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

    const friends = []
    const resData = await response.json();

    resData.forEach(friend => {
      const friendModel = new UserPriviewModel(
        friend.id,
        friend.name,
        friend.gender,
        '', // TODO: avatar
        friend.isOnline
      );

      friends.push(friendModel);
    });

    dispatch({
      type: FETCH_FRIENDS,
      friends: friends
    });
  };
}

export const addFriend = (id) => {
  return async (dispatch, getState) => {
    const state = getState();
    if(!authorized(state.auth)) {
      return dispatch(handleUnauthorized());
    }

    const response = await fetch(`${Api.url}/users/friends`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      },
      body: JSON.stringify({
        userId: id
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

    const userResponse = await fetch(`${Api.url}/users/users/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      }
    });

    const userResData = await userResponse.json();
    const friend = new UserPriviewModel(
      userResData.id,
      userResData.fullName,
      userResData.gender,
      '', // TODO: avatar
      userResData.isOnline
    );

    dispatch({
      type: ADD_FRIEND,
      friend: friend
    });
  };
}