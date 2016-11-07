using System;

using Appulate.TestModel;
using Swd.Core.Pages;
using Swd.Core.WebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Appulate.TestProject.Smoke
{

    [TestClass]
    public class Smoke_test_for_each_pageobject : TestBase
    {

        public void PageTest(MyPageBase page)
        {
            // Implement Dispose inside page object in order to do cleanup
            using (page)
            {
                // SwdBrowser.HandleJavaScriptErrors();

                page.Invoke();

                // SwdBrowser.HandleJavaScriptErrors();

                page.VerifyExpectedElementsAreDisplayed();

                // SwdBrowser.HandleJavaScriptErrors();
            }
        }
        
        // Add testMethods for your new pages here:
        // *PageName*_VerifyExpectedElements()
        public void InsuredClientPoliciesPage_VerifyExpectedElements()
        {
            PageTest(MyPages.InsuredClientPoliciesPage);
        }
        
        [TestMethod]
        public void QuoteEditorPage_VerifyExpectedElements()
        {
            PageTest(MyPages.QuoteEditorPage);
        }
        
        [TestMethod]
        public void SignInPage_VerifyExpectedElements()
        {
            PageTest(MyPages.SignInPage);
        }
    }
}
