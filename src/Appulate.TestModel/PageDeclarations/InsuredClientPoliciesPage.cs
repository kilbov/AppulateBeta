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
    public class InsuredClientPoliciesPage : MyPageBase
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = @"id(""tabs-0"")/div[2]/a[1]/img[1]")]
        protected IWebElement plusInvestmentManagement { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""tabs-0"")/div[2]/div[1]/div[1]/a[1]")]
        protected IWebElement txtWorkersCompensation { get; set; }

        #endregion

        #region Invoke() and IsDisplayed()
        public override void Invoke()
        {
            Driver.Url = @"https://appulatebeta.com/icc/insuredclientpolicies.aspx";
        }

        public override bool IsDisplayed()
        {
            return Driver.Url.Contains(@"icc/insuredclientpolicies");
        }
        #endregion

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("plusInvestmentManagement", plusInvestmentManagement);
            VerifyElementVisible("txtWorkersCompensation", txtWorkersCompensation);
        }

        public void ExpandListPolicies()
        {
            plusInvestmentManagement.Click();
            txtWorkersCompensation.Click();
        }

    }
}