namespace ET.Server
{
    [ChildOf(typeof(AccountComponent))]
    public class Account: Entity, IAwake
    {
        public string Passwd; //密码

        public string UserName; //用户名

        public long RegisterTime; //注册时间
    }
}