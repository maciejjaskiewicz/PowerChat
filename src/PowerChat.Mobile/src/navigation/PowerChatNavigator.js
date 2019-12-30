import React from 'react';
import { enableScreens } from 'react-native-screens';
import { createStackNavigator } from 'react-navigation-stack';
import { createBottomTabNavigator } from 'react-navigation-tabs';
import { createAppContainer, createSwitchNavigator, SafeAreaView } from 'react-navigation'
import { Icon, BottomNavigation, BottomNavigationTab } from '@ui-kitten/components';

import BootScreen from './../screens/BootScreen';
import SignInScreen from './../screens/auth/SignInScreen';
import SignUpScreen from './../screens/auth/SignUpScreen';
import ConversationsScreen from '../screens/chat/ConversationsScreen';
import ChatScreen from './../screens/chat/ChatScreen';
import FriendsScreen from './../screens/friends/FriendsScreen';
import FriendProfileScreen from '../screens/friends/FriendProfileScreen';
import AddFriendScreen from './../screens/friends/AddFriendScreen';
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
  conversations: ConversationsScreen,
  chat: ChatScreen,
  profile: FriendProfileScreen
}, {
  headerMode: 'none',
  navigationOptions: {
    headerShown: 'false'
  }
});

chatNavigator.navigationOptions = ({ navigation }) => {
  let tabBarVisible = true;
  for (let i = 0; i < navigation.state.routes.length; i++) {
    if (navigation.state.routes[i].routeName == "chat" ||
        navigation.state.routes[i].routeName == "profile") {
      tabBarVisible = false;
    }
  }

  return {
    tabBarVisible
  };
};


const friendsNavigator = createStackNavigator({
  friends: FriendsScreen,
  addFriend: AddFriendScreen,
  friendProfile: FriendProfileScreen,
  chat: ChatScreen
}, { headerMode: 'none' });

friendsNavigator.navigationOptions = ({ navigation }) => {
  let tabBarVisible = true;
  for (let i = 0; i < navigation.state.routes.length; i++) {
    if (navigation.state.routes[i].routeName == "chat") {
      tabBarVisible = false;
    }
  }

  return {
    tabBarVisible
  };
};

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
  Boot: BootScreen,
  Auth: authNavigator,
  App: appNavigator
});

const createAppNavigator = () => {
  enableScreens();
  return createAppContainer(powerChatNavigator);
}

export default createAppNavigator();