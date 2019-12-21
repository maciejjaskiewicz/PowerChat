import React from 'react';
import { 
  Divider, 
  Layout, 
  Text, 
  TopNavigation, 
  withStyles 
} from '@ui-kitten/components';

import SafeAreaLayout, { SafeAreaInset } from './../../components/UI/SafeAreaLayout';

const profileScreen = props => {
  const { themedStyle, style, ...restProps } = props;

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation title='Profile' alignment='center' />
      <Divider/>
      <Layout style={themedStyle.container}>
        <Text>Profile</Text>
      </Layout>
    </SafeAreaLayout>
  );
};

export default withStyles(profileScreen, theme => ({
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