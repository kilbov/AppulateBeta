
#region Usings - System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
#region Usings - SWD
using Swd.Core;
using Swd.Core.Pages;
using Swd.Core.WebDriver;
#endregion
#region Usings - WebDriver
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
#endregion

namespace Appulate.TestModel.PageDeclarations
{
    public class SignInPage : MyPageBase
    {
        #region WebElements

        [FindsBy(How = How.Id, Using = @"email")]
        protected IWebElement txtEmail { get; set; }


        [FindsBy(How = How.Id, Using = @"password")]
        protected IWebElement txtPassword { get; set; }


        [FindsBy(How = How.Id, Using = @"RememberMe")]
        protected IWebElement chkRememberMe { get; set; }


        [FindsBy(How = How.XPath, Using = @"/html/body/main[1]/div[1]/form[1]/button[1]")]
        protected IWebElement btnSingIn { get; set; }
              

        #endregion

        public void SignInToAppulate(string Email, string Pass)
        {
            txtEmail.SendKeys(Email);
            txtPassword.SendKeys(Pass);
            chkRememberMe.Click();
            btnSingIn.Click();
        }
            
        #region Invoke() and IsDisplayed()
        public override void Invoke()
        {
            Driver.Url = @"https://appulatebeta.com/signin";
        }

        public override bool IsDisplayed()
        {
            return Driver.Url.Contains(@"appulatebeta.com/signin");
        }
        #endregion

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("txtEmail", txtEmail);
            VerifyElementVisible("txtPassword", txtPassword);
            VerifyElementVisible("chkRememberMe", chkRememberMe);
            VerifyElementVisible("btnSingIn", btnSingIn);
        }
    }
}
