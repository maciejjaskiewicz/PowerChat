import { SIGN_IN } from './../actions/auth';

const initialState = {
  userId: null,
  token: null,
  expires: null
};

const signInHandler = (state, action) => {
  console.log(action.sinInResponse);
  return {
    userId: action.sinInResponse.userId,
    token: action.sinInResponse.token,
    expires: action.sinInResponse.expires
  }
};

export default (state = initialState, action) => {
  switch(action.type) {
    case SIGN_IN: return signInHandler(state, action);
  }

  return state;
};