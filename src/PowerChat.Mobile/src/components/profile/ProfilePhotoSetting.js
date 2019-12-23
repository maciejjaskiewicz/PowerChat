import React from 'react';
import { View } from 'react-native';
import { Icon, Avatar, Button, withStyles } from '@ui-kitten/components'

const profilePhotoSetting = props => {
  const { style, themedStyle, ...restProps } = props;

  let imageSource = require('./../../assets/images/avatar-male.png');

  if(props.imgUrl && props.imgUrl.length > 0) {
    imageSource = {uri: props.imgUrl}
  } else if(props.gender && props.gender === 'Female') {
    imageSource = require('./../../assets/images/avatar-female.png');
  }

  return (
    <View style={style}>
      <Avatar
        style={[style, themedStyle.avatar]}
        source={imageSource}
        {...restProps}
      />
      {props.renderEditButton ?
        <Button
          style={themedStyle.photoButton}
          icon={(style) => <Icon {...style} name='edit'/>}
          onPress={() => {}}
        /> : null
      }
    </View>
  );
}

export default withStyles(profilePhotoSetting, theme => ({
  avatar: {
    alignSelf: 'center',
  },
  photoButton: {
    position: 'absolute',
    alignSelf: 'flex-end',
    top: 110,
    width: 48,
    height: 48,
    borderRadius: 24
  }
}));