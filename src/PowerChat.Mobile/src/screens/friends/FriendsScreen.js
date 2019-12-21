import React from 'react';
import { 
  Divider, 
  Layout, 
  Text, 
  TopNavigation, 
  withStyles 
} from '@ui-kitten/components';

import SafeAreaLayout, { SafeAreaInset } from './../../components/UI/SafeAreaLayout';

const friendsScreen = props => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation title='Friends' alignment='center' />
      <Divider/>
      <Layout style={themedStyle.container}>
        <Text>Friends</Text>
      </Layout>
    </SafeAreaLayout>
  );
};

export default withStyles(friendsScreen, theme => ({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: theme['background-basic-color-2']
  },
  flex1: {
    flex: 1
  }
}));