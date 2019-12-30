import React from 'react';
import { Layout, withStyles } from '@ui-kitten/components';

const appLayout = props => {
  return (
    <Layout style={props.themedStyle.container}>
      {props.children}
    </Layout>
  );
};

export default withStyles(appLayout, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-2']
  }
}));