import React from 'react';
import { useSafeArea } from 'react-native-safe-area-context';
import { Layout, withStyles } from '@ui-kitten/components';

export const SafeAreaInset = {
  TOP: 'TOP',
  BOTTOM: 'BOTTOM'
} 

const safeAreaLayout = props => {
  const safeAreaInsets = useSafeArea();
  const { insets, style, themedStyle, ...layoutProps } = props;

  const toStyleProp = (inset) => {
    switch (inset) {
      case SafeAreaInset.BOTTOM:
        return { paddingBottom: safeAreaInsets.bottom };
      case SafeAreaInset.TOP:
        return {
          ...themedStyle.container,
          paddingTop: safeAreaInsets.top,
        };
    }
  };

  const createInsets = () => {
    return React.Children.map(insets, toStyleProp);
  };

  return (
    <Layout
      {...layoutProps}
      style={[style, createInsets()]}
    />
  );
}

export default withStyles(safeAreaLayout, theme => ({
  container: { }
}));