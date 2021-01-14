import Api from './../../constants/Api';
import PowerChatError from './../../models/PowerChatError';
import { AuthData } from '../../models/auth/AuthData';
import { storeAuthData } from './../../utils/auth';
import SignalRService from './../SignalRService';
import jwt_decode from "jwt-decode";

export const SIGN_IN = 'SIGN_IN';
export const SIGN_UP = 'SIGN_UP';
export const SIGN_OUT = 'SIGN_OUT';
export const AUTHENTICATE = 'AUTHENTICATE';

export const signIn = (email, password) => {
  return async dispatch => {
    const authParams = {
      'username': email,
      'password': password,
      'grant_type': 'password',
      'client_id': 'PowerChatMobileClient',
      'client_secret': 'PowerChatMobileClient',
      'scope': 'PowerChatAPI openid profile offline_access'
    }

    const payload = Object.keys(authParams).map((key) => {
      return encodeURIComponent(key) + '=' + encodeURIComponent(authParams[key]);
    }).join('&');

    const response = await fetch(`${Api.url}/identity/connect/token`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      body: payload
    });

    if(!response.ok) {
      const errorData = await response.json();
      const errorCode = errorData.error_description;
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';

      switch(errorCode)
      {
        case 'invalid_username_or_password':
            title = 'Invalid credentials!';
            message = 'Please check your credentials and try again.';
            break;
      }

      throw new PowerChatError(title, message);
    }

    const resData = await response.json();
    const decodedToken = jwt_decode(resData.access_token);

    console.log(decodedToken.sub);

    const authData = new AuthData(
      decodedToken.sub,
      resData.access_token,
      resData.expires_in
    );

    SignalRService.connect(authData.token);

    dispatch({ type: SIGN_IN, sinInResponse: authData });
    storeAuthData(authData);
  };
};

export const signUp = (signUpModel) => {
  return async dispatch => {
    const payload = JSON.stringify({
      FirstName: signUpModel.firstname,
      LastName: signUpModel.lastname,
      Email: signUpModel.email,
      Password: signUpModel.password,
      Gender: signUpModel.gender
    });

    const response = await fetch(`${Api.url}/identity/account/create`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: payload
    });

    if(!response.ok) {
      const errorData = await response.json();
      const errorCode = errorData.Code;
      const failures = errorData.Result;    

      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';

      if(errorCode === 'validation' && Array.isArray(failures) && failures.length > 0) {
        title = 'Oh, crap!';
        if(failures.some(x => x.Code === 'EmailAlreadyExists')) {
          message = 'A user with this email address already exists.';
        } else {
          message = 'Please check your data and try again.'
        }
      }

      throw new PowerChatError(title, message);
    }

    dispatch({ type: SIGN_UP });
  };
};

export const signOut = () => {
  return async dispatch => {
    SignalRService.disconnect();
    dispatch({ type: SIGN_OUT });
  };
};

export const authenticate = (authData) => {
  return async dispatch => {
    if(!SignalRService.isConnected()) {
      SignalRService.connect(authData.token);
    }

    dispatch({ type: AUTHENTICATE, authData: authData });
  };
};