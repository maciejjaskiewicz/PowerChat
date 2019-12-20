import React, { useReducer, useCallback } from 'react';
import { View, Image, Alert } from 'react-native';
import { Button, Layout, withStyles } from '@ui-kitten/components';

import ScrollableAvoidKeyboard from './../../components/UI/ScrollableAvoidKeyboard';
import SignUpForm from './../../components/auth/SignUpForm';
import textStyle from './../../constants/TextStyle';
import { FORM_INPUT_UPDATE, formReducer } from './../../utils/form';

const signUpScreen = props => {
  const { themedStyle, style, ...restProps } = props;

  const [formState, dispatchFormState] = useReducer(formReducer, {
    inputValues: {
      firstname: '',
      lastname: '',
      email: '',
      password: '',
      gender: ''
    },
    inputValidities: {
      firstname: false,
      lastname: false,
      email: false,
      password: false,
      gender: false,
    },
    formIsValid: false
  });

  const submitHandler = useCallback(() => {
    console.log(formState);
    if (!formState.formIsValid) {
      Alert.alert('Wrong input!', 'Please check the errors in the form.', [
        { text: 'Okay' }
      ]);
      return;
    }

  }, [formState]);

  const inputChangeHandler = useCallback(
    (inputIdentifier, inputValue, inputValidity) => {
      dispatchFormState({
        type: FORM_INPUT_UPDATE,
        value: inputValue,
        isValid: inputValidity,
        input: inputIdentifier
      });
    },
    [dispatchFormState]
  );

  return (
    <ScrollableAvoidKeyboard style={themedStyle.container}>
      <Layout style={themedStyle.full}>
        <View style={themedStyle.headerContainer}>
          <Image 
            style={themedStyle.logo}
            resizeMode='contain'
            source={require('./../../assets/images/icon-white.png')} 
          />
        </View>
        <SignUpForm style={themedStyle.formContainer} onInputChange={inputChangeHandler} />
        <Button
          style={themedStyle.signUpButton}
          textStyle={textStyle.button}
          size='giant'
          onPress={submitHandler}>
          SIGN UP
        </Button>
        <Button
          style={themedStyle.signInButton}
          textStyle={themedStyle.signInText}
          appearance='ghost'
          activeOpacity={0.75}
          onPress={() => {
            props.navigation.goBack();
          }}>
          Already have an account? Sign In
        </Button>
      </Layout>
    </ScrollableAvoidKeyboard>
  );
};

export default withStyles(signUpScreen, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-1'],
  },
  full: {
    flex: 1
  },
  headerContainer: {
    justifyContent: 'center',
    alignItems: 'center',
    minHeight: 216,
    backgroundColor: theme['color-primary-default'],
  },
  logo: {
    width: '60%',
    marginTop: 10
  },
  formContainer: {
    flex: 1,
    marginTop: 32,
    paddingHorizontal: 16,
  },
  signUpButton: {
    marginTop: 12,
    marginHorizontal: 16,
  },
  signInButton: {
    marginVertical: 12,
  },
  signInText: {
    color: theme['text-hint-color'],
    ...textStyle.subtitle,
  }
}));