import React, { useState, useEffect, useCallback } from 'react';
import { View, FlatList, RefreshControl } from 'react-native';
import { 
  Icon, 
  Layout, 
  Text,
  Spinner,
  TopNavigation, 
  TopNavigationAction, 
  withStyles 
} from '@ui-kitten/components';
import { useSelector, useDispatch } from 'react-redux';

import TextStyle from './../../constants/TextStyle';
import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';
import ConversationPreview from './../../components/chat/ConversationPreview';
import * as chatActions from './../../store/actions/chat';

const conversationsScreen = props => {
  const { themedStyle, style, navigation, ...restProps } = props;
  const dispatch = useDispatch();

  const conversations = useSelector(state => state.chat.conversations);

  const [isLoading, setIsLoading] = useState(false);
  useEffect(() => {
    const loadFriends = async () => {
      setIsLoading(true);
      await dispatch(chatActions.fetchConversations());
      setIsLoading(false);
    };

    loadFriends();
  }, [dispatch]);

  const [refreshing, setRefreshing] = useState(false);
  const onRefresh = useCallback(async () => {
    setRefreshing(true);
    await dispatch(chatActions.fetchConversations());
    setRefreshing(false);
  }, [refreshing]);

  let content = (
    <View style={themedStyle.loadingContainer}>
      <Spinner size="giant" />
    </View>
  );

  if(!isLoading) {
    content = (
      <Layout style={themedStyle.container}>
        <FlatList 
          data={conversations}
          keyExtractor={item => item.id.toString()}
          style={themedStyle.container}
          refreshControl={<RefreshControl refreshing={refreshing} onRefresh={onRefresh} />}
          renderItem={itemData => 
            <ConversationPreview
              style={themedStyle.listItem}
              conversationPreviewModel={itemData.item}
              onSelect={() =>
                navigation.navigate('chat', { conversationPreview: itemData.item })
              }
            />
          }
        />
      </Layout>
    );
  }

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation 
        title='Conversations' 
        alignment='center'
        titleStyle={themedStyle.headerText} />
      {content}
    </SafeAreaLayout>
  );
};

export default withStyles(conversationsScreen, theme => ({
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