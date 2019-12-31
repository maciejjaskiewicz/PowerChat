export default signalRMiddleware = store => {
  return (next) => async (action) => {
    //console.log(action);

    return next(action);
  }
}