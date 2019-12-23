import React from 'react';
import { View } from 'react-native';
import { Avatar, Text, withStyles } from '@ui-kitten/components';
import { Ionicons } from '@expo/vector-icons';

import TextStyle from './../../constants/TextStyle';

const profileInfo = (props) => {
  const { style, themedStyle, ...restProps } = props;

  return (
    <View style={[themedStyle.container, style]} {...restProps}>
      <Avatar style={themedStyle.profileAvatar} source={require('./../../assets/images/profile.jpg')} />
      <Text
        style={themedStyle.nameLabel}
        category='h6'>
        Scarlett Johansson
      </Text>
      <View style={themedStyle.genderContainer}>
        <Ionicons name="md-female" size={14} color="white" />
        <Text style={themedStyle.genderText} appearance='hint' category='p2'>Female</Text>
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