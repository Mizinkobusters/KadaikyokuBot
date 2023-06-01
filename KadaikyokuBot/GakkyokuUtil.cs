using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using static System.Net.Mime.MediaTypeNames;

namespace KadaikyokuBot
{
    public sealed class GakkyokuUtil
    {
        /// <summary>
        /// 課題曲に関するUtility class
        /// </summary>

        private GakkyokuUtil() { }

        // 課題曲生成数(1クレジット想定で3曲生成)
        public const int KADAIKYOKU_COUNT = 3;
        // 難易度の色コードを格納した配列
        private static Color[] colorCodes =
        {
            0x7CFC00,   // BASIC_COLOR
            0xFFA500,   // ADVANCED_COLOR
            0xFF0000,   // EXPERT_COLOR
            0x9932CC,   // MASTER_COLOR
            0xDC143C,   // ULTIMA_COLOR
            0x000000,   // WORLDSEND_COLOR
        };

        private static Random random = new Random();
        private const string PATTERN = @"[0-9]+[.]?[0-9]|[0-9]+";

        // Discord Embedで使うランダムなカラーを返却する関数
        public static Color getRandomColor()
        {
            return (uint)random.Next(0x000000, 0xffffff);
        }

        public static string[] splitArguments(string args)
        {
            return args.Split(new string[] { " ", "　" }, StringSplitOptions.RemoveEmptyEntries);
        }

        // 全曲から指定された条件の楽曲を抽出する関数
        public static List<Fumen> extractKadaikyoku(List<Fumen> fumenList, KadaikyokuCondition condition)
        {
            List<Fumen> extractedKadaikyoku = new List<Fumen>();
            for (int i = 0; i < fumenList.Count; i++)
            {
                if (condition.minLevel <= fumenList[i].diff.constant
                    && fumenList[i].diff.constant <= condition.maxLevel)
                {
                    extractedKadaikyoku.Add(fumenList[i]);
                }
            }
            
            return extractedKadaikyoku.OrderBy(v => Guid.NewGuid()).ToList();
        }

        // WE含む全曲から課題曲を選出する関数
        public static List<Fumen> selectKadaikyoku(List<Fumen> fumenList)
        {
            return fumenList.OrderBy(v => Guid.NewGuid()).ToList();
        }

        public static string diffToString (Gakkyoku.Rootobject rootobject, Gakkyoku.Diff diff)
        {
            string s = "Unknown Difficulty";
            if (rootobject.data.bas == diff)
            {
                s = "BASIC";
            } 
            else if (rootobject.data.adv == diff)
            {
                s = "ADVANCED";
            }
            else if (rootobject.data.exp == diff)
            {
                s = "EXPERT";
            }
            else if (rootobject.data.mas == diff)
            {
                s = "MASTER";
            }
            else if (rootobject.data.ult == diff)
            {
                s = "ULTIMA";
            }
            else if (rootobject.data.we == diff)
            {
                s = "WORLD'S END";
            }

            return s;
        }
    }
}
