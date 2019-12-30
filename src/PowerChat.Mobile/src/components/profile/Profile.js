import React from 'react';
import { View } from 'react-native';
import {
  Layout, 
  Text,
  Icon,
  Button,
  withStyles 
} from '@ui-kitten/components';

import ContainerView from './../UI/view/ContainerView'
import ProfileInfo from './ProfileInfo';
import TextStyle from './../../constants/TextStyle'

const profileScreen = props => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <ContainerView style={themedStyle.container} {...restProps}>
      <Layout style={themedStyle.profileInfoContainer}>
        <ProfileInfo
          name={props.profileModel.name}
          gender={props.profileModel.gender}
          imgUrl={props.profileModel.avatarUrl}
        />
        <View style={themedStyle.actionContainer}>
          {props.profileModel.isFriend ?
            <Button
              style={themedStyle.followButton}
              textStyle={TextStyle.button}
              disabled={true}>
              FRIEND
            </Button> :
            <Button
              style={themedStyle.followButton}
              textStyle={TextStyle.button}
              icon={(style) => <Icon {...style} name='person-add' />}
              onPress={props.onAddFriend}>
              ADD
            </Button>
          }
          <Button
            style={themedStyle.messageButton}
            textStyle={TextStyle.button}
            appearance={!props.profileModel.isFriend ? 'outline' : 'filled'}
            icon={(style) => <Icon {...style} name='message-circle' />}
            onPress={props.onMessage}>
            MESSAGE
          </Button>
        </View>
        <Layout style={themedStyle.activityContainer}>
          <View 
            activeOpacity={0.65}
            style={themedStyle.parameterContainer}>
              <Text style={themedStyle.valueLabel}>{props.profileModel.friends}</Text>
              <Text
                style={themedStyle.hintLabel}
                appearance='hint'
                category='s2'>
                Friends
              </Text>
          </View>
          <View 
            activeOpacity={0.65}
            style={[themedStyle.parameterContainer, themedStyle.marginLeft]}>
              <Text style={themedStyle.valueLabel}>{`1h ago`}</Text>
              <Text
                style={themedStyle.hintLabel}
                appearance='hint'
                category='s2'>
                Last seen
              </Text>
          </View>
        </Layout>
      </Layout>
      <View style={themedStyle.profileSection}>
        <Text style={themedStyle.profileSectionLabel} category='s1'>
          About
        </Text>
        <Text
          style={themedStyle.profileSectionContent}
          appearance='hint'>
          {props.profileModel.about && props.profileModel.about.length > 0 
            ? props.profileModel.about 
            : "The user hasn't provided any description yet" }
        </Text>
      </View>
    </ContainerView>
  );
};

export default withStyles(profileScreen, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-2']
  },
  profileInfoContainer: {
    paddingHorizontal: 24,
    paddingBottom: 24
  },
  actionContainer: {
    flexDirection: 'row',
    marginTop: 32,
  },
  followButton: {
    flex: 1,
    marginRight: 4,
  },
  messageButton: {
    flex: 1,
    marginLeft: 4
  },
  activityContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    marginTop: 24,
    paddingVertical: 16
  },
  parameterContainer: {
    alignItems: 'center',
  },
  marginLeft: {
    marginLeft: 40
  },
  valueLabel: {
    ...TextStyle.caption2,
    color: 'white'
  },
  hintLabel: { 
    ...TextStyle.subtitle,
    color: 'white'
  },
  profileSection: {
    marginTop: 32,
  },
  profileSectionLabel: {
    marginHorizontal: 16,
    ...TextStyle.subtitle,
  },
  profileSectionContent: {
    marginTop: 8,
    marginHorizontal: 16,
    ...TextStyle.paragraph
  }
}));