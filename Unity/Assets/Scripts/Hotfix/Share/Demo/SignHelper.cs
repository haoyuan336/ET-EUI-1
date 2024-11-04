using System;
using System.Reflection;

namespace ET
{
    public static class SignHelper
    {
        public static string GetSign(MessageObject messageObject, long time)
        {
            Type type = messageObject.GetType();

            messageObject.SignTimeStamp = time;

            PropertyInfo[] propertyInfos = type.GetProperties();

            string endStr = "";

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name == "Sign" || propertyInfo.Name == "RpcId")
                {
                    continue;
                }

                endStr += propertyInfo.Name + "=" + propertyInfo.GetValue(messageObject) + "&";
            }

            endStr += "12dwqefqwr2323";

            string sign = MD5Helper.StringMD5(endStr);

            return sign;
        }

        public static bool CheckSign(MessageObject messageObject)
        {
            string sign = messageObject.Sign;

            long timeStamp = messageObject.SignTimeStamp;

            string checkSign = GetSign(messageObject, timeStamp);

            long signTimeStamp = messageObject.SignTimeStamp;

            long nowTime = TimeInfo.Instance.ServerNow();

            if (nowTime - signTimeStamp > 1000)
            {
                Log.Error("sign time out");

                return false;
            }

            bool result = string.Equals(sign, checkSign);

            if (!result)
            {
                Log.Warning("check error");
            }

            return result;
        }
    }
}