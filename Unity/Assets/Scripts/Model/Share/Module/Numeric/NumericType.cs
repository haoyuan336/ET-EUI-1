namespace ET
{
    public enum WordAttributeType
    {
        BaseAttribute = 1, //基础属性
        SpecicalAttribute = 2, //特殊属性
        ElementAttribute = 3, //元素属性
    }

    // 这个可弄个配置表生成
    public static class NumericType
    {
        // public const int Max = 10000;
        //
        // public const int Speed = 1000;
        // public const int SpeedBase = Speed * 10 + 1;
        // public const int SpeedAdd = Speed * 10 + 2;
        // public const int SpeedPct = Speed * 10 + 3;
        // public const int SpeedFinalAdd = Speed * 10 + 4;
        // public const int SpeedFinalPct = Speed * 10 + 5;
        //
        // public const int Hp = 1001;
        // public const int HpBase = Hp * 10 + 1; 
        //
        // public const int MaxHp = 1002;
        // public const int MaxHpBase = MaxHp * 10 + 1;
        // public const int MaxHpAdd = MaxHp * 10 + 2;
        // public const int MaxHpPct = MaxHp * 10 + 3;
        // public const int MaxHpFinalAdd = MaxHp * 10 + 4;
        // public const int MaxHpFinalPct = MaxHp * 10 + 5;
        //
        // public const int AOI = 1003;
        // public const int AOIBase = AOI * 10 + 1;
        // public const int AOIAdd = AOI * 10 + 2;
        // public const int AOIPct = AOI * 10 + 3;
        // public const int AOIFinalAdd = AOI * 10 + 4;
        // public const int AOIFinalPct = AOI * 10 + 5;

        public const int Hp = 101;
        public const int Attack = 102;
        public const int BaoJiLv = 103;
        public const int BaoShangJiaCheng = 104;
        public const int MingZhongLv = 105;
        public const int ShanBiLv = 106;
        public const int XiXueLv = 107;
        public const int XiXueJiaCheng = 108;
        public const int KangBaoJiaLv = 109;
        public const int KangBaoJiaCheng = 110;
        public const int KangXiXueLv = 111;
        public const int KangXiXueJiaCheng = 112;
        public const int KangGeDangLv = 113;
        public const int KangGeDangJiaCheng = 114;
        public const int DuXiQiangDu = 115;
        public const int DuXiKangXing = 116;
        public const int HuoXiKangXing = 117;
        public const int DianXiKangXing = 118;
        public const int AnXiQiangDu = 119;
        public const int AnXiKangXing = 120;
        public const int XueXiKangXing = 121;
    }
}