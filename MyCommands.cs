using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Linq;
using System;
using System.Data;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Timers;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Threading;


namespace LukeBot2
{
    public class MyCommands
    {
        public static DataAccessLayer DAL = new DataAccessLayer();
        public const string BLOCK = "```";
        public int times = new int();

        //simple text responses
        [Command("hi"), Description("A simple greeting!")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");
        }

        //[Command("samantha"), Description("A message about a girl.")]
        //public async Task Samantha(CommandContext ctx)
        //{
        //    await ctx.RespondAsync("Samantha is the prettiest girl in world!");
        //}

        //[Command("affleck"), Description("Ben Affleck sucks!")]
        //public async Task Affleck(CommandContext ctx)
        //{
        //    await ctx.RespondAsync("The worst Batman ever produced. He only belongs in Jay and Silent Bob films! ben affleck doesn't deserve proper capitolization.");
        //}

        //[Command("lukus"), Description("A message about a boy.")]
        //public async Task Lukus(CommandContext ctx)
        //{
        //    await ctx.Message.RespondAsync("Not as pretty as Samantha but he makes cool bots!");
        //}

        [Command("ping"), Description("Ping Pong!")]
        public async Task Ping(CommandContext ctx)
        {
            //ping pong!
            Stopwatch sw = Stopwatch.StartNew();
            await ctx.Message.RespondAsync("Pong! (took: " + sw.ElapsedMilliseconds.ToString() + " ms)");
            sw.Stop();
            sw.Reset();
        }

        [Command("calc"), Description("Calculate the given expression")]
        public async Task Calc(CommandContext ctx, string expr)
        {
            string output = new DataTable().Compute(expr, null).ToString();
            var builder = new DiscordEmbedBuilder()
                    .WithTitle("Result:")
                    .WithColor(new DiscordColor(0x87A2EF))
                    .WithDescription(output);
            await ctx.RespondAsync(embed: builder.Build());
        }

        [Command("flipcoin"), Aliases("flip"), Description("Flips a coin for head or tails")]
        public async Task Flipcoin(CommandContext ctx)
        {
            int num = DAL.GetRandomNumber(1, 3);
            string output = string.Empty;

            if (num == 1)
                output = @"C:\Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Coin\Heads.PNG";
            else
                output = @"C:\Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Coin\Tails.PNG";
            //send output
            await ctx.Channel.SendFileAsync(output);
        }

        [Command("rps"), Description("Play rock paper scissors")]
        public async Task Rps(CommandContext ctx, string player)
        {
            int num = DAL.GetRandomNumber(1, 4);
            string bot = string.Empty;
            string winner = string.Empty;
            player = player.ToLower();

            switch (num)
            {
                case 1:
                    bot = "rock";
                    break;
                case 2:
                    bot = "paper";
                    break;
                case 3:
                    bot = "scissors";
                    break;
                default:
                    bot = "I broke";
                    break;
            }

            if (player == bot)
                winner = "draw";

            else if (player == "rock" && bot == "paper")
                winner = bot;

            else if (player == "rock" && bot == "scissors")
                winner = player;

            else if (player == "paper" && bot == "rock")
                winner = player;

            else if (player == "paper" && bot == "scissors")
                winner = bot;

            else if (player == "scissors" && bot == "rock")
                winner = bot;

            else if (player == "scissors" && bot == "paper")
                winner = player;

            string output = $"You chose {player}. I chose {bot}.";
            if (winner == "draw")
                output += "\nIt was a draw!";
            else
            {
                if (winner == player)
                {
                    output += "\nYou won!";
                }
                else
                {
                    output += "\nYou lost!";
                }
            }
            await ctx.RespondAsync(output);
        }

        [Command("roll"), Description("Role dice for a dicing game")]
        public async Task Roll(CommandContext ctx)
        {
            int roll = DAL.GetRandomNumber(1, 7);
            string output = string.Empty;
            //store image into output based on number rolled
            switch (roll)
            {
                case 1:
                    output = (@"C:\Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Dice\One.png");
                    break;
                case 2:
                    output = (@"C:\Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Dice\Two.png");
                    break;
                case 3:
                    output = (@"C:\Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Dice\Three.png");
                    break;
                case 4:
                    output = (@"C:\Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Dice\Four.png");
                    break;
                case 5:
                    output = (@"C:\Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Dice\Five.png");
                    break;
                case 6:
                    output = (@"C: \Users\lukus.house\source\repos\LukeBot2\LukeBot2\Images\Dice\Six.png");
                    break;
                default:
                    break;
            }
            //display output
            await ctx.Channel.SendFileAsync(output);
        }

        [Command("dice"), Aliases("d"), Description("Roll the dice, numbered 1-6")]
        public async Task Dice(CommandContext ctx)
        {
            int roll = DAL.GetRandomNumber(1, 101);
            await ctx.RespondAsync(roll.ToString());
        }

        [Command("knock"), Aliases("Knock"), Description("Tells a knock knock joke")]
        public async Task Knock(CommandContext ctx)
        {
            // storing jokes in a variable jokes

            string[] names =
            {
                "Dozen",
                "Avenue",
                "Ice Cream",
                "Adore",
                "Lettuce",
                "Mikey",
            };
            string[] answer =
            {
                "anybody want to let me in?",
                "knocked on this door before?",
                "if you don't let me in!",
                "is between us. Open up!",
                "in. Its cold out here!",
                "doesnt fit through this keyhole!"
            };
            int jokeChoice = DAL.GetRandomNumber(0, names.Length);
            //Formatting the output to return in a new line and plug in the output variables
            string output =
                "Knock, Knock.\n" +
                "Who's there?\n" +
                names[jokeChoice] + ".\n" +
                names[jokeChoice] + " who?\n" +
                names[jokeChoice] + " " + answer[jokeChoice];
            output = string.Concat(BLOCK, output, BLOCK);

            await ctx.RespondAsync(output);
        }

        [Command("stats"), Description("Retrieve a players stats")]
        public async Task Stats(CommandContext ctx, params string[] UserName)
        {
            await ctx.TriggerTypingAsync();
            string userName = string.Join("+", UserName);
            string output = string.Empty;
            try
            {
                Int32[][] playerData = DAL.GetStats(userName);
                //header of Skills table
                output = "Skill".PadRight(15) + "Level".PadRight(9) + "Experience".PadRight(14) + "Rank".PadRight(7) + "\n";
                //populate rolls of skills table
                for (int i = 0; i < DAL.skills.Length; i++)
                {
                    output += DAL.skills[i].PadRight(15) +
                         playerData[1][i].ToString("###,###,###").PadRight(9) +
                         playerData[2][i].ToString("###,###,###").PadRight(14) +
                         playerData[0][i].ToString("###,###,###").ToString().PadRight(7) + '\n';
                };
                output = string.Concat(BLOCK, output, BLOCK);
            }
            catch (Exception)
            {
                output = "There was an error searching for this user.";
            }
            await ctx.RespondAsync(output);
        }

        [Command("vstats"), Description("Retrieve a players virtual stats")]
        public async Task Vstats(CommandContext ctx, params string[] UserName)
        {
            await ctx.TriggerTypingAsync();
            string output = string.Empty;
            try
            {
                string userName = string.Join("+", UserName);

                string[][] playerData = DAL.GetVStats(userName);
                string[] newValues = new string[DAL.skills.Length + 1];
                newValues[0] = userName.Replace('+', ' ');                                // set the prepended value
                Array.Copy(DAL.skills, 0, newValues, 1, DAL.skills.Length);
                //populate rolls of skills table
                for (int i = 0; i < newValues.Length; i++)
                {
                    output += newValues[i].PadRight(15) +
                         playerData[0][i].PadRight(10) +
                         playerData[1][i].PadRight(15) +
                         playerData[2][i].PadRight(10) + '\n';
                };
                output = string.Concat(BLOCK, output, BLOCK);
            }
            catch (Exception)
            {
                output = "There was an error searching for this user.";
            }
            await ctx.RespondAsync(output);
        }

        [Command("activity"), Aliases("active"), Description("")]
        public async Task Levels(CommandContext ctx, params string[] UserName)
        {
            string userName = string.Join("+", UserName);
            string output = string.Empty;
            string[][] activity = DAL.GetActivity(userName);
            output += "Recent Activity for " + userName.Replace('+', ' ') + '\n' + '\n';
            for (int i = 0; i < 15; i++)
            {
                output += activity[0][i].PadRight(23) + " " + activity[1][i] + '\n';
            }
            output = string.Concat(BLOCK, output, BLOCK);
            await ctx.RespondAsync(output);
        }

        [Command("top"), Description("Get the top 15 exp gainz")]
        public async Task Top(CommandContext ctx)
        {
            string output = string.Empty;
            await ctx.TriggerTypingAsync();
            try
            {
                string[][] top = DAL.GetTopTen();
                output += "Top 15 gainers this month!" + "\n\n";
                for (int i = 0; i < 15; i++)
                {
                    output += top[0][i].PadRight(3) + " " + top[1][i].PadRight(15) + " " + top[2][i] + '\n';
                }
                output = string.Concat(BLOCK, output, BLOCK);
            }
            catch (Exception)
            {
                output = "There was an error performing this action.";
            }
            await ctx.RespondAsync(output);
        }

        [Command("compare"), Description("Compare two players' stats")]
        public async Task Compare(CommandContext ctx, params string[] UserName)
        {
            string output = string.Empty;
            await ctx.TriggerTypingAsync();
            try
            {
                string user = string.Join("+", UserName);
                string[] userName = user.Split(',');
                string[] lamdas = new string[DAL.skills.Length];
                int wins1 = 0;
                int wins2 = 0;

                Int32[][] playerData1 = DAL.GetStats(userName[0]);

                Int32[][] playerData2 = DAL.GetStats(userName[1]);

                for (int i = 0; i < DAL.skills.Length; i++)
                {
                    if (playerData1[2][i] == playerData2[2][i])
                    {
                        lamdas[i] = "==";
                    }
                    else if (playerData1[2][i] > playerData2[2][i])
                    {
                        lamdas[i] = "<=";
                        wins1++;
                    }
                    else
                    {
                        lamdas[i] = "=>";
                        wins2++;
                    }
                }

                //header of Skills table

                output += userName[0].PadLeft(24).Replace('+', ' ') + userName[1].PadLeft(32).Replace('+', ' ') + '\n';
                output += "Skills".PadRight(10) + "Experience".PadLeft(16) +
                    "Level".PadLeft(11) + "Experience".PadLeft(19) + '\n';
                //populate rolls of skills table
                for (int i = 0; i < DAL.skills.Length; i++)
                {
                    output += DAL.skills[i].PadRight(15) +
                        playerData1[2][i].ToString("###,###,###").PadRight(14) +
                        playerData1[1][i].ToString().PadRight(6) +
                        lamdas[i].PadRight(4) +
                        playerData2[1][i].ToString().PadRight(6) +
                        playerData2[2][i].ToString("###,###,###").PadRight(14) + '\n';
                };
                output += userName[0].Replace('+', ' ') + " won " + wins1 + " times." + '\n';
                output += userName[1].Replace('+', ' ').Remove(0, 1) + " won " + wins2 + " times." + '\n';
                output = string.Concat(BLOCK, output, BLOCK);

            }
            catch (Exception)
            {
                output = "There was an error performing this action.";
            }
            await ctx.RespondAsync(output);
        }

        [Command("gainz"), Description("Get gainz for specified user")]
        public async Task Gainz(CommandContext ctx, params string[] UserName)
        {
            string user = string.Join("+", UserName);
            string output = string.Empty;
            await ctx.TriggerTypingAsync();
            try
            {
                string[][] gainers = DAL.GetGainz(user);
                string[] newValues = new string[DAL.skills.Length + 1];
                newValues[0] = user;                                // set the prepended value
                Array.Copy(DAL.skills, 0, newValues, 1, DAL.skills.Length);
                for (int i = 0; i < gainers[0].Length; i++)
                {
                    int j = gainers[0][i].LastIndexOf("+");
                    if (j > 0)
                        gainers[0][i] = gainers[0][i].Substring(0, j); // or index + 1 to keep plus
                }
                for (int i = 0; i < gainers[0].Length; i++)
                {
                    int j = gainers[1][i].LastIndexOf("+");
                    if (j > 0)
                        gainers[1][i] = gainers[1][i].Substring(0, j); // or index + 1 to keep plus
                }
                for (int i = 0; i < gainers[0].Length; i++)
                {
                    int j = gainers[2][i].LastIndexOf("+");
                    if (j > 0)
                        gainers[2][i] = gainers[2][i].Substring(0, j); // or index + 1 to keep plus
                }
                for (int i = 0; i < newValues.Length; i++)
                {
                    output += newValues[i].PadRight(15) +
                        gainers[0][i].PadLeft(12) +
                        gainers[1][i].PadLeft(12) +
                        gainers[2][i].PadLeft(12) + '\n';
                }
                output = string.Concat(BLOCK, output, BLOCK);
            }
            catch
            {
                output = "No results could be found. Make sure RuneClan is tracking you, to use this command.";
            }

            await ctx.RespondAsync(output);
        }

        [Command("alch"), Description("")]
        public async Task Alch(CommandContext ctx, string page)
        {
            string output = string.Empty;
            string[][] topalchs = DAL.GetAlchables(page);
            for (int i = 0; i < topalchs[0].Length; i++)
            {
                output += topalchs[0][i].PadRight(35) + topalchs[1][i].PadRight(15) + topalchs[2][i] + '\n';
            }
            output = string.Concat(BLOCK, output, BLOCK);
            await ctx.RespondAsync(output);
        }

        [Command("compz"), Description("Compare the gainz of two to four users")]
        public async Task Compz(CommandContext ctx, params string[] UserName)
        {
            string output = string.Empty;
            await ctx.TriggerTypingAsync();
            string user = string.Join("+", UserName);
            string[] userName = user.Split(',');
            try
            {
                string[][] gainers1 = DAL.GetGainz(userName[0]);
                string[][] gainers2 = DAL.GetGainz(userName[1]);
                string[][] gainers3 = new string[3][];
                string[][] gainers4 = new string[3][];
                if (UserName.Length > 2)
                {
                    gainers3 = DAL.GetGainz(userName[2]);
                }
                if (UserName.Length > 3)
                {
                    gainers4 = DAL.GetGainz(userName[3]);
                }

                string[] newValues = new string[DAL.skills.Length + 1];
                newValues[0] = " ";
                Array.Copy(DAL.skills, 0, newValues, 1, DAL.skills.Length);

                output += " ".PadRight(12) +
                    userName[0].PadLeft(12).Replace('+', ' ') +
                    userName[1].PadLeft(12).Replace('+', ' ');
                if (UserName.Length > 2)
                {
                    output += userName[2].PadLeft(12).Replace('+', ' ');
                }
                if (UserName.Length > 3)
                {
                    output += userName[3].PadLeft(12).Replace('+', ' ');
                }
                output += '\n';

                for (int i = 0; i < gainers1[0].Length; i++)
                {
                    int j = gainers1[0][i].LastIndexOf("+");
                    if (j > 0)
                        gainers1[0][i] = gainers1[0][i].Substring(0, j); // or index + 1 to keep plus
                }
                for (int i = 0; i < gainers2[0].Length; i++)
                {
                    int j = gainers2[0][i].LastIndexOf("+");
                    if (j > 0)
                        gainers2[0][i] = gainers2[0][i].Substring(0, j); // or index + 1 to keep plus
                }
                if (UserName.Length > 2)
                {
                    for (int i = 0; i < gainers3[0].Length; i++)
                    {
                        int j = gainers3[0][i].LastIndexOf("+");
                        if (j > 0)
                            gainers3[0][i] = gainers3[0][i].Substring(0, j); // or index + 1 to keep plus
                    }
                }
                if (UserName.Length > 3)
                {
                    for (int i = 0; i < gainers4[0].Length; i++)
                    {
                        int j = gainers4[0][i].LastIndexOf("+");
                        if (j > 0)
                            gainers4[0][i] = gainers4[0][i].Substring(0, j); // or index + 1 to keep plus
                    }
                }

                for (int i = 0; i < newValues.Length; i++)
                {
                    output += newValues[i].PadRight(15) +
                        gainers1[0][i].PadLeft(12).Replace("Today", " ") +
                        gainers2[0][i].PadLeft(12).Replace("Today", " ");
                    if (UserName.Length > 2)
                    {
                        output += gainers3[0][i].PadLeft(12).Replace("Today", " ");
                    }
                    if (UserName.Length > 3)
                    {
                        output += gainers4[0][i].PadLeft(12).Replace("Today", " ");
                    }
                    output += '\n';
                }
                output = string.Concat(BLOCK, output, BLOCK);
            }
            catch (Exception)
            {
                output = "There was an error comparing theese two users.";
            }

            await ctx.RespondAsync(output);
        }

        [Command("achievements"), Aliases("a"), Description("Get Casual Oasis Clan achievements")]
        public async Task Achievements(CommandContext ctx)
        {
            string output = string.Empty;
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load("http://casualoasis.clanwebsite.com/");
                HtmlNode heading = doc.DocumentNode.SelectNodes("//div[@class='custompanel_content']").First();
                foreach (HtmlNode data in heading.SelectNodes("span"))
                {
                    output += data.InnerText + '\n';
                }
                output = string.Concat(BLOCK, output, BLOCK);
            }
            catch (Exception)
            {
                output = "There was an error loading achievements";
            }
            await ctx.RespondAsync(output);
        }

        [Command("schedule"), Description("Get Casual Oasis Clan schedule")]
        public async Task Schedule(CommandContext ctx)
        {
            string output = string.Empty;
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load("http://casualoasis.clanwebsite.com/");
                HtmlNode heading = doc.DocumentNode.SelectNodes("//div[@class='custompanel_content']").Last();
                //output = heading.InnerText;
                foreach (HtmlNode data in heading.SelectNodes("span | b"))
                {
                    output += data.InnerText + '\n';
                }
                output = string.Concat(BLOCK, output, BLOCK);
            }
            catch (Exception)
            {
                output = "There was an error loading the schedule";
            }
            await ctx.RespondAsync(output);
        }

        [Command("month"), Description("Monthly reminder for D&D's")]
        public async Task Month(CommandContext ctx)
        {
            var builder = new DiscordEmbedBuilder()
                .WithTitle("Don't forget to do your monthlies!")
                .WithUrl("http://runescape.wikia.com/wiki/Repeatable_events")
                .WithColor(new DiscordColor(0x87A2EF))
                .WithDescription("\n\"Troll Invasion\" in Burthorpe for reward book worth up to 77k exp in any skill!\nhttp://runescape.wikia.com/wiki/Troll_Invasion\n" +
                        "\n\"God Statues\" located all over Gielinor for construction, prayer or slayer exp!\nhttp://runescape.wikia.com/wiki/God_Statues\n" +
                        "\n\"Giant Oyster\" located on Tutorial Island (after Beneath Cursed Tides quest) for free clue scroll reward!\nhttp://runescape.wikia.com/wiki/Giant_Oyster\n" +
                        "\n\"Premier Club Vault\" located in Varrock G/E for awesome free rewards!\nhttp://runescape.wikia.com/wiki/Premier_Club_Vault\n");

            await ctx.RespondAsync(embed: builder.Build());
        }

        [Command("week"), Description("Weekly reminder for D&D's")]
        public async Task Week(CommandContext ctx)
        {
            var builder = new DiscordEmbedBuilder()
               .WithTitle("Don't forget to do your weeklies!")
               .WithUrl("http://runescape.wikia.com/wiki/Repeatable_events")
               .WithColor(new DiscordColor(0x87A2EF))
               .WithDescription("\n\"Familiarisation\" find the small obelisk for triple charm drop rates!\nhttp://runescape.wikia.com/wiki/Familiarisation\n" +
               "\n\"Aquarium\" go to your POH aguariium for free clue scroll and other valuable items!\nhttp://runescape.wikia.com/wiki/Aquarium\n" +
               "\n\"Skeletal Horror\" talk to the Odd Old Man for some easy slayer exp!\nhttp://runescape.wikia.com/wiki/Skeletal_horror\n" +
               "\n\"Rush of Blood\" head to Prifddinas for some awesome rewards!\nhttp://runescape.wikia.com/wiki/Rush_of_Blood\n" +
               "\n\"Circus\" head to Balthazar Beauregard for some easy exp and awesome outfits!\nhttp://runescape.wikia.com/wiki/Balthazar_Beauregard%27s_Big_Top_Bonanza\n" +
               "\n\"Penguins\" locate all those devious penguins for some awesome exp rewards!\nhttp://runescape.wikia.com/wiki/Penguin_Hide_and_Seek\n");
            //"\n\"Tears of Guthix\"\nhttp://runescape.wikia.com/wiki/Tears_of_Guthix\n" +
            await ctx.RespondAsync(embed: builder.Build());
        }

        [Command("date"), Description("Get's the current Date and time")]
        public async Task Date(CommandContext ctx)
        {
            DateTime date = DateTime.UtcNow;
            await ctx.RespondAsync(date.ToString());
        }       

        [Command("clancomp"), Description("Clan competition progress. Select page with 1-5 or 50 for top 50 in competition")]
        public async Task Clancomp(CommandContext ctx, int page = 1)
        {
            string output = string.Empty;
            string URL = "http://casualoasis.com/";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(URL);
            string[][] stats = new string[3][];
            stats[0] = new string[51];//rank
            stats[1] = new string[51];//player
            stats[2] = new string[51];//xp gained
            int j = 0;
            string title = doc.DocumentNode.SelectSingleNode("body").FirstChild.InnerText;
            //string updated = doc.DocumentNode.SelectNodes("p").First().InnerText;

            var table = doc.DocumentNode.SelectSingleNode("//table");

            foreach (var row in table.SelectNodes("tr"))
            {
                if (j==51)
                {
                    break;
                }
                stats[0][j] = row.SelectNodes("th|td")[0].InnerText.Replace("&nbsp;", "");
                stats[1][j] = row.SelectNodes("th|td")[1].InnerText.Replace("&nbsp;", "");
                stats[2][j] = row.SelectNodes("th|td")[2].InnerText.Replace("&nbsp;", "");
                j++;
            }
            int q = 0;
            switch (page)
            {
                case 1:
                    q = 0;
                    page = 11;
                    break;
                case 2:
                    q = 11;
                    page = 21;
                    break;
                case 3:
                    q = 21;
                    page = 31;
                    break;
                case 4:
                    q = 31;
                    page = 41;
                    break;
                case 5:
                    q = 41;
                    page = 51;
                    break;
                case 50:
                    q = 0;
                    page = 51;
                    break;
                default:
                    page = 11;
                    break;
            }
            for (int i = q; i < page; i++)
            {
                output += stats[0][i].PadRight(5) +
                    stats[1][i].PadRight(14) +
                    stats[2][i].PadLeft(12) + '\n';
            }
            var builder = new DiscordEmbedBuilder()
                .WithTitle(title)
                .WithUrl(URL)
                .WithColor(new DiscordColor(0x87A2EF))
                .WithDescription(/*updated + '\n' + */string.Concat(BLOCK, output, BLOCK));
            await ctx.RespondAsync(embed: builder.Build());
        }
        [Command("prune"), Aliases("p"), Description("Delete x number of messages")]
        public async Task Prune(CommandContext ctx, int count)
        {
            count++;
            if (count < 1)
                return;
            if (count > 100)
                count = 100;
            try
            {
                await ctx.Channel.DeleteMessagesAsync(await ctx.Channel.GetMessagesAsync(count));
            }
            catch (Exception ex)
            {
                string output = string.Empty;
                if (ex.Message.Contains("403"))
                {
                    output = "The bot does not have priveleges for this command!";
                }
                else
                {
                    output = "Something strange went wrong here.";
                }
                await ctx.RespondAsync(output);
                throw;
            }
        }

    }
}
