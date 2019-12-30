import React, { useEffect, useState, useCallback, createRef } from 'react';
import { 
  View,
  KeyboardAvoidingView, 
  Keyboard,
  FlatList,
  TouchableOpacity,
  TouchableWithoutFeedback 
} from 'react-native';
import { 
  Icon, 
  Button,
  Input,
  Text, 
  Avatar,
  Spinner,
  TopNavigation, 
  TopNavigationAction, 
  withStyles 
} from '@ui-kitten/components';
import { useSelector, useDispatch } from 'react-redux';

import TextStyle from './../../constants/TextStyle';
import SafeAreaLayout, { SafeAreaInset } from '../../components/UI/view/SafeAreaLayout';
import ChatMessage from './../../components/chat/ChatMessage';
import { MessageModel } from './../../models/chat/MessageModel';
import * as chatActions from './../../store/actions/chat';

const chatScreen = props => {
  const { themedStyle, style, ...restProps } = props;
  const dispatch = useDispatch();

  const [inputMessage, setInputMessage] = useState("");
  const conversationPreview = props.navigation.getParam("conversationPreview");
  const chat = useSelector(state => 
    state.chat.chats.find(x => x.id === conversationPreview.id)
  );

  const [isLoading, setIsLoading] = useState(false);
  useEffect(() => {
    const loadChat = async () => {
      setIsLoading(true);
      await dispatch(chatActions.fetchChat(conversationPreview.id));
      setIsLoading(false);
    };

    loadChat();
  }, [dispatch]);

  let listRef = createRef();
  const onListContentSizeChange = () => {
    setTimeout(() => {
      if(listRef.current) listRef.current.scrollToEnd({ animated: true }); 
    }, 0);
  }

  const renderMessage = itemData => {
    return <ChatMessage
      style={themedStyle.message} 
      message={itemData.item} />
  }

  const onMessageChange = text => {
    setInputMessage(text);
  }

  const onSendMessage = useCallback(async (userId) => {
    const messageModel = new MessageModel(null, null, inputMessage,
      null, null, true);

    try {
      await dispatch(chatActions.sendMessage(chat.id, messageModel));
      setInputMessage("");
    } catch(err) {
      // TODO
    }
  }, [inputMessage, setInputMessage, dispatch])

  let imageSource = require('./../../assets/images/avatar-male.png');

  if(conversationPreview.imgUrl && conversationPreview.imgUrl.length > 0) {
    imageSource = {uri: conversationPreview.imgUrl}
  } else if(conversationPreview.gender && conversationPreview.gender === 'Female') {
    imageSource = require('./../../assets/images/avatar-female.png');
  }

  const backIcon = style => <Icon {...style} name='arrow-back'/>;
  const plusIcon = style => <Icon {...style} name='plus'/>;
  const sendIcon = style => <Icon {...style} name='paper-plane'/>;

  const renderLeftControls = () => [
    <TopNavigationAction icon={backIcon} onPress={() => {
      props.navigation.goBack();
    }} />
  ];

  const renderRightControls = () => [
    <TouchableOpacity activeOpacity={0.75} onPress={() => {
      if(chat.interlocutor.id) {
        props.navigation.navigate('profile', { userId: chat.interlocutor.id, fromChat: true });
      }
    }}>
      <Avatar source={imageSource} />
    </TouchableOpacity>
  ];

  let content = (
    <View style={themedStyle.loadingContainer}>
      <Spinner size="giant" />
    </View>
  );

  if(!isLoading) {
    content = (
      <KeyboardAvoidingView 
        style={themedStyle.container} 
        bouncesZoom={false}
        alwaysBounceVertical={false}
        alwaysBounceHorizontal={false}
        contentContainerStyle={themedStyle.contentContainer} 
        behavior="padding" 
        enabled>
        {!chat || chat.messages.length === 0 ?
          <View style={themedStyle.center}>
            <Text>You haven't talked yet</Text>
          </View> :
          <FlatList 
            ref={listRef}
            keyExtractor={item => item.id.toString()}
            contentContainerStyle={themedStyle.chatContainer}
            data={chat.messages}
            onContentSizeChange={onListContentSizeChange}
            renderItem={renderMessage}
          />
        }
        <View style={themedStyle.inputContainer}>
          <Button
            style={themedStyle.addMessageButton}
            textStyle={TextStyle.button}
            icon={plusIcon}
            onPress={() => {}}
          />
          <Input
            style={themedStyle.messageInput}
            textStyle={TextStyle.paragraph}
            placeholder='Message...'
            value={inputMessage}
            onChangeText={onMessageChange}
          />
          <Button
            style={themedStyle.addMessageButton}
            appearance='ghost'
            size='large'
            disabled={inputMessage.length === 0}
            icon={sendIcon}
            onPress={onSendMessage}
          />
        </View>
      </KeyboardAvoidingView>
    );
  }

  return (
    <SafeAreaLayout style={themedStyle.flex1} insets={SafeAreaInset.TOP}>
      <TopNavigation 
        title={conversationPreview.name}
        subtitle='Last seen 1h ago' 
        alignment='center'
        titleStyle={themedStyle.headerText}
        rightControls={renderRightControls()}
        leftControl={renderLeftControls()} />
      {content}
    </SafeAreaLayout>
  );
};

export default withStyles(chatScreen, theme => ({
  container: {
    flex: 1,
    backgroundColor: theme['background-basic-color-2']
  },
  flex1: {
    flex: 1,
    padding: 0
  },
  contentContainer: {
    flex: 1,
    flexGrow: 1,
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: theme['background-basic-color-2']
  },
  center: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center'
  },
  headerText: {
    ...TextStyle.subtitle
  },
  inputContainer: {
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: theme['background-basic-color-1'],
  },
  addMessageButton: {
    width: 26,
    height: 26,
    borderRadius: 26,
  },
  messageInput: {
    flex: 1,
    marginHorizontal: 8,
  },
  chatContainer: {
    paddingHorizontal: 16,
    paddingVertical: 12
  },
  message: {
    marginVertical: 12
  }
}));