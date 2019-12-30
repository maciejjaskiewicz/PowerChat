import React from 'react';
import { Dimensions, View } from 'react-native';
import { Text, Spinner, withStyles } from '@ui-kitten/components';

import { toMessageDate } from './../../utils/date';
import TextStyle from './../../constants/TextStyle';

const leftMessage = props => {
  const { themedStyle, message } = props;

  return (
    <View style={themedStyle.cloudContainer} key={0}>
      <View style={[themedStyle.triangle, themedStyle.triangleLeft]}/>
      <View style={[themedStyle.cloud, themedStyle.cloudLeft]}>
        <Text
          style={themedStyle.messageLabel}>
          {message.content}
        </Text>
      </View>
      {message.sentDate ?
        <Text
          style={themedStyle.dateLabel}
          appearance='hint'
          category='c1'>
          {toMessageDate(message.sentDate)}
        </Text> :
        <Spinner />
      }
    </View>
  )
}

export default withStyles(leftMessage, theme => ({
  triangle: {
    borderLeftWidth: 10,
    borderRightWidth: 10,
    borderBottomWidth: 15,
    borderLeftColor: 'transparent',
    borderRightColor: 'transparent',
    backgroundColor: 'transparent',
  },
  triangleLeft: {
    transform: [{ rotate: '-90deg' }],
    borderBottomColor: theme['color-primary-default'],
  },
  cloudContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  dateLabel: TextStyle.caption1,
  cloud: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
    borderRadius: 8,
    maxWidth: Dimensions.get('window').width - 120,
  },
  cloudLeft: {
    left: -3,
    backgroundColor: theme['color-primary-default'],
    marginRight: 16,
  }
}));
