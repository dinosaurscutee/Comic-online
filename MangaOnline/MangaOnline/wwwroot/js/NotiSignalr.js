var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/hubs/notification").build();


connectionNotification.on("LoadNotification", function (message) {
    console.log(111,message);
});

connectionNotification.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});
