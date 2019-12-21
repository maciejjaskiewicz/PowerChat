export const FORM_INPUT_UPDATE = 'FORM_INPUT_UPDATE';

const inputUpdateHandler = (state, action) => {
  const updatedValues = {
    ...state.inputValues,
    [action.input]: action.value
  };  
  const updatedValidities = {
    ...state.inputValidities,
    [action.input]: action.isValid
  };
  let updatedFormIsValid = true;
  for (const key in updatedValidities) {
    updatedFormIsValid = updatedFormIsValid && updatedValidities[key];
  }
  return {
    formIsValid: updatedFormIsValid,
    inputValidities: updatedValidities,
    inputValues: updatedValues
  };
}

export const formReducer = (state, action) => {
  switch(action.type) {
    case FORM_INPUT_UPDATE: return inputUpdateHandler(state, action);
  }

  return state;
};