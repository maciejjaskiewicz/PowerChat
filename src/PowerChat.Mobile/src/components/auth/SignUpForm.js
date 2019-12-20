import React from 'react';
import { View } from 'react-native';
import { Icon, CheckBox, Radio, withStyles, Layout } from '@ui-kitten/components';

import TextInput from '../UI/form/TextInput';
import PasswordInput from '../UI/form/PasswordInput';
import RadioInput from './../UI/form/RadioInput';
import textStyle from './../../constants/TextStyle';
import { NameValidator, EmailValidator } from './../../utils/validators';

const signUpForm = props => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <View style={[themedStyle.container, style]} {...restProps}>
      <View style={themedStyle.formContainer}>
        <TextInput
          id='firstname'
          textStyle={textStyle.paragraph}
          placeholder='Firstname'
          icon={(style) => <Icon {...style} name='person' />}
          onInputChange={props.onInputChange}
          validator={NameValidator}
          required
        />
        <TextInput
          id='lastname'
          textStyle={textStyle.paragraph}
          style={themedStyle.inputMargin}
          placeholder='Lastname'
          icon={(style) => <Icon {...style} name='person' />}
          onInputChange={props.onInputChange}
          validator={NameValidator}
          required
        />
        <TextInput
          id='email'
          autoCapitalize='none'
          textStyle={textStyle.paragraph}
          style={themedStyle.inputMargin}
          placeholder='Email'
          icon={(style) => <Icon {...style} name='email' />}
          onInputChange={props.onInputChange}
          validator={EmailValidator}
          required
        />
        <PasswordInput
          id='password'
          style={themedStyle.inputMargin}
          textStyle={textStyle.paragraph}
          placeholder='Password'
          onInputChange={props.onInputChange}
          required
        />
        <RadioInput 
          id='gender'
          containerStyle={themedStyle.radioInput} 
          options={['Male', 'Female']} 
          onInputChange={props.onInputChange}>
          <Radio style={themedStyle.radio} textStyle={themedStyle.radioText} text='Male'/>
          <Radio style={themedStyle.radio} textStyle={themedStyle.radioText} text='Female'/>
        </RadioInput>
      </View>
    </View>
  );
};

export default withStyles(signUpForm, theme => ({
  container: {},
  forgotPasswordContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-end'
  },
  inputMargin: {
    marginTop: 16,
  },
  radioInput: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    width: '100%',
    marginTop: 8
  },
  radio: {
    margin: 8
  },
  radioText: {
    color: theme['text-hint-color'],
    ...textStyle.subtitle
  },
  termsCheckBoxText: {
    color: theme['text-hint-color'],
    ...textStyle.subtitle
  }
}));