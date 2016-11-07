using System;
using Appulate.TestModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appulate.TestProject
{
    
    [TestClass]
    public class UploadImagesTest
    {
        /// <summary>
        /// Log on under the insured’s account 
        /// </summary>
        [TestMethod]
        public void SignInPage_LoginTest()
        {          
            var page = MyPages.SignInPage;
            page.Invoke();
            page.SignInToAppulate("david.smith.appulatetest@appulatemail.com", "123321");
        }

        /// <summary>
        /// On the Applications page, expand the list of applications and policies
        /// </summary>
        [TestMethod]
        public void InsuredClientPoliciesPage_ExpandListPoliciesTest()
        {
            SignInPage_LoginTest();
            MyPages.InsuredClientPoliciesPage.ExpandListPolicies();
        }

        /// <summary>
        /// Click Additional Information on the sections’ menu.
        /// On the open Additional Information page, upload image file  using the Choose Files button.
        /// </summary>
        [TestMethod]
        public void QuoteEditorPage_UploadImageFileTest()
        {
            InsuredClientPoliciesPage_ExpandListPoliciesTest();
            MyPages.QuoteEditorPage.OpenAdditionalInformationPage();

            MyPages.QuoteEditorPage.UploadImageFile(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\SWD.jpg",  10000);
            MyPages.QuoteEditorPage.UploadImageFile(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\bmp-2.bmp",10000);
            MyPages.QuoteEditorPage.UploadImageFile(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\army.gif", 10000);
            MyPages.QuoteEditorPage.UploadImageFile(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\1390.png", 10000);

        }
    }
}
