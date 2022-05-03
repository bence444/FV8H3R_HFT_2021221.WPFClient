let connection;

let log = console.log;

let Users = Array();

$("document").ready(function () {
    function SetupSignalR() {
        connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:48623/hub")
            .configureLogging()
            .build();

        connection.on("User created", (user, msg) => GetData());
        connection.on("User deleted", (user, msg) => GetData());

        connection.onclose(async () => await Start());

        Start();
    }

    async function Start() {
        try {
            await connection.Start();
        }
        catch (error) {
            log(error);
            setTimeout(start, 2500);
        }
    }

    async function GetData() {
        log("GetData");
        await fetch("http://localhost:48623/user/")
            .then(x => x.json())
            .then(y => { Users = y; Display(); });
    }

    function Display() {
        $("#results").html("");
    }

    function Post() {
        var name = $("#DogName").val();

        await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ UserName: name }),
        })
            .then(response => response)
            .then(data => { log("User added"); GetData(); })
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
            body: JSON.stringify({UserId: Id, UserName: name}),
        })
            .then(response => response)
            .then(data => { log("User updated"); GetData(); })
            .catch(error => log("Error: ", error));
    }
});