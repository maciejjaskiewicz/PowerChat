import React from 'react';
import { View, Image } from 'react-native';
import { Button, Layout, withStyles } from '@ui-kitten/components';

import ScrollableAvoidKeyboard from './../../components/UI/ScrollableAvoidKeyboard';
import SignInForm from './../../components/auth/SignInForm';
import textStyle from './../../constants/TextStyle';

const signInScreen = props => {
  const { themedStyle, style, ...restProps } = props;

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
        <SignInForm style={themedStyle.formContainer} />
        <Button
          style={themedStyle.signInButton}
          textStyle={textStyle.button}
          size='giant'
          onPress={() => {}}>
          SIGN IN
        </Button>
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