import React, { useEffect } from 'react';
import { Spinner, Layout, withStyles } from '@ui-kitten/components'
import { useDispatch } from 'react-redux';

import { fetchAuthData } from './../utils/auth';
import * as authActions from './../store/actions/auth';

const bootScreen = (props) => {
  const dispatch = useDispatch();

  useEffect(() => {
    const tryLogin = async () => {
      const authData = await fetchAuthData();

      if(!authData || !authData.valid()) {
        props.navigation.navigate('Auth');
        return;
      }

      props.navigation.navigate('App');
      dispatch(authActions.authenticate(authData));
    };

    tryLogin();
  }, [dispatch]);

  return (
    <Layout style={props.themedStyle.container}>
      <Spinner size='giant' />
    </Layout>
  );
}

export default withStyles(bootScreen, theme => ({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center'
  }
}));