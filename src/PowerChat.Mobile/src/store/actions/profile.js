import Api from './../../constants/Api';
import { handlUnauthorized, asertAuthorization } from './../../utils/auth';
import { ProfileModel } from './../../models/profile/ProfileModel';
import PowerChatError from './../../models/PowerChatError';

export const FETCH_PROFILE = 'FETCH_PROFILE';
export const UPDATE_PROFILE = 'UPDATE_PROFILE';

export const fetchProfile = () => {
  return async (dispatch, getState) => {
    const state = getState();
    asertAuthorization(state, dispatch);

    const response = await fetch(`${Api.url}/account`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      }
    });

    if(!response.ok) {
      if(response.status === 401) {
        dispatch(handlUnauthorized());
      }
      
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';
      throw new PowerChatError(title, message);
    }

    const resData = await response.json();
    const profile = new ProfileModel(
      resData.firstname,
      resData.lastname,
      resData.gender,
      resData.about,
      resData.email,
      new Date(resData.createdDate)
    );

    dispatch({
      type: FETCH_PROFILE,
      profile: profile
    });
  };
}

export const updateProfile = (updateProfileModel) => {
  return async (dispatch, getState) => {
    const state = getState();
    asertAuthorization(state, dispatch);

    const response = await fetch(`${Api.url}/account`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${state.auth.token}`
      },
      body: JSON.stringify({...updateProfileModel})
    });

    if(!response.ok) {
      let title = 'An Error Occurred!';
      let message = 'Something went wrong. Please try again.';

      console.log(await response.json());

      throw new PowerChatError(title, message);
    }

    dispatch({
      type: UPDATE_PROFILE,
      updateProfileModel: updateProfileModel
    });
  };
}