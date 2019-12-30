import moment from 'moment';

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