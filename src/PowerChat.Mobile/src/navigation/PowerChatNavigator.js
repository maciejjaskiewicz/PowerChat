import React from 'react';
import { enableScreens } from 'react-native-screens';
import { createStackNavigator } from 'react-navigation-stack';
import { createBottomTabNavigator } from 'react-navigation-tabs';
import { createAppContainer, createSwitchNavigator, SafeAreaView } from 'react-navigation'
import { Icon, BottomNavigation, BottomNavigationTab } from '@ui-kitten/components';

import SignInScreen from './../screens/auth/SignInScreen';
import SignUpScreen from './../screens/auth/SignUpScreen';
import ChannelsScreen from './../screens/chat/ChannelsScreen';
import FriendsScreen from './../screens/friends/FriendsScreen';
import ProfileScreen from './../screens/profile/ProfileScreen';
import EditProfileScreen from './../screens/profile/EditProfileScreen';

const authNavigator = createStackNavigator({
  SignIn: SignInScreen,
  SignUp: SignUpScreen
}, {
  headerMode: 'none',
  navigationOptions: {
    headerShown: 'false'
  }
});

const chatNavigator = createStackNavigator({
  channels: ChannelsScreen
}, {
  headerMode: 'none',
  navigationOptions: {
    headerShown: 'false'
  }
});

const friendsNavigator = createStackNavigator({
  friends: FriendsScreen
}, { headerMode: 'none' });

const profileNavigator = createStackNavigator({
  profile: ProfileScreen,
  editProfile: EditProfileScreen
}, { headerMode: 'none' });

const BottonTabBar = ({ navigation }) => {
  const onSelect = (index) => {
    const selectedTabRoute = navigation.state.routes[index];
    navigation.navigate(selectedTabRoute.routeName);
  };

  return (
    <SafeAreaView>
      <BottomNavigation selectedIndex={navigation.state.index} onSelect={onSelect}>
        <BottomNavigationTab icon={(style) => <Icon {...style} name='message-circle' />}/>
        <BottomNavigationTab icon={(style) => <Icon {...style} name='people' />} />
        <BottomNavigationTab icon={(style) => <Icon {...style} name='person' />} />
      </BottomNavigation>
    </SafeAreaView>
  );
}

const appNavigator = createBottomTabNavigator({
  Chat: chatNavigator,
  Friends: friendsNavigator,
  Profile: profileNavigator
}, {
  tabBarComponent: BottonTabBar,
  headerMode: 'screen',
  defaultNavigationOptions: {
    header: null,
  }
});

const powerChatNavigator = createSwitchNavigator({
  Auth: authNavigator,
  App: appNavigator
});

const createAppNavigator = () => {
  enableScreens();
  return createAppContainer(powerChatNavigator);
}

export default createAppNavigator();