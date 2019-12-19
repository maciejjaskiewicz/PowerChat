export const getCurrentStateName = state => {
	return findCurrentRoute(state).routeName;
};

export const getCurrentRouteState = navigation => {
	return findCurrentRoute(navigation.state);
};

const findCurrentRoute = state => {
	if (state.routes && state.routes.length !== 0) {
		return findCurrentRoute(state.routes[state.index]);
	}
	return state;
};