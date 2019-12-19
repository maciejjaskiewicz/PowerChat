import React from 'react';
import { withStyles } from '@ui-kitten/components';
import { KeyboardAwareScrollView } from 'react-native-keyboard-aware-scroll-view';

const scrollableAvoidKeyboard = props => {
  const { style, contentContainerStyle, themedStyle, ...restProps } = props;

  return (
    <KeyboardAwareScrollView
      bounces={false}
      bouncesZoom={false}
      alwaysBounceVertical={false}
      alwaysBounceHorizontal={false}
      style={[themedStyle.container, style]}
      contentContainerStyle={[themedStyle.contentContainer, contentContainerStyle]}
      enableOnAndroid={true}
      {...restProps}
    />
  );
};

export default withStyles(scrollableAvoidKeyboard, theme => ({
  container: {
    flex: 1,
  },
  contentContainer: {
    flexGrow: 1,
  }
}));