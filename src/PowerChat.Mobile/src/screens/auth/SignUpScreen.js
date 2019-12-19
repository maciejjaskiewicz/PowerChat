import React from 'react';
import { View } from 'react-native';
import { Text, Button, withStyles } from '@ui-kitten/components';

const signUpScreen = props => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <View style={themedStyle.container}>
      <Text>Registration</Text>
      <Button onPress={() => {props.navigation.goBack()}}>SIGN IN</Button>
    </View>
  );
};

export default withStyles(signUpScreen, theme => ({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: theme['background-basic-color-1'],
  }
}));