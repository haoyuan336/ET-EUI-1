namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误

        // 110000以下的错误请看ErrorCore.cs

        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        public const int Passwd_Error = 200001;

        public const int Not_Found_Account = 200002;

        public const int SceneType_Error = 200003;

        public const int Not_Found_HeroCard = 200004;

        public const int Sign_Error = 200005; //签名错误

        public const int Sign_TimeOut = 200006; //签名时间超时
    }
}