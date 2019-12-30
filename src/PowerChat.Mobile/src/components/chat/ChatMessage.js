import React from 'react';
import { View } from 'react-native';
import { withStyles } from '@ui-kitten/components';

import LeftMessage from './LeftMessage';
import RightMessage from './RightMessage';

const chatMessage = props => {
  const { style, themedStyle, message } = props;

  const aligmentStyle = {
    justifyContent: !message.own ? 'flex-start' : 'flex-end'
  }

  return (
    <View style={[themedStyle.container, aligmentStyle, style]}>
      {!message.own ?
        <LeftMessage message={message} /> :
        <RightMessage message={message} />
      }
    </View>
  );
}

export default withStyles(chatMessage, theme => ({
  container: {
    flexDirection: 'row',
    alignItems: 'center'
  }
}));