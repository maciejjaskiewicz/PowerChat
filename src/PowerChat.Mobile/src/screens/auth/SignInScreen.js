import React, { useState, useEffect, useReducer, useCallback } from 'react';
import { View, Image, Alert } from 'react-native';
import { Button, Layout, Spinner, withStyles } from '@ui-kitten/components';
import { useDispatch } from 'react-redux';

import ScrollableAvoidKeyboard from '../../components/UI/view/ScrollableAvoidKeyboard';
import SignInForm from './../../components/auth/SignInForm';
import textStyle from './../../constants/TextStyle';
import * as authActions from './../../store/actions/auth';
import { FORM_INPUT_UPDATE, formReducer } from './../../utils/form';

const signInScreen = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();
  const [formState, dispatchFormState] = useReducer(formReducer, {
    inputValues: {
      email: '',
      password: ''
    },
    inputValidities: {
      email: false,
      password: false
    },
    formIsValid: false
  });

  const submitHandler = useCallback(async () => {
    if (!formState.formIsValid) {
      Alert.alert('Wrong input!', 'Please check the errors in the form.', [
        { text: 'Okay' }
      ]);
      return;
    }

    setError(null);
    setIsLoading(true);
    try {
      await dispatch(authActions.signIn(
        formState.inputValues.email,
        formState.inputValues.password
      ));

      props.navigation.navigate('App');
    } catch (err) {
      setError(err);
      setIsLoading(false);
    }
  }, [formState]);

  useEffect(() => {
    if(error) {
      Alert.alert(error.title, error.message, [{ text: 'Ok' }]);   
    }
  }, [error]);

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
        <SignInForm style={themedStyle.formContainer} onInputChange={inputChangeHandler} />    
        {isLoading ? 
          <View style={themedStyle.loadingContainer}><Spinner size="large" /></View> :
          <Button
            style={themedStyle.signInButton}
            textStyle={textStyle.button}
            size='giant'
            disabled={!formState.formIsValid}
            onPress={submitHandler}>
            SIGN IN
          </Button>
        }
        <Button
          style={themedStyle.signUpButton}
          textStyle={themedStyle.signUpText}
          appearance='ghost'
          activeOpacity={0.75}
          onPress={() => {
            props.navigation.navigate('SignUp');
          }}>
          Don't have an account? Create
        </Button>
      </Layout>
    </ScrollableAvoidKeyboard>
  );
}

export default withStyles(signInScreen, theme => ({
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
  loadingContainer: {
    alignItems: 'center'
  },
  signInButton: {
    marginHorizontal: 16,
  },
  signUpButton: {
    marginVertical: 12,
  },
  signUpText: {
    color: theme['text-hint-color'],
    ...textStyle.subtitle,
  }
}));