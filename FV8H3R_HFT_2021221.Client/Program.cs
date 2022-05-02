using ConsoleTools;
using FV8H3R_HFT_2021221.Models;
using System;

namespace FV8H3R_HFT_2021221.Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            RestService service = new RestService("http://localhost:48623");

            ConsoleMenu main = new ConsoleMenu(args, 0);

            ConsoleMenu userMenu = new ConsoleMenu(args, 1);
            ConsoleMenu matchMenu = new ConsoleMenu(args, 1);
            ConsoleMenu msgMenu = new ConsoleMenu(args, 1);
            ConsoleMenu extra = new ConsoleMenu(args, 1);

            main.Add("User menu", () => userMenu.Show());
            main.Add("Match menu", () => matchMenu.Show());
            main.Add("Message menu", () => msgMenu.Show());
            main.Add("Non-crud methods", () => extra.Show());

            userMenu.Add("Back to main menu", () => userMenu.CloseMenu());
            matchMenu.Add("Back to main menu", () => matchMenu.CloseMenu());
            msgMenu.Add("Back to main menu", () => msgMenu.CloseMenu());
            extra.Add("Back to main menu", () => extra.CloseMenu());

            main.Add("Quit", () => main.CloseMenu());

            #region user
            userMenu.Add("list-all (users)", () => {
                var results = service.Get<User>("/user");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });

            userMenu.Add("list-one (user)", () => {
                Console.Write("user's id: ");
                int id = int.Parse(Console.ReadLine());

                var result = service.GetSingle<User>("/user/" + id);

                Console.WriteLine(result);
                Console.ReadKey();
            });

            userMenu.Add("create (user)", () => {
                Console.Write("user's name: ");
                string name = Console.ReadLine();

                Console.Write("Bio: ");
                string bio = Console.ReadLine();

                service.Post(new User() { Name = name, Bio = bio, AvailableLikes = 10, RegDate = DateTime.Now }, "/user");

                Console.WriteLine("user added");
                Console.ReadKey();
            });

            userMenu.Add("delete (user)", () => {
                Console.Write("user's id: ");
                int id = int.Parse(Console.ReadLine());

                service.Delete(id, "/user");

                Console.WriteLine("user deleted");
                Console.ReadKey();
            });

            userMenu.Add("update (user)", () => {
                Console.Write("user's id: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("new name: ");
                string name = Console.ReadLine();

                Console.Write("new bio: ");
                string bio = Console.ReadLine();

                Console.Write("available likes: ");
                int likes = int.Parse(Console.ReadLine());

                var user = service.GetSingle<User>("/user/" + id);
                user.Name = name;
                user.Bio = bio;
                user.AvailableLikes = likes;

                service.Put(user, "/user/" + id);
            });
            #endregion

            #region match
            matchMenu.Add("list-all (matches)", () => {
                var results = service.Get<Match>("/match");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });

            matchMenu.Add("list-one (match)", () => {
                Console.Write("match's id: ");
                int id = int.Parse(Console.ReadLine());

                var result = service.GetSingle<Match>("/match/" + id);

                Console.WriteLine(result);
                Console.ReadKey();
            });

            matchMenu.Add("create (match)", () => {
                Console.Write("user1 id: ");
                int id1 = int.Parse(Console.ReadLine());

                Console.Write("user2 id: ");
                int id2 = int.Parse(Console.ReadLine());

                service.Post(new Match() { User_1 = id1, User_2 = id2 }, "/match");

                Console.WriteLine("match created");
                Console.ReadKey();
            });

            matchMenu.Add("delete (match)", () => {
                Console.Write("match's id: ");
                int id = int.Parse(Console.ReadLine());

                service.Delete(id, "/match");

                Console.WriteLine("match deleted");
                Console.ReadKey();
            });

            matchMenu.Add("update (match)", () => {
                Console.WriteLine("match's id: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("user1 id: ");
                int id1 = int.Parse(Console.ReadLine());

                Console.Write("user2 id: ");
                int id2 = int.Parse(Console.ReadLine());

                var match = service.GetSingle<Match>("/match/" + id);
                match.User_1 = id1;
                match.User_2 = id2;

                service.Put(match, "/match/" + id);
            });
            #endregion

            #region message
            msgMenu.Add("list-all (msg)", () => {
                var results = service.Get<Message>("/message");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });

            msgMenu.Add("list-one (msg)", () => {
                Console.Write("message's id: ");
                int id = int.Parse(Console.ReadLine());

                var result = service.GetSingle<Message>("/message/" + id);

                Console.WriteLine(result);
                Console.ReadKey();
            });

            msgMenu.Add("create (msg)", () => {
                Console.Write("sender's id: ");
                int sender = int.Parse(Console.ReadLine());

                Console.Write("match's id: ");
                int match = int.Parse(Console.ReadLine());

                Console.Write("text: ");
                string text = Console.ReadLine();

                service.Post(new Message() { SenderId = sender, MatchId = match, MessageSent = text }, "/message");

                Console.WriteLine("message created");
                Console.ReadKey();
            });

            msgMenu.Add("delete (msg)", () => {
                Console.Write("message's id: ");
                int id = int.Parse(Console.ReadLine());

                service.Delete(id, "/message");

                Console.WriteLine("message deleted");
                Console.ReadKey();
            });

            msgMenu.Add("update (msg)", () => {
                Console.WriteLine("message's id: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("new sender id: ");
                int sender = int.Parse(Console.ReadLine());

                Console.Write("new text: ");
                string text = Console.ReadLine();

                var msg = service.GetSingle<Message>("/message/" + id);
                msg.SenderId = sender;
                msg.MessageSent = text;

                service.Put(msg, "/message/" + id);
            });
            #endregion

            #region non-curd
            extra.Add("users w/ deleted match", () => {
                var results = service.Get<User>("/stat/userwdm");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });

            extra.Add("messages w/ user likes > 10", () => {
                var results = service.Get<Message>("/stat/highlikes");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });

            extra.Add("last match's messages", () => {
                var results = service.Get<Message>("/stat/lastchat");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });

            extra.Add("messages where user's name contains 'á'", () => {
                var results = service.Get<Message>("/stat/msgof");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });

            extra.Add("users with trust issues (deleted messages)", () => {
                var results = service.Get<User>("/stat/issues");

                foreach (var item in results)
                    Console.WriteLine(item);

                Console.ReadKey();
            });
            #endregion

            main.Show();
        }
    }
}
