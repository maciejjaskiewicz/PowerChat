import React, { useEffect, useState, useCallback } from 'react';
import { View, FlatList, RefreshControl } from 'react-native';
import { 
  Text,
  Icon,
  Spinner,
  Button,
  TopNavigation, 
  TopNavigationAction,
  withStyles 
} from '@ui-kitten/components';
import { useSelector, useDispatch } from 'react-redux';

import ContainerView from './../../components/UI/view/ContainerView';
import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';
import TextStyle from './../../constants/TextStyle';
import UserListItem from './../../components/friends/UserListItem';
import * as friendsActions from './../../store/actions/friends';
import * as chatActions from './../../store/actions/chat';

const friendsScreen = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();

  const friends = useSelector(state => state.friends.friends);

  const [isLoading, setIsLoading] = useState(false);
  useEffect(() => {
    const loadFriends = async () => {
      setIsLoading(true);
      await dispatch(friendsActions.fetchFriends());
      setIsLoading(false);
    };

    loadFriends();
  }, [dispatch]);

  const [refreshing, setRefreshing] = useState(false);
  const onRefresh = useCallback(async () => {
    setRefreshing(true);
    await dispatch(friendsActions.fetchFriends());
    setRefreshing(false);
  }, [refreshing]);

  const onMessage = useCallback(async (userId) => {
    setIsLoading(true);
    const conversation = await dispatch(chatActions.getUserConversation(userId));
    setIsLoading(false);

    props.navigation.navigate('chat', { conversationPreview: conversation });
    
  }, [isLoading, dispatch])

  const addIcon = style => <Icon {...style} name='person-add'/>;
  const renderRightControls = () => [
    <TopNavigationAction icon={addIcon} onPress={() => {
      props.navigation.navigate('addFriend');
    }} />
  ];

  let content = (
    <View style={themedStyle.loadingContainer}>
      <Spinner size="giant" />
    </View>
  );

  if(!isLoading) {
    content = (
      <View style={themedStyle.container}>
        <FlatList 
          data={friends}
          keyExtractor={item => item.id.toString()}
          style={themedStyle.container}
          refreshControl={<RefreshControl refreshing={refreshing} onRefresh={onRefresh} />}
          renderItem={itemData => 
            <UserListItem
              style={themedStyle.listItem}
              userPreviewModel={itemData.item}
              onPreview={() => 
                props.navigation.navigate('friendProfile', { userId: itemData.item.id })
              }>
              <Button 
                icon={style => <Icon {...style} name='message-circle'/>}
                size='small' 
                onPress={() => onMessage(itemData.item.id)}
                />
            </UserListItem>
          }
        />
      </View>
    );
  }

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation 
        title='Friends' 
        alignment='center'
        titleStyle={themedStyle.headerText}
        rightControls={renderRightControls()} />
      {content}
    </SafeAreaLayout>
  );
};

export default withStyles(friendsScreen, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-2']
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: theme['background-basic-color-2']
  },
  flex1: {
    flex: 1
  },
  headerText: {
    ...TextStyle.subtitle
  },
  listItem: {
    marginTop: 1
  }
}));