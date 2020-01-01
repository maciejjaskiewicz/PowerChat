import React from 'react';
import { View, TouchableOpacity } from 'react-native';
import { Avatar, Icon, Text, Button, withStyles } from '@ui-kitten/components';
import { Ionicons } from '@expo/vector-icons';

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
            {props.userPreviewModel.isOnline ?
              <View style={themedStyle.onlineIndicator}/> :
              null
            }
        </View>
        <View style={themedStyle.nameContainer}>
          <Text style={TextStyle.subtitle}>{props.userPreviewModel.name}</Text>
          <View style={themedStyle.genderContainer}>
            <Ionicons 
              name={props.userPreviewModel.gender === 'Male' ? 'md-male' : "md-female"} 
              size={10}
              color='#8F9BB3'
            />
            <Text 
              style={themedStyle.genderText} 
              appearance='hint' 
              category='c1' 
              adjustsFontSizeToFit={true}>
              {props.userPreviewModel.gender}
            </Text>
          </View>
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
  },
  genderContainer: {
    flexDirection: 'row',
    alignItems: 'center'
  },
  genderText: {
    marginLeft: 4
  },
  onlineIndicator: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: theme['color-success-default'],
    position: 'absolute',
    alignSelf: 'flex-end',
    bottom: 2,
    right: 18,
  }
}))