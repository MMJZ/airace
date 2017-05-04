// Main Socket.io server
const port = 4567;

let app    = require('express')();
let server = app.listen(port);
let io     = require('socket.io')(server);


app.get('/', function(req, res){
  res.sendFile(__dirname + '/index.html');
});

io.on('connection', function(socket){
    console.log('connection');
  socket.on("ResultToServer", function(result){
    console.log('goitem');
    socket.broadcast.emit('ResultToDisplay', result);
  });
  socket.on('ScriptToServer', function(script){
    console.log('gotem');
    socket.broadcast.emit("ScriptToUnity", script);
  });
});


