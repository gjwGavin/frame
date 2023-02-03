using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using JWT.Exceptions;
using static WebApplication1.Tool.ConfigTool;

namespace WebApplication1.Aop.Tool
{
    public class TokenTool
    {
        public static string TokenKey = "WWF3bGlo55qEVkhE6aG555uu";

        public class TokenInfo
        {

            public TokenInfo() { }
            /// <summary>
            /// jwt签发者
            /// </summary>
            public string iss { set; get; } = "签发者信息";
            /// <summary>
            /// jwt所面向的用户
            /// </summary>
            public string aud { set; get; } = "面向的用户";
            /// <summary>
            /// 接收jwt的一方
            /// </summary>
            public string sub { set; get; } = "接受jwt的";
            /// <summary>
            /// jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
            /// </summary>
            public string jti { set; get; } = Guid.NewGuid().ToString();
            /// <summary>
            /// 用户ID
            /// </summary>
            public string UserID { set; get; }

        }

        public static string SaveHeader(TokenInfo t)
        {

            IDictionary<string, object> payload = new Dictionary<string, object>();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            //var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
            int time = int.Parse(GetConfigValue(ConfigField.TokenTime)) * 60;
            var exp = (DateTime.UtcNow.AddSeconds(time) - new DateTime(1970, 1, 1)).TotalSeconds;
            //拼接header头
            payload.Add("exp", exp);    //expire 指定token的生命周期。unix时间戳格式
            payload.Add("iat", DateTime.Now.ToString()); //发布时间
            payload.Add("UserId", t.UserID);
            //创建Token
            string token = CreateJwtToken(payload, TokenKey);
            return token;
        }




        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="payload">任意有效负载(必须可序列化为JSON)</param>
        /// <param name="secret">用于签名令牌的密钥</param>
        /// <param name="extraHeaders"></param>
        /// <returns>token字符串</returns>
        public static string CreateJwtToken(IDictionary<string, object> payload, string secret, IDictionary<string, object> extraHeaders = null)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
        }
        public static IDictionary<string, object> ValidateJwtToken(string token, string secret)
        {

            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm alg = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, alg);
            string str = decoder.DecodeHeader(token);
            var json = decoder.Decode(token, secret, true);
            IDictionary<string, object> dic = JsonConvert.DeserializeObject<IDictionary<string, object>>(json);
            return dic;
        }


        public static Dictionary<string, object> Validate(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm alg = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, alg);
            var json = decoder.Decode(token);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }


        /// <summary>
        /// 校验解析token
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="secret">秘钥</param>
        /// <returns>结果</returns>
        public static string ValidateJwtToken(string token, string secret, out string Message)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm alg = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, alg);
                var json = decoder.Decode(token, secret, true);
                //校验通过，返回解密后的字符串
                //进行数据库访问 对Token信息进行数据化验证 
                Message = "good";
                return json;
            }
            catch (TokenExpiredException)
            {
                //表示过期
                Message = "Token已过期";
                return "expired";
            }
            catch (SignatureVerificationException)
            {
                //表示验证不通过
                Message = "Token验证不通过";
                return "invalid";
            }
            catch (Exception)
            {
                Message = "Token其他error";
                return "error";
            }
        }



    }

}