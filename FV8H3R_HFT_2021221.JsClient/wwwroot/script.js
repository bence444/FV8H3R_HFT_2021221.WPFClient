let log = console.log;

let connection;
let Users = Array();

$("document").ready(function () {
    SetupSignalR();
    GetData();
});

function SetupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:48623/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("User added", (user, msg) => GetData());
    connection.on("User removed", (user, msg) => GetData());

    connection.onclose(async () => await start());

    Start();
}

async function Start() {
    try {
        await connection.start();
        log("SignalR connected");
    }
    catch (error) {
        log(error);
        setTimeout(Start, 5000);
    }
}

async function GetData() {
    log("GetData");
    await fetch("http://localhost:48623/user")
        .then(x => x.json())
        .then(y => { Users = y; Display(); });
}

function Display() {
    log(Users);

    $("#UserList").html("");

    Users.forEach(x => {
        $("#UserList").append('<div class="row my-2 py-3 z-depth-3 bg-light rounded" id="User.' + x.id + '">\
                <div class="col-md-1">#' + x.id + '</div>\
                <div class="col-md-9"><input class="d-block w-100" id="User.Name.' + x.id + '" value="' + x.name + '"></div>\
                <div class="col-md-2">\
                    <button onclick="Update(' + x.id + ');" class="btn btn-sm btn-block btn-primary mb-0">update</button>\
                    <button onclick="Remove(' + x.id + ');" class="btn btn-sm btn-block btn-outline-warning">delete</button>\
                </div>\
            </div >');
    });
}

function Post() {
    var name = $("#UserName").val();

    fetch("http://localhost:48623/user", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name: name }),
    })
        .then(response => response)
        .then(data => { log("User added"); GetData(); })
        .catch(error => log("Error: ", error));
}

function Remove(Id) {
    fetch("http://localhost:48623/user/" + Id, {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
        body: null,
    })
        .then(response => response)
        .then(data => { log("User removed"); GetData(); })
        .catch(error => log("Error: ", error));
}

function Update(Id) {
    var name = $("User.Name." + Id).val();

    fetch("http://localhost:48623/user", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ UserId: Id, UserName: name }),
    })
        .then(response => response)
        .then(data => { log("User updated"); GetData(); })
        .catch(error => log("Error: ", error));
}