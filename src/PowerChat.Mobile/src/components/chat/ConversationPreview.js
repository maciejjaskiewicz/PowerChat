import React from 'react';
import { View, TouchableOpacity } from 'react-native';
import { Avatar, Icon, Text, Button, withStyles } from '@ui-kitten/components';
import moment from 'moment-twitter';

import TextStyle from './../../constants/TextStyle';
import { truncate } from './../../utils/string';

const conversationPreiew = props => {
  const { themedStyle, style } = props;

  let imageSource = require('./../../assets/images/avatar-male.png');

  if(props.conversationPreviewModel.imgUrl && props.conversationPreviewModel.imgUrl.length > 0) {
    imageSource = {uri: props.conversationPreviewModel.imgUrl}
  } else if(props.conversationPreviewModel.gender && props.conversationPreviewModel.gender === 'Female') {
    imageSource = require('./../../assets/images/avatar-female.png');
  }

  const renderMessageStatusImage = () => {
    if(!props.conversationPreviewModel.lastMessageDate) {
      return null;
    }

    let icon = (
      <Icon 
        {...themedStyle.messageIndicatorIcon} 
        {...themedStyle.messageIndicatorIconDelivered} 
        name='done-all-outline'/>
    );

    if(props.conversationPreviewModel.seen) {
      icon = (
        <Icon 
          {...themedStyle.messageIndicatorIcon} 
          {...themedStyle.messageIndicatorIconRead} 
          name='done-all-outline'/>
      );
    }

    return icon;
  }

  let date = '';

  if(!props.conversationPreviewModel.lastMessageDate){
    date = moment(props.conversationPreviewModel.createdDate).twitterShort()
  } else {
    date = moment(props.conversationPreviewModel.lastMessageDate).twitterShort()
  }

  return (
    <TouchableOpacity
      activeOpacity={0.75}
      style={[themedStyle.container, style]}
      onPress={props.onSelect}>
      <View style={themedStyle.leftSection}>
        <View style={themedStyle.avatarContainer}>
          <Avatar
            source={imageSource}
            style={themedStyle.avatar}/>
        </View>
        <View style={themedStyle.nameContainer}>
          <Text style={TextStyle.subtitle} category='s2'>{props.conversationPreviewModel.name}</Text>
          <Text
            style={themedStyle.lastMessageLabel}
            appearance='hint'
            category='c1'
            adjustsFontSizeToFit={true}>
            {!props.conversationPreviewModel.lastMessage ?
            "You haven't talked yet" :
            truncate(props.conversationPreviewModel.lastMessage, 30, true)}
          </Text>
        </View>
      </View>
      <View style={themedStyle.rightSection}>
        {renderMessageStatusImage()}
        <Text
          style={themedStyle.dateLabel}
          appearance='hint'
          category='p2'>
          {date}
        </Text>
      </View>
    </TouchableOpacity>
  );
}

export default withStyles(conversationPreiew, theme => ({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: 16,
    backgroundColor: theme['background-basic-color-1']
  },
  leftSection: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  avatarContainer: {
    justifyContent: 'flex-end',
    alignSelf: 'center'
  },
  avatar: {
    marginRight: 16,
  },
  lastMessageLabel: TextStyle.caption1,
  dateLabel: TextStyle.paragraph,
  rightSection: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
  },
  messageIndicatorIcon: {
    width: 18,
    height: 18,
    marginRight: 4,
  },
  messageIndicatorIconRead: {
    tintColor: theme['color-primary-default'],
  },
  messageIndicatorIconDelivered: {
    tintColor: theme['text-hint-color'],
  }
}))