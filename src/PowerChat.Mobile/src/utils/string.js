export const truncate = ( str, n, useWordBoundary ) => {
  if (str.length <= n) { return str; }
  var subString = str.substr(0, n-1);
  return (useWordBoundary 
     ? subString.substr(0, subString.lastIndexOf(' ')) 
     : subString) + "...";
};