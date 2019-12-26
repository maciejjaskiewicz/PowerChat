import React from 'react';
import { View, TouchableOpacity } from 'react-native';
import { Avatar, Icon, Text, Button, withStyles } from '@ui-kitten/components';

import TextStyle from './../../constants/TextStyle';

const userListItem = props => {
  const { themedStyle, style } = props;

  let imageSource = require('./../../assets/images/avatar-male.png');

  if(props.userPreviewModel.imgUrl && props.userPreviewModel.imgUrl.length > 0) {
    imageSource = {uri: props.userPreviewModel.imgUrl}
  } else if(props.userPreviewModel.gender && props.userPreviewModel.gender === 'Female') {
    imageSource = require('./../../assets/images/avatar-female.png');
  }

  return (
    <TouchableOpacity
      activeOpacity={0.75}
      style={[themedStyle.container, style]}
      onPress={props.onPreview}>
      <View style={themedStyle.leftSection}>
        <View style={themedStyle.avatarContainer}>
          <Avatar
            source={imageSource}
            style={themedStyle.avatar}/>
        </View>
        <View style={themedStyle.nameContainer}>
          <Text style={TextStyle.subtitle}>{props.userPreviewModel.name}</Text>
        </View>
      </View>
      <View style={themedStyle.rightSection}>
        {props.children}
      </View>
    </TouchableOpacity>
  );
}

export default withStyles(userListItem, theme => ({
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
  rightSection: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
  }
}))