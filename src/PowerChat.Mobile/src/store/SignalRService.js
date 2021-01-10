import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

import Api from './../constants/Api';

let _store;
let _hubConnection;
let _commandsFactories = [];

const isConnected = () => {
  return !!_hubConnection;
}

const connect = (token) => {
  _hubConnection = new HubConnectionBuilder()
    .withUrl(`${Api.url}/users/ws`, {
      accessTokenFactory: () => token
    })
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect()
    .build();

  _hubConnection.start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.log('Error while establishing websocket connection.'));

  _commandsFactories.forEach(cmdFactory => {
    cmdFactory(_store, _hubConnection);
  });

  // _hubConnection.onclose((error) => {
  //   console.log(error);
  //   connect(token);
  // });
}

const disconnect = () => {
  if(isConnected) {
    _hubConnection.stop();
  }
}

const connection = () => {
  return _hubConnection;
}

const registerCommandFactry = (store, commandFactory) => {
  _store = store;
  _commandsFactories.push(commandFactory);
}

export default {
  isConnected,
  connect,
  disconnect,
  registerCommandFactry,
  connection
};