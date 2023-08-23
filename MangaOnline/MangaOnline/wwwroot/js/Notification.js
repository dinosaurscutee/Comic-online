function notification() {
    var myDiv = document.getElementById("alertNotification");
    if(myDiv!==null){
        myDiv.remove();
    }
}

// Chạy hàm sayHello sau 3 giây
setTimeout(notification, 3000);