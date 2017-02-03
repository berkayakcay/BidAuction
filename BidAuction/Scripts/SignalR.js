//(function() {
//    var announceHub = $.connection.announceHub;
//    $.connection.hub.start()
//        .done(function() {
//            console.log("SignalR works!");
//            announceHub.server.announce("A","Connected!");
//        })
//        .fail(function() { console.log("Error connecting to SignalR") });

//    announceHub.client.announce = function(group, message) {
//        showAnnouncement(group + ": ", message);
//    };
//    var showAnnouncement = function(message) {
//        $("#welcome-messages").append(message + "<br />");
//    };
//})()

(function () {
    var announceHub = $.connection.announceHub;
    var showAnnouncement;
    announceHub.client.announce = function (group, message) {
        showAnnouncement(group + ": " + message);
    };

    $.connection.hub.start()
        .done(function () {
            console.log("SignalR works!");
            announceHub.server.announce(document.getElementById("Id").value, "AAA");
        })
        .fail(function () {
            console.log("Error connecting to SignalR");
        });
    showAnnouncement = function (message) {
        $("#welcome-messages").append("<span>"+ message + "</span></br>");
    };
})()