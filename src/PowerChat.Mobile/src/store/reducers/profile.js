import { FETCH_PROFILE, UPDATE_PROFILE } from './../actions/profile';

const initialState = {
  firstname: '',
  lastname: '',
  gender: '',
  about: '',
  email: '',
  avatarUrl: '',
  joined: undefined
}

const fetchProfileHandler = (state, action) => {
  return {
    firstname: action.profile.firstname,
    lastname: action.profile.lastname,
    gender: action.profile.gender,
    about: action.profile.about,
    email: action.profile.email,
    avatarUrl: '',
    joined: action.profile.joined
  };
}

const updateProfileHandler = (state, action) => {
  return {
    ...state,
    firstname: action.updateProfileModel.firstname,
    lastname: action.updateProfileModel.lastname,
    gender: action.updateProfileModel.gender,
    about: action.updateProfileModel.about,
  }
}

export default (state = initialState, action) => {
  switch(action.type) {
    case FETCH_PROFILE: return fetchProfileHandler(state, action);
    case UPDATE_PROFILE: return updateProfileHandler(state, action); 
  }

  return state;
};

