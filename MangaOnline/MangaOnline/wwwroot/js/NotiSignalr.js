var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/hubs/notification").build();


connectionNotification.on("LoadNotification", function (message) {
    console.log(111,message);
    if ($.cookie("MANGA_FOLLOW") !== '' && $.cookie("MANGA_FOLLOW") !== null) {
        let listFollow = stringToArray($.cookie("MANGA_FOLLOW"));
        let index = $.inArray(message, listFollow);
        if (index !== -1) {
            // co trong array 
            $('#noti-follow').show();
            $.cookie('NotiFollowStatus', 'True');
            // update cookie
        } else {
            // co trong array 
            console.log(111, 'ko');
        }
    }
});

connectionNotification.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

function stringToArray(str) {
    return str.split(",");
}