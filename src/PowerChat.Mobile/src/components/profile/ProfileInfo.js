import React from 'react';
import { View } from 'react-native';
import { Avatar, Text, withStyles } from '@ui-kitten/components';
import { Ionicons } from '@expo/vector-icons';

import TextStyle from './../../constants/TextStyle';

const profileInfo = (props) => {
  const { style, themedStyle, ...restProps } = props;

  let imageSource = require('./../../assets/images/avatar-male.png');

  if(props.imgUrl && props.imgUrl.length > 0) {
    imageSource = {uri: props.imgUrl}
  } else if(props.gender && props.gender === 'Female') {
    imageSource = require('./../../assets/images/avatar-female.png');
  }

  return (
    <View style={[themedStyle.container, style]} {...restProps}>
      <Avatar style={themedStyle.profileAvatar} source={imageSource} />
      <Text
        style={themedStyle.nameLabel}
        category='h6'>
        {props.name}
      </Text>
      <View style={themedStyle.genderContainer}>
        <Ionicons name="md-female" size={14} color="white" />
        <Text style={themedStyle.genderText} appearance='hint' category='p2'>{props.gender}</Text>
      </View>
    </View>
  );
}

export default withStyles(profileInfo, theme => ({
  container: {
    alignItems: 'center'
  },
  profileAvatar: {
    width: 150,
    height: 150,
  },
  nameLabel: {
    marginTop: 16,
    color: 'white',
    ...TextStyle.headline,
  },
  genderContainer: {
    flexDirection: 'row',
    alignItems: 'center'
  },
  genderIcon: {
    width: 14,
    height: 14,
    tintColor: 'white'
  },
  genderText: {
    ...TextStyle.caption2,
    marginTop: 2,
    marginLeft: 6,
    color: 'white'
  }
}));