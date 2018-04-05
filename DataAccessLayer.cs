using HtmlAgilityPack;
using System;
using System.Net;

namespace LukeBot2
{
    public class DataAccessLayer
    {
        public static myGainz Skills = new myGainz();
        private static Random getrandom = new Random();

        public int GetRandomNumber(int min, int max)
        {
            int output = getrandom.Next(min, max);
            return output;
        }
        public Int32[][] GetStats(string userName)
        {
            WebClient wc = new WebClient();
            string url1 = "http://services.runescape.com/m=hiscore/index_lite.ws?player=" + userName;
            byte[] raw1 = wc.DownloadData(url1);
            string webData = System.Text.Encoding.UTF8.GetString(raw1);
            Int32[][] stats = new Int32[3][];
            stats[0] = new Int32[skills.Length];
            stats[1] = new Int32[skills.Length];
            stats[2] = new Int32[skills.Length];
            //define arrays and begin splitting webData
            string[] levels = webData.Split('\n');
            //split data into an array for each value
            for (int f = 0; f < skills.Length; f++)
            {
                string[] tempArray = levels[f].Split(',');
                stats[0][f] = Convert.ToInt32(tempArray[0]);
                stats[1][f] = Convert.ToInt32(tempArray[1]);
                stats[2][f] = Convert.ToInt32(tempArray[2]);
            }
            return stats;
        }
        public string[] skills = {
                "Overall","Attack" , "Defence" ,"Strength" ,"Constitution" ,"Ranged" , "Prayer" ,"Magic" ,
                "Cooking" , "Woodcutting" ,"Fletching" ,"Fishing" , "Firemaking" ,"Crafting" , "Smithing" , "Mining" ,
                "Herblore" , "Agility" , "Thieving" ,"Slayer" , "Farming" ,"Runecrafting" , "Hunter" ,"Construction" ,
                "Summoning" ,"Duneoneering" , "Divination" ,"Invention"
            };
        public string[] GetTops(string user)
        {
            string[] MonthlyGain = new string[3];
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://services.runescape.com/m=hiscore/a=13/ranking?table=0&category_type=0&time_filter=2&date=1517592084834&user=" + user);
            foreach (HtmlNode tr in doc.DocumentNode.SelectNodes("//tr[contains(@class, 'hover')]"))
            {
                MonthlyGain[0] = tr.SelectNodes("td")[0].InnerText.Replace("\n", string.Empty);
                MonthlyGain[1] = tr.SelectNodes("td")[1].InnerText.Replace("\n", string.Empty);
                MonthlyGain[2] = tr.SelectNodes("td")[2].InnerText.Replace("\n", string.Empty);
            }
            return MonthlyGain;
        }
        public string[][] GetVStats(string userName)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.runeclan.com/user/" + userName);
            string[][] stats = new string[3][];
            stats[0] = new string[skills.Length + 1];
            stats[1] = new string[skills.Length + 1];
            stats[2] = new string[skills.Length + 1];
            int z = 0;
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    stats[0][z] = row.SelectNodes("th|td")[1].InnerText;                             //Level
                    stats[1][z] = row.SelectNodes("th|td")[2].InnerText.Replace("XP", "Experience");                             //XP
                    stats[2][z] = row.SelectNodes("th|td")[3].InnerText;     //Rank
                    z++;
                }
            }
            return stats;
        }
        public string[][] GetTopTen()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://services.runescape.com/m=hiscore/a=13/ranking?category_type=0&table=0&time_filter=2&page=1");
            string[][] highscores = new string[3][];
            highscores[0] = new string[25];
            highscores[1] = new string[25];
            highscores[2] = new string[25];
            int z = 0;

            foreach (HtmlNode td in doc.DocumentNode.SelectNodes("//td[contains(@class, 'col1 align')]"))
            {
                highscores[0][z] = td.InnerText.Replace("\n", string.Empty);
                z++;
            }
            z = 0;
            foreach (HtmlNode td in doc.DocumentNode.SelectNodes("//td[contains(@class, 'col2')]"))
            {
                highscores[1][z] = td.InnerText.Replace("\n", string.Empty);
                z++;
            }
            z = 0;
            foreach (HtmlNode td in doc.DocumentNode.SelectNodes("//td[contains(@class, 'col4 align')]"))
            {
                highscores[2][z] = td.InnerText.Replace("\n", string.Empty);
                z++;
            }
            return highscores;
        }
        public string[][] GetActivity(string userName)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.runeclan.com/user/" + userName);
            string[][] stats = new string[2][];
            stats[0] = new string[15];
            stats[1] = new string[15];
            int z = 0;
            foreach (HtmlNode div in doc.DocumentNode.SelectNodes("//div[contains(@class,'xp_tracker_activity_l')]"))
            {
                stats[0][z] = div.InnerText;
                z++;
            }
            z = 0;
            foreach (HtmlNode dive in doc.DocumentNode.SelectNodes("//div[contains(@class,'xp_tracker_activity_r')]"))
            {
                stats[1][z] = dive.InnerText;
                z++;
            }
            return stats;
        }
        public string[][] GetGainz(string userName)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.runeclan.com/user/" + userName);
            string[][] stats = new string[3][];
            stats[0] = new string[skills.Length + 1];
            stats[1] = new string[skills.Length + 1];
            stats[2] = new string[skills.Length + 1];
            int z = 0;
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    stats[0][z] = row.SelectNodes("th|td")[4].InnerText.Replace("&nbsp;", "");                             //today
                    stats[1][z] = row.SelectNodes("th|td")[5].InnerText.Replace("&nbsp;", "");                             //yesterday
                    stats[2][z] = row.SelectNodes("th|td")[6].InnerText.Replace("&nbsp;", "").Replace("DXP Wknd", "");     //week
                    z++;
                }
            }
            return stats;
        }
        public string[][] GetAlchables(string page)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.grandexchangewatch.com/item-db/alchemy/?query=&sort=high&dir=desc&show=both&min=&max=&start=" + page);
            string[][] topalchs = new string[3][];
            topalchs[0] = new string[16];//item
            topalchs[1] = new string[16];//price
            topalchs[2] = new string[16];//alch profit
            int z = 0;
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table[contains(@id,'item-list')]"))
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    if (z == 16)
                    {
                        break;
                    }
                    topalchs[0][z] = row.SelectNodes("th|td")[1].InnerText.Replace("&#039;", "'").Replace("Price", "Item");//item
                    topalchs[1][z] = row.SelectNodes("th|td")[2].InnerText.Replace("&#039;", "'").Replace("High Alch Profit", "Cost");//price
                    topalchs[2][z] = row.SelectNodes("th|td")[3].InnerText.Replace("&#039;", "'").Replace("Low Alch Profit", "High Alch Profit");//profit                            
                    z++;
                }
            }
            return topalchs;
        }
    }
}
