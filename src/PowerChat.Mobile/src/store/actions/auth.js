export const SIGN_IN = 'SIGN_IN';

export const signIn = (email, password) => {
  return { type: SIGN_IN, credentials: { email, password } };
}