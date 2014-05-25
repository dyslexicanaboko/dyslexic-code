using System;
using System.ServiceModel.Configuration;

//WCFServicesSecurity.AuthorizationBehaviorExtensionElement
namespace WCFServicesSecurity
{
    public class AuthorizationBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new AuthorizationBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(AuthorizationBehavior);
            }
        }
    }
}
