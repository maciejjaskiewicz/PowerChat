import React from 'react';
import { ScrollView } from 'react-native';

const containerView = (props) => {
  const { children, ...restProps } = props;
  
  return (
    <ScrollView
      bounces={false}
      bouncesZoom={false}
      alwaysBounceVertical={false}
      alwaysBounceHorizontal={false}
      {...restProps}
    >
      {children}
    </ScrollView>
  );
}

export default containerView;