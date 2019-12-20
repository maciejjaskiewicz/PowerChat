import React, { useState, useReducer, useEffect } from 'react';
import { RadioGroup, withStyles } from '@ui-kitten/components';

const INPUT_CHANGE = 'INPUT_CHANGE';

const inputReducer = (state, action) => {
  switch (action.type) {
    case INPUT_CHANGE:
      return {
        ...state,
        value: action.value,
        isValid: action.isValid
      };
    default:
      return state;
  }
};

const radioInput = props => {
  const { themedStyle, style, id, onInputChange, ...restProps } = props;

  const [selectedIndex, setSelectedIndex] = useState(undefined);
  const [inputState, dispatch] = useReducer(inputReducer, {
    value: props.initialValue ? props.initialValue : '',
    isValid: props.initiallyValid ? props.initiallyValid : false 
  });

  useEffect(() => {
    onInputChange(id, inputState.value, inputState.isValid);
  }, [inputState, onInputChange, id]);

  const optionChangeHandler = index => {
    let isValid = true;
    let value = undefined;

    if(props.options[index]) {
      value = props.options[index];
      setSelectedIndex(index);
    } else {
      isValid = false;
    }

    dispatch({ type: INPUT_CHANGE, value: value, isValid: isValid });
  };

  return (
    <RadioGroup 
      {...restProps}
      style={props.containerStyle}
      selectedIndex={selectedIndex}
      onChange={optionChangeHandler}>
      {props.children}
    </RadioGroup>
  );
};

export default withStyles(radioInput, theme => ({
  container: {}
}));