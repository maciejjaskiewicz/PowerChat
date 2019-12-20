import React from 'react';
import { View } from 'react-native';
import { Button, Icon, withStyles } from '@ui-kitten/components';

import TextInput from '../UI/form/TextInput';
import PasswordInput from '../UI/form/PasswordInput';
import textStyle from './../../constants/TextStyle';
import { EmailValidator } from './../../utils/validators';

const signInForm = (props) => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <View style={[themedStyle.container, style]} {...restProps}>
      <View style={themedStyle.formContainer}>
        <TextInput
          id='email'
          autoCapitalize='none'
          textStyle={textStyle.paragraph}
          placeholder='Email'
          icon={(style) => <Icon {...style} name='person' />}
          onInputChange={props.onInputChange}
          validator={EmailValidator}
          required
        />
        <PasswordInput
          id='password'
          style={themedStyle.passwordInput}
          textStyle={textStyle.paragraph}
          placeholder='Password'
          onInputChange={props.onInputChange}
          required
        />
        <View style={themedStyle.forgotPasswordContainer}>
          <Button
            style={themedStyle.forgotPasswordButton}
            textStyle={themedStyle.forgotPasswordText}
            appearance='ghost'
            activeOpacity={0.75}
            onPress={props.onForgotPassword}>
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
    justifyContent: 'flex-end'
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

 