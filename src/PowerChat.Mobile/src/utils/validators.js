export const PATTERN_EMAIL = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
export const PATTERN_PASSWORD = /(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}/;
export const PATTERN_NAME = /[a-zA-ZčČćĆđĐšŠžŽ-]+/;

export const EmailValidator = value => {
  return PATTERN_EMAIL.test(value);
};

export const PasswordValidator = value => {
  return PATTERN_PASSWORD.test(value);
};

export const NameValidator = value => {
  return PATTERN_NAME.test(value);
};