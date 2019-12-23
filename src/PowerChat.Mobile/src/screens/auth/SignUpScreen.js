import React, { useState, useEffect, useReducer, useCallback } from 'react';
import { View, Image, Alert } from 'react-native';
import { Button, Layout, Spinner, withStyles } from '@ui-kitten/components';
import { useDispatch } from 'react-redux';

import ScrollableAvoidKeyboard from '../../components/UI/view/ScrollableAvoidKeyboard';
import SignUpForm from './../../components/auth/SignUpForm';
import textStyle from './../../constants/TextStyle';
import { FORM_INPUT_UPDATE, formReducer } from './../../utils/form';
import { SignUpModel } from './../../models/auth/SignUpModel';
import * as authActions from './../../store/actions/auth';

const signUpScreen = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();
  const formInitialState = {
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
  };
  const [formState, dispatchFormState] = useReducer(formReducer, formInitialState);

  const submitHandler = useCallback(async () => {
    if (!formState.formIsValid) {
      Alert.alert('Wrong input!', 'Please check the errors in the form.', [
        { text: 'Okay' }
      ]);
      return;
    }

    const signUpModel = new SignUpModel(
      formState.inputValues.firstname,
      formState.inputValues.lastname,
      formState.inputValues.email,
      formState.inputValues.password,
      formState.inputValues.gender
    );

    setError(null);
    setIsLoading(true);
    try {
      await dispatch(authActions.signUp(signUpModel));

      Alert.alert("Congratulations!", "Your account has been created!", [
        { 
          text: 'Ok', 
          onPress: () => props.navigation.goBack()
        }
      ]);
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
        <SignUpForm style={themedStyle.formContainer} onInputChange={inputChangeHandler} />
        {isLoading ? 
          <View style={themedStyle.loadingContainer}><Spinner size="large" /></View> :
          <Button
            style={themedStyle.signUpButton}
            textStyle={textStyle.button}
            size='giant'
            disabled={!formState.formIsValid}
            onPress={submitHandler}>
            SIGN UP
          </Button>
        }
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
  loadingContainer: {
    alignItems: 'center'
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