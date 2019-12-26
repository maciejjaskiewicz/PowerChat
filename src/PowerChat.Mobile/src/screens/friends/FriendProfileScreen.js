import React, { useEffect, useState, useCallback } from 'react';
import { View, Alert, RefreshControl } from 'react-native';
import { 
  Spinner,
  Icon,
  TopNavigation, 
  TopNavigationAction,
  withStyles 
} from '@ui-kitten/components';
import { useSelector, useDispatch } from 'react-redux';

import Profile from '../../components/profile/Profile';
import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';
import TextStyle from '../../constants/TextStyle';
import Api from './../../constants/Api';
import { authorized, handleUnauthorized } from './../../utils/auth';
import { UserProfileModel } from './../../models/profile/UserProfileModel'; 
import * as friendsActions from './../../store/actions/friends';

const loadFriendProfile = async (id, state) => {
  if(!authorized(state)) {
    handleUnauthorized();
    return;
  }

  const response = await fetch(`${Api.url}/users/${id}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${state.token}`
    }
  });

  if(!response.ok) {
    if(response.status === 401) {
      handleUnauthorized();
      return;
    }
    
    let title = 'An Error Occurred!';
    let message = 'Something went wrong. Please try again.';
    throw new PowerChatError(title, message);
  }

  const resData = await response.json();
  const profileModel = new UserProfileModel(
    resData.id,
    resData.fullName,
    resData.gender,
    resData.about,
    '', // TODO: avatar
    resData.isFriend,
    resData.friends
  );
  return profileModel;
}

const friendProfileScreen = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();
  const authState = useSelector(state => state.auth);

  const profileId = props.navigation.getParam('userId');
  const [profile, setProfile] = useState(UserProfileModel.empty());

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();

  const loadProfile = async () => {
    setIsLoading(true);
    const resProfile = await loadFriendProfile(profileId, authState);
    setProfile(resProfile);
    setIsLoading(false);
  };

  useEffect(() => {
    loadProfile();
  }, [authState]);

  useEffect(() => {
    if(error) {
      Alert.alert(error.title, error.message, [{ text: 'Ok' }]);
    }
  }, [error]);

  const [refreshing, setRefreshing] = useState(false);
  const onRefresh = useCallback(async () => {
    setRefreshing(true);
    const resProfile = await loadFriendProfile(profileId, authState);
    setProfile(resProfile);
    setRefreshing(false);
  }, [authState, refreshing]);

  const onAddFriendHandler = () => {
    Alert.alert('Are you sure?', `Do you want to add ${profile.name} to your friends?`, [
      { text: 'Cancel' },
      { test: 'Add', style: 'destructive', onPress: async () => {
        setError(null);
        setIsLoading(true);
        try {
          await dispatch(friendsActions.addFriend(profile.id));

          Alert.alert('Congratulations!', `You and ${profile.name} are friends now!`, [
            { text: 'Ok', onPress: loadProfile }
          ]);
        } catch(err) {
          setError(err);
          setIsLoading(false);
        }
      }}
    ]);
  }

  const backIcon = style => <Icon {...style} name='arrow-back'/>;
  const renderLeftControls = () => [
    <TopNavigationAction icon={backIcon} onPress={() => {
      props.navigation.goBack();
    }} />
  ];

  let content = (
    <View style={themedStyle.loadingContainer}>
      <Spinner size="giant" />
    </View>
  );

  if(!isLoading) {
    content = (
      <Profile
        profileModel={profile}
        refreshControl={<RefreshControl refreshing={refreshing} onRefresh={onRefresh} />}
        onAddFriend={onAddFriendHandler}
      />
    );
  }

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation 
        title='Profile' 
        alignment='center'
        titleStyle={themedStyle.headerText}
        leftControl={renderLeftControls()} />
      {content}
    </SafeAreaLayout>
  );
};

export default withStyles(friendProfileScreen, theme => ({
  container: { },
  flex1: {
    flex: 1
  },
  headerText: {
    ...TextStyle.subtitle
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: theme['background-basic-color-2']
  }
}));