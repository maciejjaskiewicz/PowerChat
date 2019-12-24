import { Alert, AsyncStorage } from 'react-native';
import { NavigationActions } from 'react-navigation';

import { AuthData } from './../models/auth/AuthData';

export const handleUnauthorized = (dispatch) => {
  Alert.alert('Unauthorized!', 'Please sign in and try again.', [
    { 
      text: 'Okay', 
      onPress: () => {
        dispatch(NavigationActions.navigate('Auth'));
      }
    }
  ]);
}

export const assertAuthorization = (state, dispatch) => {
  if((!state.auth.token || state.auth.token.length === 0) ||
     (!state.auth.expires || state.auth.expires <= Date.now())) {
    handleUnauthorized(dispatch);
    return true;
  }

  return false;
}

export const storeAuthData = (authData) => {
  AsyncStorage.setItem('authData', JSON.stringify({
    userId: authData.userId,
    token: authData.token,
    expires: authData.expires.toISOString()
  }));
}

export const fetchAuthData = async () => {
  const authData = await AsyncStorage.getItem('authData');

  if (!authData) {
    return undefined;
  }

  const parsedAuthData = JSON.parse(authData);

  return new AuthData(
    parsedAuthData.userId,
    parsedAuthData.token,
    parsedAuthData.expires
  );
}