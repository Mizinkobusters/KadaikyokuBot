using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KadaikyokuBot
{
    public class KadaikyokuCmd : ModuleBase
    {
        private List<Fumen> fumenList = Fumen.fumenList;
        private const int KADAIKYOKU_COUNT = GakkyokuUtil.KADAIKYOKU_COUNT;
        private List<Fumen> extractedKadaikyokuList = new List<Fumen>();
        private List<Fumen> selectedKadaikyokuList = new List<Fumen>();

        private string[] titleArray = new string[KADAIKYOKU_COUNT];
        private string[] artistArray = new string[KADAIKYOKU_COUNT];
        private Gakkyoku.Diff[] diffArray = new Gakkyoku.Diff[KADAIKYOKU_COUNT];
        private string[] fieldList = new string[KADAIKYOKU_COUNT];

        private string[] conditions;
        private KadaikyokuCondition condition;

        private const double DEFAULT_MIN_LEVEL = 1.0;
        private const double DEFAULT_MAX_LEVEL = 15.4;

        [Command("kadaikyoku")]
        [Alias("kadai")]
        public async Task Reply([Remainder]string args = null)
        {
            if (args != null)
            {
                conditions = GakkyokuUtil.splitArguments(args);
                condition = new KadaikyokuCondition(0.0, 0.0);

                for (int i = 0; i < conditions.Length; i++)
                {
                    if (i == 0)
                    {
                        condition.minLevel = double.TryParse(conditions[i], out var minLevel) ? minLevel : DEFAULT_MIN_LEVEL;
                    }
                    if (i == 1)
                    {
                        condition.maxLevel = double.TryParse(conditions[i], out var maxLevel) ? maxLevel : DEFAULT_MAX_LEVEL;
                    }
                }

                if (condition.maxLevel < condition.minLevel)
                {
                    condition.maxLevel = condition.minLevel;
                }

                if (condition.minLevel < DEFAULT_MIN_LEVEL)
                {
                    condition.minLevel = DEFAULT_MIN_LEVEL;
                }

                if (condition.maxLevel > DEFAULT_MAX_LEVEL)
                {
                    condition.maxLevel = DEFAULT_MAX_LEVEL;
                }

                extractedKadaikyokuList = GakkyokuUtil.extractKadaikyoku(fumenList, condition);
            }
            else
            {
                extractedKadaikyokuList = fumenList;
            }

            selectedKadaikyokuList = GakkyokuUtil.selectKadaikyoku(extractedKadaikyokuList);

            for (int i = 0; i < KADAIKYOKU_COUNT; i++) 
            {
                titleArray[i] = selectedKadaikyokuList[i].rootobject.meta.title;
                artistArray[i] = selectedKadaikyokuList[i].rootobject.meta.artist;
                diffArray[i] = selectedKadaikyokuList[i].diff;

                if (diffArray[i].is_const_unknown)
                {
                    fieldList[i] = $"{artistArray[i]} | {GakkyokuUtil.diffToString(selectedKadaikyokuList[i].rootobject, selectedKadaikyokuList[i].diff)} (*{diffArray[i].level}*)";
                } 
                else
                {
                    fieldList[i] = $"{artistArray[i]} | {GakkyokuUtil.diffToString(selectedKadaikyokuList[i].rootobject, selectedKadaikyokuList[i].diff)} ({diffArray[i].constant:F1})";
                }
                
            }

            // 課題曲を表示する用のEmbed
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder
            .WithColor(GakkyokuUtil.getRandomColor())
            .WithDescription("対象曲数: " + extractedKadaikyokuList.Count)
            .WithTitle("本日の課題曲")
            .AddField(titleArray[0], fieldList[0])
            .AddField(titleArray[1], fieldList[1])
            .AddField(titleArray[2], fieldList[2])
            .WithFooter(Context.Message.Author.Username, Context.Message.Author.GetAvatarUrl())
            .WithCurrentTimestamp();

            await ReplyAsync("課題曲を生成しています... (ꈍ◡ꈍ)");
            await ReplyAsync(embed: embedBuilder.Build());

        }
    }
}