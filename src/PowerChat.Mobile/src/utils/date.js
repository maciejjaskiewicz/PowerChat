import moment from 'moment-twitter';

export const second = 1e3
export const minute = 6e4
export const hour = 36e5
export const day = 864e5
export const week = 6048e5

export const toLocal = date => {
  if(!date) return date;

  const dateUtc = moment.utc(date).toDate();
  const local = moment(dateUtc).local().toDate();

  return local;
}

export const toMessageDate = sentDateUtc => {
  const now = new Date();
  const sentDate = toLocal(sentDateUtc);

  let formatedSentDate = '';
  if(now.getFullYear() === sentDate.getFullYear() && 
     now.getMonth() === sentDate.getMonth() &&
     now.getDate() === sentDate.getDate()) {
    formatedSentDate = moment(sentDate).format('HH:MM');
  } else {
    formatedSentDate = moment(sentDate).format('DD/MM/YY');
    formatedSentDate += "\n";
    formatedSentDate += moment(sentDate).format('HH:MM');
  }

  return formatedSentDate;
}

export const toTimeAgo = date => {
  const now = new Date();
  const diff = now - new Date(date);

  let timeAgo = moment(date).twitterLong();

  if(diff < week) {
    timeAgo += ' ago';
  }

  return timeAgo;
}