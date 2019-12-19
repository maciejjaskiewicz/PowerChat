import React from 'react';
import { Layout, Text, withStyles } from '@ui-kitten/components'

const friendsScreen = props => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <Layout style={themedStyle.container}>
      <Text>Friends</Text>
    </Layout>
  );
};

export default withStyles(friendsScreen, theme => ({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center'
  }
}));