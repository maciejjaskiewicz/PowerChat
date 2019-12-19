import React from 'react';
import { Layout, Text, withStyles } from '@ui-kitten/components'

const profileScreen = props => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <Layout style={themedStyle.container}>
      <Text>Profile</Text>
    </Layout>
  );
};

export default withStyles(profileScreen, theme => ({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center'
  }
}));