import React from 'react';
import { Dimensions, View } from 'react-native';
import { Text, Spinner, withStyles } from '@ui-kitten/components';

import { toMessageDate } from './../../utils/date';
import TextStyle from './../../constants/TextStyle';

const rightMessage = props => {
  const { themedStyle, message } = props;

  return (
    <View style={themedStyle.cloudContainer} key={1}>
      {message.sentDate ?
        <Text
          style={themedStyle.dateLabel}
          appearance='hint'
          category='c1'>
          {toMessageDate(message.sentDate)}
        </Text> :
        <Spinner />
      }
      <View style={[themedStyle.cloud, themedStyle.cloudRight]}>
        <Text
          style={themedStyle.messageLabel}>
          {message.content}
        </Text>
      </View>
      <View style={[themedStyle.triangle, themedStyle.triangleRight]}/>
    </View>
  )
}

export default withStyles(rightMessage, theme => ({
  triangle: {
    borderLeftWidth: 10,
    borderRightWidth: 10,
    borderBottomWidth: 15,
    borderLeftColor: 'transparent',
    borderRightColor: 'transparent',
    backgroundColor: 'transparent',
  },
  triangleRight: {
    transform: [{ rotate: '90deg' }],
    borderBottomColor: theme['text-hint-color'],
  },
  cloudContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  dateLabel: {
    ...TextStyle.caption1,
    textAlign: 'right'
  },
  cloud: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
    borderRadius: 8,
    maxWidth: Dimensions.get('window').width - 120,
  },
  cloudRight: {
    left: 3,
    backgroundColor: theme['text-hint-color'],
    marginLeft: 16,
  }
}));
