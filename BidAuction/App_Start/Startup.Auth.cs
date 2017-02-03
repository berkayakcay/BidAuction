using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using BidAuction.Models;
using Microsoft.Owin.Security.Twitter;

namespace BidAuction
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "2L3FpITwi3LViAVAB0bIPhCCu",
            //   consumerSecret: "RBXJxvYezWwSLG07orrQQyJDUoCoxE84zoP16gqyCn0WRftzBp");


            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "2L3FpITwi3LViAVAB0bIPhCCu",
                ConsumerSecret = "RBXJxvYezWwSLG07orrQQyJDUoCoxE84zoP16gqyCn0WRftzBp",
                BackchannelCertificateValidator =
                new Microsoft.Owin.Security.CertificateSubjectKeyIdentifierValidator(
                new[] {
                // VeriSign Class 3 Secure Server CA - G2
                "A5EF0B11CEC04103A34A659048B21CE0572D7D47",
                // VeriSign Class 3 Secure Server CA - G3
                "0D445C165344C1827E1D20AB25F40163D8BE79A5", 
                // VeriSign Class 3 Public Primary Certification Authority - G5
                "7FD365A7C2DDECBBF03009F34339FA02AF333133", 
                // Symantec Class 3 Secure Server CA - G4
                "39A55D933676616E73A761DFA16A7E59CDE66FAD", 
                // Symantec Class 3 EV SSL CA - G3
                "‎add53f6680fe66e383cbac3e60922e3b4c412bed", 
                // VeriSign Class 3 Primary CA - G5
                "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", 
                // DigiCert SHA2 High Assurance Server C‎A 
                "5168FF90AF0207753CCCD9656462A212B859723B",
                // DigiCert High Assurance EV Root CA 
                "B13EC36903F8BF4701D498261A0802EF63642BC3"
                })
            });

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}