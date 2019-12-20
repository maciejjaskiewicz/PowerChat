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

const textInput = props => {
  const { themedStyle, style, id, onInputChange, ...restProps } = props;

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

  const textChangeHandler = text => {
    let isValid = true;

    if(props.validator) {
      isValid = props.validator(text);
    }

    if (props.required && text.trim().length === 0) {
      isValid = false;
    }

    dispatch({ type: INPUT_CHANGE, value: text, isValid: isValid });
  };

  const getStatus = () => {
    if (!inputState.isValid && inputState.touched) {
      return 'danger';
    }

    return 'basic';
  }
  
  return (
    <Input
      {...restProps}
      status={getStatus()}
      value={inputState.value}
      style={[themedStyle.container, style]}
      onChangeText={textChangeHandler}
      onBlur={lostFocusHandler}
    />
  );
};

export default withStyles(textInput, theme => ({
  container: {}
}));