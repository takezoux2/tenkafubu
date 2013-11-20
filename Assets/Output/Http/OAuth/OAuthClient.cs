using System;
using Tenkafubu.Util;
using System.Text;
using System.Collections.Generic;
using Tenkafubu.Http;
using Tenkafubu.Concurrent;
using Tenkafubu.Concurrent.Factory;

namespace Tenkafubu.Http.OAuth
{
	public class OAuthClient : AbstractHttpClient
	{
		string token;
		string tokenSecret;
		string consumerKey;
		string consumerSecret;
		string serverUrl ;

		string OAuthVersion = "1.0";
		string SignatureMethod = "HMAC-SHA1";


		public OAuthClient (string serverUrl ,
		                    string token,string tokenSecret,
		                    string consumerKey , string consumerSecret) : base()
		{
			this.serverUrl = serverUrl;
			this.token = token;
			this.tokenSecret = tokenSecret;
			this.consumerKey = consumerKey;
			this.consumerSecret = consumerSecret;
		}


		public override Future<Response> Send(HttpMethod method,string path,byte[] body){

			var url = StringUtil.JoinUrlPath(serverUrl,path);

			var req = new Request(method.ToString(),url,body);

			SignRequest(req,method,path,body);
			ModifyRequest(req);

			var f = FutureFactory.NewFuture<Response>();
			req.OnFinish = r => {
				f.SetResult(r.response);
			};
			req.Send();
			return f;
		}

		private void SignRequest(Request req,HttpMethod method,string path,byte[] body) {
			
			var timestamp = TimeUtil.CurrentUnixTime_Sec();
			var nonce = StringUtil.GetRandomString(10);
			var getParams = StringUtil.GetGetParams(path);
			
			var oauthParams = new SortedDictionary<string,string>();
			oauthParams.Add("oauth_timestamp",timestamp.ToString());
			oauthParams.Add("oauth_nonce",nonce);
			oauthParams.Add("oauth_consumer_key",consumerKey);
			oauthParams.Add("oauth_version",OAuthVersion);
			oauthParams.Add("oauth_signature_method",SignatureMethod);
			oauthParams.Add("oauth_token",Uri.EscapeUriString(token));
			foreach(var p in getParams){
				oauthParams.Add(p.V1,p.V2);
			}
			if(body != null && body.Length > 0){
				oauthParams.Add("body_hash",Uri.EscapeUriString(HashUtil.ToMD5(body)));
			}
			
			var paramString = new StringBuilder();
			foreach(var e in oauthParams){
				paramString.Append(e.Value + "=" + e.Value + "&");
			}
			paramString.Remove(paramString.Length - 1,1);
			
			
			var baseString = new StringBuilder();
			baseString.Append(method.ToString() + "&");
			baseString.Append(Uri.EscapeUriString(StringUtil.GetPathWithoutQuery(path)) + "&");
			baseString.Append(Uri.EscapeUriString(paramString.ToString()));

			var hashPassword = consumerSecret + "&" + tokenSecret;
			var signature = HashUtil.ToHMacSHA1(baseString.ToString(),hashPassword);

			oauthParams.Add("oauth_signature",Uri.EscapeUriString(signature));

			var authHeader = new StringBuilder();
			authHeader.Append("OAuth ");
			foreach(var e in oauthParams){
				if(e.Key.StartsWith("oauth")){
					authHeader.Append(e.Key + "=\"" + e.Value + "\","); 
				}
			}
			authHeader.Remove(authHeader.Length - 1,1);

			req.SetHeader("Authorization",authHeader.ToString());

		}

		protected virtual void ModifyRequest(Request req){
			
		}
	}


}

