/* jslint node: true, esversion: 6 */

const express = require('express');
const app = express();
const http = require('http');
app.set('port', 6565);
app.set('ip', '127.0.0.1');
const server = http.createServer(app).listen(app.get('port'), app.get('ip'), () => {
  console.log('listen %s:%d ', app.get('ip'), app.get('port'));
});
const io = require('socket.io')(server);

var unity;
var browser;

io.on('connection', (socket) => {

  let isB;

  console.log('connection');

  socket.on('isBrowser', () => {
    console.log('got browser');
    isB = true;
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
    console.log('got unity');
    isB = false;
    unity = socket;
  });

  socket.on('finishedRun', data => {
    console.log('finished');
    browser.emit('finishedRun', data);
  });

  socket.on('disconnect', () => {
    if (isB) {
      browser = undefined;
      console.log("lost browser");
    } else {
      unity = undefined;
      console.log("lost unity");
    }
  });
});
