let log = console.log;

let connection;
let Users = Array();

$("document").ready(function () {
    SetupSignalR();
    GetData();

    function SetupSignalR() {
        connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:48623/hub")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.on("User created", (user, msg) => GetData());
        connection.on("User deleted", (user, msg) => GetData());

        connection.onclose(async () => await start());

        start();
    }

    async function start() {
        try {
            await connection.start();
            log("SignalR connected");
        }
        catch (error) {
            log(error);
            setTimeout(start, 5000);
        }
    }

    async function GetData() {
        log("GetData");
        await fetch("http://localhost:48623/user")
            .then(x => x.json())
            .then(y => { Users = y; Display(); });
    }

    function Display() {
        $("#UserList").html("");

        Users.forEach(x => {
            $("#UserList").html('<div class="row">' + x.Id + '</div>');
        });
    }

    function Post() {
        var name = $("#DogName").val();

        fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ UserName: name }),
        })
            .then(response => response)
            .then(data => { log("User created"); GetData(); })
            .catch(error => log("Error: ", error));
    }

    function Remove(Id) {
        fetch(url + Id, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" },
            body: null,
        })
            .then(response => response)
            .then(data => { log("User deleted"); GetData(); })
            .catch(error => log("Error: ", error));
    }

    function Update(Id) {
        var name = $(Id + ".Name").val()
        fetch("http://localhost:48623/user/" + Id, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ UserId: Id, UserName: name }),
        })
            .then(response => response)
            .then(data => { log("User updated"); GetData(); })
            .catch(error => log("Error: ", error));
    }
});