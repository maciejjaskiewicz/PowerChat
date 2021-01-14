import React, { useState } from 'react';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import { Provider } from 'react-redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import AppLoading  from 'expo-app-loading';
import * as Font from 'expo-font';
import { ApplicationProvider, IconRegistry, Layout } from '@ui-kitten/components';
import { mapping, dark as darkTheme } from '@eva-design/eva';
import { EvaIconsPack } from '@ui-kitten/eva-icons';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import ReduxThunk from 'redux-thunk';
import { LogBox } from 'react-native';

import AppLayout from './components/AppLayout';
import PowerChatNavigator from './navigation/PowerChatNavigator';
import NavigationService from './navigation/NavigationService';
import AuthReducer from './store/reducers/auth';
import ChatReducer from './store/reducers/chat';
import FriendsReducer from './store/reducers/friends';
import ProfileReducer from './store/reducers/profile';
import SignalRMiddleware from './store/middleware/signalRMiddleware';
import SignalRService from './store/SignalRService';
import { registerSignalRChatCommands } from './store/actions/chat';

const rootReducer = combineReducers({
  auth: AuthReducer,
  chat: ChatReducer,
  friends: FriendsReducer,
  profile: ProfileReducer
});

const fetchFonts = () => {
  return Font.loadAsync({
    'opensans-semibold': require('./assets/fonts/opensans-semibold.ttf'),
    'opensans-bold': require('./assets/fonts/opensans-bold.ttf'),
    'opensans-extrabold': require('./assets/fonts/opensans-extra-bold.ttf'),
    'opensans-light': require('./assets/fonts/opensans-light.ttf'),
    'opensans-regular': require('./assets/fonts/opensans-regular.ttf')
  });
}

const store = createStore(rootReducer, composeWithDevTools(
  applyMiddleware(ReduxThunk),
  applyMiddleware(SignalRMiddleware)
));

SignalRService.registerCommandFactry(store, registerSignalRChatCommands);

const App = () => {
  const [fontLoaded, setFontLoaded] = useState(false);

  LogBox.ignoreLogs(['Setting a timer for a long period of time']);
  LogBox.ignoreLogs(['It appears that you are using old version of react-navigation']);
  LogBox.ignoreLogs(['Animated: `useNativeDriver` was not specified.']);
  LogBox.ignoreLogs(['Your project is accessing the following']);
  LogBox.ignoreLogs(['The global "__expo" and "Expo" objects will']);
  LogBox.ignoreLogs(['Error: Connection disconnected with']);

  if(!fontLoaded) {
    return <AppLoading startAsync={fetchFonts} 
      onFinish={() => {
        setFontLoaded(true);
      }}
      onError={error => console.log(error)}
    />
  }

  return (
    <React.Fragment>
      <IconRegistry icons={EvaIconsPack} />
      <ApplicationProvider mapping={mapping} theme={darkTheme}>
        <AppLayout>
          <Provider store={store}>
            <SafeAreaProvider>
              <PowerChatNavigator ref={navigatorRef => {
                NavigationService.setTopLevelNavigator(navigatorRef);
              }} /> 
            </SafeAreaProvider>
          </Provider>
        </AppLayout>
      </ApplicationProvider>
    </React.Fragment>
  );
}

export default App;