import { SIGN_IN, SIGN_OUT, AUTHENTICATE } from './../actions/auth';

const initialState = {
  userId: null,
  token: null,
  expires: null
};

const signInHandler = (state, action) => {
  return {
    userId: action.sinInResponse.userId,
    token: action.sinInResponse.token,
    expires: action.sinInResponse.expires
  }
};

const signOutHandler = (state, action) => {
  return {
    ...initialState
  };
};

const authenticateHandler = (state, action) => {
  return {
    userId: action.authData.userId,
    token: action.authData.token,
    expires: action.authData.expires
  }
};

export default (state = initialState, action) => {
  switch(action.type) {
    case SIGN_IN: return signInHandler(state, action);
    case SIGN_OUT: return signOutHandler(state, action);
    case AUTHENTICATE: return authenticateHandler(state, action);
  }

  return state;
};