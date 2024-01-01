
"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var nameuser = document.getElementById("nameuser").value;

connection.on('StartConnection', renderConnection);
connection.on('sendmessage', recivemessage);
document.addEventListener('DOMContentLoaded', ready);
connection.on('ReciveUserOnline', ReciveUserOnline);
connection.onclose(function () {
    Disconnect();
});

connect();

window.addEventListener("beforeunload", function (event) {
    connection.invoke('Stopconnection', iduser);

    // جلوگیری از بستن صفحه به طور خودکار
    event.preventDefault();
    // تعریف پیام هشدار
    event.returnValue = "آيا مطمئن هستيد كه مي‌خواهيد صفحه را ببنديد؟";

});
function renderConnection(message) {

    onconnect();
    
    

}
function hidenconecttoserver()
{
    document.getElementById("conecttoserver").innerHTML = "";
    document.getElementById("conecttoserver").style.display = "none"

}
function ReciveUserOnline(user) {
        var Chattype = document.getElementById('htmluseronline');
        var namespan = document.createElement('span');
        namespan.textContent = user;
        Chattype.appendChild(namespan);
    

}


function connect() {
    connection.start().then(onconnect).catch(Disconnect);
}

function onconnect() {
    document.getElementById("conecttoserver").style.display = "block";
    document.getElementById("conecttoserver").innerHTML = "ارتباط با سرور وصل شد";
    setTimeout(hidenconecttoserver, 4000);
    sendUserOnline(document.getElementById("Idcode").value);
}
function Disconnect() {
    document.getElementById("conecttoserver").style.display = "block";
    document.getElementById("conecttoserver").innerHTML = "ارتباط با سرور قطع شد مجدد تلاش فرمایید";
    connect();
}
function sendUserOnline(iduser) {

    connection.invoke('senduser', iduser);
    
}
function sendMessage(text, name) {
    connection.invoke('sendMessage', text, name);

}
function recivemessage(text, name,time) {
    var span = document.createElement('span');
    span.className = "Chatname";
    span.textContent = name;
    var spantime = document.createElement('span');
    spantime.className = "Chattime";
    spantime.textContent = time;
    var textelement = document.createElement('p');
    textelement.className = "Chatdescription";
    textelement.textContent = text;
    var divchat = document.createElement('div');
    if (nameuser == name) {
        divchat.className = "owner";
    } else {
        divchat.className = "Audience";
    }

    divchat.appendChild(span);
    divchat.appendChild(textelement);
    divchat.appendChild(spantime);
    var chatbody = document.getElementById('chatbody');
    chatbody.appendChild(divchat);
}
function ready() {

    
    var chatform = document.getElementById('chatform');
    chatform.addEventListener('submit',
        function (e) {
            e.preventDefault();
            var text = e.target[0].value;
            var name = e.target[1].value;
            e.target[0].value = '';
            sendMessage(text, name);
            
        });
    
}
