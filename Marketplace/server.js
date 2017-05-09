/* jslint node: true, esversion: 6 */

var io = require('socket.io')({
  transports: ['websocket'],
});

io.attach(4567);

var unity;
var browser;

var count = 0;

io.on('connection', (socket) => {

  let type = 0;
  count++;
  console.log("count: " + count);

  console.log('connection');

  socket.emit('requestType');

  socket.on('isBrowser', () => {
    console.log('got browser');
    type = 1;
    browser = socket;
  });

  socket.on('isReady', () => {
    socket.emit('readyStatus', unity !== undefined);
  });

  socket.on('run', (data) => {
    console.log('running');
    unity.emit('run', data);
  });

  socket.on('isUnity', () => {
    console.log(unity);
    if (unity !== undefined) {
      console.log('got unity again');
      type = 2;
      unity.disconnect();
      unity = socket;
    } else {
      console.log('got unity');
      type = 2;
      unity = socket;
    }
  });

  socket.on('finishedRun', data => {
    console.log('finished');
    browser.emit('finishedRun', data);
  });

  socket.on('disconnect', () => {
    count--;
    console.log("count: " + count);
    if (type === 1) {
      browser = undefined;
      console.log("lost browser");
    } else if (type === 2) {
      unity = undefined;
      console.log("lost unity");
      if (browser !== undefined) browser.emit('lostUnity');
    } else {
      console.log('lost something; fuck knows what');
    }
  });
});
