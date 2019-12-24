import React, { useState, useEffect, useReducer, useCallback } from 'react';
import { View, Alert } from 'react-native';
import { 
  Icon,
  Text,
  Radio,
  Button,
  Spinner,
  TopNavigation,
  TopNavigationAction,
  withStyles
} from '@ui-kitten/components'
import { useSelector, useDispatch } from 'react-redux';

import ContainerView from './../../components/UI/view/ContainerView';
import ScrollableAvoidKeyboard from '../../components/UI/view/ScrollableAvoidKeyboard';
import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';
import ProfilePhoto from './../../components/profile/ProfilePhotoSetting';
import TextStyle from './../../constants/TextStyle';
import TextInput from './../../components/UI/form/TextInput';
import RadioInput from './../../components/UI/form/RadioInput';
import { NameValidator } from './../../utils/validators';
import { FORM_INPUT_UPDATE, formReducer } from './../../utils/form';
import { UpdateProfileModel } from './../../models/profile/UpdateProfileModel';
import * as profileActions from './../../store/actions/profile';

const editProfile = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();

  const profile = useSelector(state => state.profile);
  const [formState, dispatchFormState] = useReducer(formReducer, {
    inputValues: {
      firstname: profile.firstname,
      lastname: profile.lastname,
      gender: profile.gender,
      about: profile.about,
    },
    inputValidities: {
      firstname: true,
      lastname: true,
      gender: true,
      about: true
    },
    formIsValid: true
  });

  const submitHandler = useCallback(async () => {
    if (!formState.formIsValid) {
      Alert.alert('Wrong input!', 'Please check the errors in the form.', [
        { text: 'Okay' }
      ]);
      return;
    }

    setError(null);
    setIsLoading(true);
    try {
      await dispatch(profileActions.updateProfile(new UpdateProfileModel(
        formState.inputValues.firstname,
        formState.inputValues.lastname,
        formState.inputValues.gender,
        formState.inputValues.about,
      )));

      props.navigation.goBack();
    } catch (err) {
      setError(err);
      setIsLoading(false);
    }

  }, [formState]);

  const backIcon = style => <Icon {...style} name='arrow-back'/>;
  const renderLeftControls = () => [
    <TopNavigationAction icon={backIcon} onPress={() => {
      props.navigation.goBack();
    }} />
  ];

  useEffect(() => {
    if(error) {
      Alert.alert(error.title, error.message, [{ text: 'Ok' }]);   
    }
  }, [error]);

  const inputChangeHandler = useCallback(
    (inputIdentifier, inputValue, inputValidity) => {
      dispatchFormState({
        type: FORM_INPUT_UPDATE,
        value: inputValue,
        isValid: inputValidity,
        input: inputIdentifier
      });
    },
    [dispatchFormState]
  );

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation 
        title='Edit Profile' 
        alignment='center' 
        titleStyle={themedStyle.headerText}
        leftControl={renderLeftControls()}
      />
      <ScrollableAvoidKeyboard>
        <ContainerView style={themedStyle.container}>
          <View style={themedStyle.photoSection}>
            <ProfilePhoto 
              style={themedStyle.photo}
              imgUrl={profile.avatarUrl}
              gender={profile.gender}
              renderEditButton
            />
          </View>
          <View style={themedStyle.section}>
            <TextInput
              id='firstname'
              label='First Name'
              textStyle={TextStyle.paragraph}
              placeholder='First Name'
              onInputChange={inputChangeHandler}
              initiallyValid={true}
              initialValue={profile.firstname}
              validator={NameValidator}
              required
            />
            <TextInput
              id='lastname'
              label='Last Name'
              style={{marginTop: 10}}
              textStyle={TextStyle.paragraph}
              placeholder='Last Name'
              onInputChange={inputChangeHandler}
              initiallyValid={true}
              initialValue={profile.lastname}
              validator={NameValidator}
              required
            />
            <Text style={themedStyle.radioLabel}>Gender</Text>
            <RadioInput 
              id='gender'
              containerStyle={themedStyle.radioInput} 
              style={{marginTop: -10}}
              options={['Male', 'Female']}
              initiallyValid={true}
              initialValue={profile.gender}
              onInputChange={inputChangeHandler}>
              <Radio style={themedStyle.radio} textStyle={themedStyle.radioText} text='Male'/>
              <Radio style={themedStyle.radio} textStyle={themedStyle.radioText} text='Female'/>
            </RadioInput>
            <TextInput
              id='about'
              label='About'
              style={{marginTop: 10}}
              multiline
              numberOfLines={4}
              textStyle={TextStyle.paragraph}
              initiallyValid={true}
              initialValue={profile.about}
              placeholder='Write something about yourself'
              onInputChange={inputChangeHandler}
            />
          </View>
          {isLoading ? 
            <View style={themedStyle.loadingContainer}><Spinner size="large" /></View> :
            <Button
              style={themedStyle.button}
              textStyle={TextStyle.button}
              size='large'
              disabled={!formState.formIsValid}
              onPress={submitHandler}>
              SAVE
            </Button>
          }
        </ContainerView>
      </ScrollableAvoidKeyboard>
    </SafeAreaLayout>
  );
}

export default withStyles(editProfile, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-2']
  },
  headerText: {
    ...TextStyle.subtitle
  },
  flex1: {
    flex: 1
  },
  photoSection: {
    marginVertical: 40
  },
  photo: {
    width: 150,
    height: 150,
    alignSelf: 'center',
  },
  section: {
    padding: 20,
    backgroundColor: theme['background-basic-color-1']
  },
  button: {
    marginHorizontal: 24,
    marginVertical: 24,
  },
  radioInput: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    width: '100%'
  },
  radio: {
    margin: 8
  },
  radioLabel: {
    fontSize: 12,
    color: theme['text-hint-color'],
    marginTop: 8
  },
  radioText: {
    color: theme['text-hint-color'],
    ...TextStyle.subtitle
  },
  loadingContainer: {
    marginVertical: 24,
    alignItems: 'center'
  }
}));