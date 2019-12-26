import { Alert, AsyncStorage } from 'react-native';

import { AuthData } from './../models/auth/AuthData';
import NavigationService from './../navigation/NavigationService';

export const handleUnauthorized = () => {
  Alert.alert('Unauthorized!', 'Please sign in and try again.', [
    { 
      text: 'Okay', 
      onPress: () => {
        NavigationService.navigate('Auth', null);
      }
    }
  ]);

  return { type: 'UNAUTHORIZED' };
}

export const authorized = (authState) => {
  if((!authState.token || authState.token.length === 0) ||
     (!authState.expires || authState.expires <= Date.now())) {
    return false;
  }

  return true;
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