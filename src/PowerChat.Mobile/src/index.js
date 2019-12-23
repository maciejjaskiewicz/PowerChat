import React, { useState } from 'react';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import { Provider } from 'react-redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import { AppLoading } from 'expo';
import * as Font from 'expo-font';
import { ApplicationProvider, IconRegistry } from '@ui-kitten/components';
import { mapping, dark as darkTheme } from '@eva-design/eva';
import { EvaIconsPack } from '@ui-kitten/eva-icons';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import ReduxThunk from 'redux-thunk';

import PowerChatNavigator from './navigation/PowerChatNavigator';
import AuthReducer from './store/reducers/auth';
import ProfileReducer from './store/reducers/profile';

const rootReducer = combineReducers({
  auth: AuthReducer,
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
  applyMiddleware(ReduxThunk)
));

const App = () => {
  const [fontLoaded, setFontLoaded] = useState(false);

  if(!fontLoaded) {
    return <AppLoading startAsync={fetchFonts} onFinish={() => {
        setFontLoaded(true);
      }}
    />
  }

  return (
    <React.Fragment>
      <IconRegistry icons={EvaIconsPack} />
      <ApplicationProvider mapping={mapping} theme={darkTheme}>
        <Provider store={store}>
          <SafeAreaProvider>
            <PowerChatNavigator /> 
          </SafeAreaProvider>
        </Provider>
      </ApplicationProvider>
    </React.Fragment>
  );
}

export default App;