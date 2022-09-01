// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using System;

namespace Icarus.Commands
{
    public class ActivityContent
    {
        public static string PendingActivityImage(string arg)
        {
            if (arg == "King's Fall")           { return "https://i.postimg.cc/pLqtkHbD/King-s-Fall.png"; }
            if (arg == "Vow of the Disciple")   { return "https://i.postimg.cc/y6ttTyQY/Vow-Of-The-Disciple.png"; }
            if (arg == "Vault of Glass")        { return "https://i.postimg.cc/XYFs7KxX/Vault-Of-Glass.png"; }
            if (arg == "Deep Stone Crypt")      { return "https://i.postimg.cc/tgNDFXKj/Deep-Stone-Crypt.png"; }
            if (arg == "Garden of Salvation")   { return "https://i.postimg.cc/ZRYHcGmy/Garden-Of-Salvation.png"; }
            if (arg == "The Last Wish")         { return "https://i.postimg.cc/W39nf5pY/LastWish.png"; }
            if (arg == "Ordeal Grandmaster")    { return "https://i.postimg.cc/xTBs2pvW/Strike.png"; }
            if (arg == "Duality")               { return "https://i.postimg.cc/5tVnHHkg/Dungeon.png"; }
            if (arg == "Grasp of Avarice")      { return "https://i.postimg.cc/5tVnHHkg/Dungeon.png"; }
            if (arg == "The Prophecy")          { return "https://i.postimg.cc/5tVnHHkg/Dungeon.png"; }
            if (arg == "Pit of Heresy")         { return "https://i.postimg.cc/5tVnHHkg/Dungeon.png"; }
            if (arg == "Shattered Throne")      { return "https://i.postimg.cc/5tVnHHkg/Dungeon.png"; }
            return "https://i.postimg.cc/J4XgYHv3/Wellspring.png";
        }

        public static string PendingActivityTime(string arg)
        {
            char[] separators = { '.', '-' };
            string[] parsedTimeString = arg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string modifiedTimeString = string.Empty;
            modifiedTimeString += parsedTimeString[0] + " ";

            switch (parsedTimeString[1])
            {
                case "01": modifiedTimeString += "January, ";   break;
                case "02": modifiedTimeString += "February, ";  break;
                case "03": modifiedTimeString += "March, ";     break;
                case "04": modifiedTimeString += "Arpil, ";     break;
                case "05": modifiedTimeString += "May, ";       break;
                case "06": modifiedTimeString += "June, ";      break;
                case "07": modifiedTimeString += "July, ";      break;
                case "08": modifiedTimeString += "August, ";    break;
                case "09": modifiedTimeString += "September, "; break;
                case "10": modifiedTimeString += "October, ";   break;
                case "11": modifiedTimeString += "November, ";  break;
                case "12": modifiedTimeString += "December, ";  break;
                default: modifiedTimeString = string.Empty;     break;
            }

            modifiedTimeString += parsedTimeString[2];
            return modifiedTimeString;
        }
    }
}