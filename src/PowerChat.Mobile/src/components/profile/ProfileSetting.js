import React from 'react';
import { View } from 'react-native';
import { Text, withStyles } from '@ui-kitten/components'

import TextStyle from './../../constants/TextStyle';

const profileSetting = props => {
  const { style, themedStyle, ...restProps } = props;
  
  const renderTextElement = (text, style) => {
    return <Text
      style={style}
      appearance='hint'>
      {text}
    </Text>
  };

  return (
      <View
        {...restProps}
        style={[themedStyle.container, style]}>
          {props.hint ? renderTextElement(props.hint, themedStyle.hintLabel) : null}
          {renderTextElement(props.value, themedStyle.valueLabel)}
      </View>
  );
}

export default withStyles(profileSetting, theme => ({
  container: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 16,
  },
  hintLabel: TextStyle.caption2,
  valueLabel: {
    color: theme['text-basic-color'],
    ...TextStyle.caption2,
  }
}));