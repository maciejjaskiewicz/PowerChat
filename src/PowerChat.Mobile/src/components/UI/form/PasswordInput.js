import React, { useState } from 'react';
import { TouchableWithoutFeedback } from 'react-native';
import { Icon } from '@ui-kitten/components';

import Input from './TextInput';
import { PasswordValidator } from '../../../utils/validators';

const passwordInput = props => {
  const [peekMode, setPeekMode] = useState(false);

  const getIcon = (style) => {
    let icon = <Icon {...style} name='eye-off' />;

    if(peekMode){
      icon = <Icon {...style} name='eye' />;
    }

    return (
      <TouchableWithoutFeedback onPress={() => setPeekMode(!peekMode)}>
        {icon}
      </TouchableWithoutFeedback>
    );
  }

  return (
    <Input
      {...props}
      autoCapitalize='none'
      secureTextEntry={!peekMode}
      icon={(style) => getIcon(style)}
      validator={PasswordValidator}
    />
  );
};

export default passwordInput;