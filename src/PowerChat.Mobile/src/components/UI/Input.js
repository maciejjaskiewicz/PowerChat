import React, { useReducer, useEffect } from 'react';
import { Input, withStyles } from '@ui-kitten/components';

const INPUT_CHANGE = 'INPUT_CHANGE';
const INPUT_BLUR = 'INPUT_BLUR';

const inputReducer = (state, action) => {
  switch (action.type) {
    case INPUT_CHANGE:
      return {
        ...state,
        value: action.value,
        isValid: action.isValid
      };
    case INPUT_BLUR:
      return {
        ...state,
        touched: true
      };
    default:
      return state;
  }
};

const input = props => {
  const { themedStyle, style, onInputChange, id, ...restProps } = props;

  const [inputState, dispatch] = useReducer(inputReducer, {
    value: props.initialValue ? props.initialValue : '',
    isValid: props.initiallyValid,
    touched: false
  });

  useEffect(() => {
    if (inputState.touched) {
      onInputChange(id, inputState.value, inputState.isValid);
    }
  }, [inputState, onInputChange, id]);

  const lostFocusHandler = () => {
    dispatch({ type: INPUT_BLUR });
  };

  const getStatus = () => {
    return 'basic';
  };
  
  return (
    <Input
      {...restProps}
      autoCapitalize='none'
      status={getStatus()}
      value={inputState.value}
      style={[themedStyle.container, style]}
      onChangeText={onInputChange}
      onBlur={lostFocusHandler}
    />
  );
};

export default withStyles(input, theme => ({
  container: {}
}));