import React from 'react';
import { View } from 'react-native';
import { Button, Icon, withStyles } from '@ui-kitten/components';

import Input from '../UI/Input';
import textStyle from './../../constants/TextStyle';

const signInForm = (props) => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <View style={[themedStyle.container, style]} {...restProps}>
      <View style={themedStyle.formContainer}>
        <Input
          id='email'
          textStyle={textStyle.paragraph}
          placeholder='Email'
          icon={(style) => <Icon {...style} name='person' />}
          onInputChange={() => {}}
        />
        <Input
          id='password'
          style={themedStyle.passwordInput}
          textStyle={textStyle.paragraph}
          placeholder='Password'
          secureTextEntry={true}
          icon={(style) => <Icon {...style} name='eye-off' />}
          onInputChange={() => {}}
        />
        <View style={themedStyle.forgotPasswordContainer}>
          <Button
            style={themedStyle.forgotPasswordButton}
            textStyle={themedStyle.forgotPasswordText}
            appearance='ghost'
            activeOpacity={0.75}
            onPress={() => {}}>
            Forgot your password?
          </Button>
        </View>
      </View>
    </View>
  );
}

export default withStyles(signInForm, theme => ({
  container: {},
  forgotPasswordContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
  },
  passwordInput: {
    marginTop: 16,
  },
  forgotPasswordButton: {
    paddingHorizontal: 0,
  },
  forgotPasswordText: {
    fontSize: 15,
    color: theme['text-hint-color'],
    ...textStyle.subtitle,
  }
}));

 