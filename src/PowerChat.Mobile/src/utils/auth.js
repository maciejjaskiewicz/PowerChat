import { Alert } from 'react-native';
import { NavigationActions } from 'react-navigation';

export const handlUnauthorized = () => {
  Alert.alert('Unauthorized!', 'Please sign in and try again.', [
    { 
      text: 'Okay', 
      onPress: () => {
        dispatch(NavigationActions.navigate('Auth'));
      }
    }
  ]);
}

export const asertAuthorization = (state, dispatch) => {
  if((!state.auth.token || state.auth.token.length === 0) ||
     (!state.auth.expires || state.auth.expires <= Date.now())) {
    handlUnauthorized();
  }

  return;
}