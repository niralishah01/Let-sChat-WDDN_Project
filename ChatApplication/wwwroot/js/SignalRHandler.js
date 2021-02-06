"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


/*connection.on("receiveMessage", function addMessageToChat(message) {
    let div = document.createElement('div');
    div.className = "bg-dark";
    let text = document.createElement('h3');
    text.className = "text-light";
    text.innerHTML = message.text;

    let datetime = document.createElement('span');
    datetime.className = "text-danger";
    var dtime = new Date(message.datetime);
    datetime.innerHTML =
        (message.dtime.getMonth() + 1) + "/"
        + message.dtime.getDate() + "/"
        + message.dtime.getFullYear() + " "
        + message.dtime.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    div.appendChild(text);
    div.appendChild(datetime);

    chatWindow.appendChild(div);
});*/

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(Message) {
    connection.invoke('SendPrivateMessage', Message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}
console.log(connection);
connection.on("receiveMessage", addMessageToChat);
/*class message {
    constructor(receiverid, userid, text, datetime, messageid, filePath) {
        this.receiverid = receiverid;
        this.userid = userid;
        this.text = text;
        this.datetime = datetime;
        this.messageid = messageid;
        this.filePath = filePath;
    }
}*/

//const chatWindow = document.getElementById("chatWindow");

/*document.getElementById("send").addEventListener("click", function (event) {
    var receiverid = document.getElementById("receiverid").value;
    var userid = document.getElementById("userid").value;
    var text = document.getElementById("text").value;
    var messageid = document.getElementById("messageid").value;
    var datetime = new Date();
    messageid = Number(messageid) + 1;
    //var filePath = null;
    var Message = new message(receiverid, userid, text, datetime.toString(), messageid, null);
    console.log(Message);
    sendMessageToHub(Message);
})*/
/*function addMessageToChat(message) {
    let div = document.createElement('div');
    div.className = "bg-dark";
    let text = document.createElement('h3');
    text.className = "text-light";
    text.innerHTML = message.text;

    let datetime = document.createElement('span');
    datetime.className = "text-danger";
    var dtime = new Date(message.datetime);
    datetime.innerHTML =
        (message.dtime.getMonth() + 1) + "/"
        + message.dtime.getDate() + "/"
        + message.dtime.getFullYear() + " "
        + message.dtime.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    div.appendChild(text);
    div.appendChild(datetime);

    chatWindow.appendChild(div);
}*/