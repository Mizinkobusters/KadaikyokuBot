using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KadaikyokuBot
{
    public class Gakkyoku
    {
        // 楽曲データ格納用配列
        public static List<Rootobject> gakkyokuList { get; set; } = new List<Rootobject>();

        [DataContract]
        public class Rootobject
        {
            [DataMember(Name = "meta")]
            public Meta meta { get; set; }
            [DataMember(Name = "data")]
            public Data data { get; set; }
        }

        [DataContract]
        public class Meta
        {
            [DataMember(Name = "id")]
            public string id { get; set; }
            [DataMember(Name = "title")]
            public string title { get; set; }
            [DataMember(Name = "genre")]
            public string genre { get; set; }
            [DataMember(Name = "artist")]
            public string artist { get; set; }
            [DataMember(Name = "release")]
            public string release { get; set; }
            [DataMember(Name = "bpm")]
            public int bpm { get; set; }
        }

        [DataContract]
        public class Data
        {
            [DataMember(Name = "BAS")]
            public Diff bas { get; set; }
            [DataMember(Name = "ADV")]
            public Diff adv { get; set; }
            [DataMember(Name = "EXP")]
            public Diff exp { get; set; }
            [DataMember(Name = "MAS")]
            public Diff mas { get; set; }
            [DataMember(Name = "ULT")]
            public Diff ult { get; set; }
            [DataMember(Name = "WE")]
            public Diff we { get; set; }
        }

        [DataContract]
        public class Diff
        {
            [DataMember(Name = "level")]
            public double level { get; set; }
            [DataMember(Name = "const")]
            public double constant { get; set; }
            [DataMember(Name = "maxcombo")]
            public int maxcombo { get; set; }
            [DataMember(Name = "is_const_unknown")]
            public bool is_const_unknown { get; set; }
        }
    }
}
