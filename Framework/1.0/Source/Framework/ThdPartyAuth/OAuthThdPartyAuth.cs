using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework.ThdPartyAuth
{
    public abstract class OAuthThdPartyAuth : AbstractThdPartyAuth
    {
        protected OAuthHelper oAuth;
        protected abstract string ConsumerKey { get; }
        protected abstract string ConsumerSecret { get; }
        protected abstract string RequestToken { get; }
        protected abstract string Authorize { get; }
        protected abstract string AccessToken { get; }
        protected abstract string UserInfoUrl { get; }
        public virtual string TokenSecret
        {
            get
            {
                return oAuth.TokenSecret;
            }
            set
            {
                oAuth.TokenSecret = value;
            }
        }
        protected string tokenSecret;
        public OAuthThdPartyAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager, string callbackUrl)
            : this(userManager, thdPartAuthManager, callbackUrl, "")
        {
        }
        public OAuthThdPartyAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager, string callbackUrl, string tokenSecret)
            : base(userManager, thdPartAuthManager)
        {
            oAuth = new OAuthHelper(ConsumerKey, ConsumerSecret, callbackUrl, RequestToken, Authorize, AccessToken);
            this.tokenSecret = tokenSecret;
        }
        public override string GetLoginUrl()
        {
            return oAuth.AuthorizationGet();
        }
    }
}
