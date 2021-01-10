import React, { useEffect, useState, useCallback } from 'react';
import { View, FlatList, Alert, KeyboardAvoidingView } from 'react-native';
import { 
  Text, 
  Icon,
  Input,
  Button,
  Spinner,
  TopNavigation,
  TopNavigationAction,
  withStyles 
} from '@ui-kitten/components';
import { useSelector, useDispatch } from 'react-redux';

import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';
import TextStyle from './../../constants/TextStyle';
import Api from './../../constants/Api';
import { authorized, handleUnauthorized } from './../../utils/auth';
import UserPriviewModel from './../../models/UserPreviewModel';
import UserListItem from './../../components/friends/UserListItem';
import * as friendsActions from './../../store/actions/friends';

const loadUsers = async (searchStr, authState) => {
  if(!authorized(authState)) {
    handleUnauthorized();
    return;
  }

  const response = await fetch(`${Api.url}/users/users/search?searchStr=${searchStr}&excludeFriends=true`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${authState.token}`
    }
  });

  if(!response.ok) {
    if(response.status === 401) {
      handleUnauthorized();
      return;
    }
    
    let title = 'An Error Occurred!';
    let message = 'Something went wrong. Please try again.';
    throw new PowerChatError(title, message);
  }

  const resData = await response.json();
  const users = [];

  resData.forEach(user => {
    const userModel = new UserPriviewModel(
      user.id,
      user.name,
      user.gender,
      '', // TODO: avatar,
      user.isOnline
    );

    users.push(userModel);
  });

  return users;
}

const friendsScreen = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();

  const authState = useSelector(state => state.auth);
  const [searchStr, setSearchStr] = useState('');
  const [users, setUsers] = useState([]);

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();

  const fetchUsers = async () => {
    setIsLoading(true);
    const users = await loadUsers(searchStr, authState);
    setUsers(users);
    setIsLoading(false);
  };

  const tryFetchUsers = useCallback(() => {
    setError(null);
    if(searchStr.length >= 2) {
      try {
        fetchUsers();
      } catch(err) {
        setError(err);
        setIsLoading(false);
      }
    }
  }, [searchStr, setSearchStr, setUsers]);

  useEffect(() => {
    tryFetchUsers()
  }, [tryFetchUsers]);

  useEffect(() => {
    const willFocusSub = props.navigation.addListener('willFocus', tryFetchUsers);

    return () => {
      willFocusSub.remove();
    };
  }, [tryFetchUsers]);

  useEffect(() => {
    if(error) {
      Alert.alert(error.title, error.message, [{ text: 'Ok' }]);   
    }
  }, [error]);

  const onInput = text => {
    setSearchStr(text);
  }

  const onAddFriend = (id) => {
    const friendModel = users.find(x => x.id === id);

    Alert.alert('Are you sure?', `Do you want to add ${friendModel.name} to your friends?`, [
      { text: 'Cancel' },
      { test: 'Add', style: 'destructive', onPress: async () => {
        setError(null);
        setIsLoading(true);
        try {
          await dispatch(friendsActions.addFriend(id));

          Alert.alert('Congratulations!', `You and ${friendModel.name} are friends now!`, [
            { text: 'Ok', onPress: fetchUsers }
          ]);
        } catch(err) {
          setError(err);
          setIsLoading(false);
        }
      }}
    ]);
  }

  let content = (
    <View style={themedStyle.loadingContainer}>
      <Spinner size="giant" />
    </View>
  );

  if(!isLoading) {
    content = (
      <FlatList 
        data={users}
        keyExtractor={item => item.id.toString()}
        style={themedStyle.container}
        renderItem={itemData => 
          <UserListItem
            style={themedStyle.listItem}
            userPreviewModel={itemData.item}
            onPreview={() => 
              props.navigation.navigate('friendProfile', { userId: itemData.item.id })
            }>
            <Button size='small' onPress={() => onAddFriend(itemData.item.id)}>
              ADD
            </Button>
          </UserListItem>
        }
      />
    );
  }

  const backIcon = style => <Icon {...style} name='arrow-back'/>;
  const searchIcon = style => <Icon {...style} name='search'/>;

  const renderLeftControls = () => [
    <TopNavigationAction icon={backIcon} onPress={() => {
      props.navigation.goBack();
    }} />
  ];

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation 
        title='Add Friend' 
        alignment='center'
        titleStyle={themedStyle.headerText}
        leftControl={renderLeftControls()} />
      <KeyboardAvoidingView style={themedStyle.container} behavior="padding" enabled>
        <Input
          style={themedStyle.searchInput}
          textStyle={TextStyle.paragraph}
          icon={searchIcon}
          placeholder='Start typing a name...'
          onChangeText={onInput}
        />
        {content}
      </KeyboardAvoidingView>
    </SafeAreaLayout>
  );
};

export default withStyles(friendsScreen, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-2']
  },
  flex1: {
    flex: 1
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: theme['background-basic-color-2']
  },
  headerText: {
    ...TextStyle.subtitle
  },
  searchInput: {
    marginHorizontal: 16,
    marginVertical: 16,
  },
  listItem: {
    marginTop: 1
  }
}));