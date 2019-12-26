import { FETCH_FRIENDS, ADD_FRIEND } from './../actions/friends';

initialState = {
  friends: []
}

const fetchFriendsHandler = (state, action) => {
  return {
    ...initialState,
    friends: action.friends
  };
}

const addFriendHandler = (state, action) => {
  return {
    ...state,
    friends: [ ...state.friends, action.friend ]
  };
}

export default (state = initialState, action) => {
  switch(action.type) {
    case FETCH_FRIENDS: return fetchFriendsHandler(state, action);
    case ADD_FRIEND: return addFriendHandler(state, action);
  }

  return state;
};