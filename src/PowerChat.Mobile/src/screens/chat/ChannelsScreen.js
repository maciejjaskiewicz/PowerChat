import React from 'react';
import { 
  Divider, 
  Icon, 
  Layout, 
  Text, 
  TopNavigation, 
  TopNavigationAction, 
  withStyles 
} from '@ui-kitten/components';

import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';

const channelsScreen = props => {
  const { themedStyle, style, navigation, ...restProps } = props;

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation title='Conversations' alignment='center' leftControl={() =>
        <TopNavigationAction 
          icon={(style) => <Icon {...style} name='arrow-back' />} 
          onPress={() => {}}
        />
      }/>
      <Divider/>
      <Layout style={themedStyle.container}>
        <Text>Conversations</Text>
      </Layout>
    </SafeAreaLayout>
  );
};

export default withStyles(channelsScreen, theme => ({
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