import React, { useEffect } from 'react';
import { View } from 'react-native';
import {
  Icon,
  Text,
  Button,
  TopNavigation,
  TopNavigationAction,
  withStyles 
} from '@ui-kitten/components';
import moment from 'moment';
import { useSelector, useDispatch } from 'react-redux';

import ContainerView from './../../components/UI/view/ContainerView';
import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';
import TextStyle from './../../constants/TextStyle';
import ProfilePhoto from './../../components/profile/ProfilePhotoSetting';
import ProfileSetting from './../../components/profile/ProfileSetting';
import * as profileActions from './../../store/actions/profile';

const profileScreen = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(profileActions.fetchProfile()); 
  }, [dispatch]);

  const profile = useSelector(state => state.profile);
  const joinedDate = moment(profile.joined).format('MMMM Do YYYY');

  const editIcon = style => <Icon {...style} name='edit'/>;
  const renderRightControls = () => [
    <TopNavigationAction icon={editIcon} onPress={() => {
      props.navigation.navigate('editProfile');
    }} />
  ];

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation 
        title='Profile' 
        alignment='center' 
        titleStyle={themedStyle.headerText}
        rightControls={renderRightControls()}
      />
      <ContainerView style={themedStyle.container}>
        <View style={themedStyle.photoSection}>
          <ProfilePhoto 
            style={themedStyle.photo}
            imgUrl={profile.avatarUrl}
            gender={profile.gender}
          />
        </View>
        <View style={themedStyle.descriptionSection}>
          <Text
            style={themedStyle.description}
            appearance='hint'>
            {profile.about && profile.about.length > 0 ? profile.about : "The user hasn't provided any description yet" }
          </Text>
        </View>
        <View style={themedStyle.section}>
          <ProfileSetting
            style={themedStyle.profileSetting}
            hint="First Name"
            value={profile.firstname}
          />
          <ProfileSetting
            style={themedStyle.profileSetting}
            hint="Last Name"
            value={profile.lastname}
          />
          <ProfileSetting
            style={themedStyle.profileSetting}
            hint="Gender"
            value={profile.gender}
          />
        </View>
        <View style={themedStyle.section}>
          <ProfileSetting
            style={themedStyle.profileSetting}
            hint="Email"
            value={profile.email}
          />
          <ProfileSetting
            style={themedStyle.profileSetting}
            hint="Joined"
            value={joinedDate}
          />
        </View>
        <Button
          style={themedStyle.button}
          textStyle={TextStyle.button}
          size='large'
          onPress={() => {}}>
          SIGN OUT
        </Button>
      </ContainerView>
    </SafeAreaLayout>
  );
};

export default withStyles(profileScreen, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-2']
  },
  headerText: {
    ...TextStyle.subtitle
  },
  flex1: {
    flex: 1
  },
  photoSection: {
    marginVertical: 40
  },
  descriptionSection: {
    paddingHorizontal: 24,
    paddingVertical: 24,
    backgroundColor: theme['background-basic-color-1'],
  },
  description: TextStyle.paragraph,
  section: {
    marginTop: 24,
    backgroundColor: theme['background-basic-color-1'],
  },
  profileSetting: {
    borderBottomWidth: 1,
    borderBottomColor: theme['border-basic-color-2'],
  },
  photo: {
    width: 150,
    height: 150,
    alignSelf: 'center',
  },
  button: {
    marginHorizontal: 24,
    marginVertical: 24,
  }
}));