import Api from './../../constants/Api';
import PowerChatError from './../../models/PowerChatError';
import { AuthData } from '../../models/auth/AuthData';
import { storeAuthData } from './../../utils/auth';
import SignalRService from './../SignalRService';

export const SIGN_IN = 'SIGN_IN';
export const SIGN_UP = 'SIGN_UP';
export const SIGN_OUT = 'SIGN_OUT';
export const AUTHENTICATE = 'AUTHENTICATE';

export const signIn = (email, password) => {
  return async dispatch => {
    const response = await fetch(`${Api.url}/account/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        Email: email,
        Password: password
      })
    });

    if(!response.ok) {
      const errorData = await response.json();
      const errorCode = errorData.Code;
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';

      switch(errorCode)
      {
        case 'UserNotFound':
            title = 'Invalid credentials!';
            message = 'This email could not be found.';
            break;

        case 'InvalidCredentials':
            title = 'Invalid credentials!';
            message = 'Please check your credentials and try again.';
            break;
      }

      throw new PowerChatError(title, message);
    }

    const resData = await response.json();
    const authData = new AuthData(
      resData.userId,
      resData.token,
      resData.expires
    );

    SignalRService.connect(authData.token);

    dispatch({ type: SIGN_IN, sinInResponse: authData });
    storeAuthData(authData);
  };
};

export const signUp = (signUpModel) => {
  return async dispatch => {
    const response = await fetch(`${Api.url}/account/create`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        FirstName: signUpModel.firstname,
        LastName: signUpModel.lastname,
        Email: signUpModel.email,
        Password: signUpModel.password,
        Gender: signUpModel.gender
      })
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