#region Usings - System
using System;
using System.IO;
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

using AutoItX3Lib;

namespace Appulate.TestModel.PageDeclarations
{
    public class QuoteEditorPage : MyPageBase
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = @"id(""boxNewTabs"")/div[1]/span[1]")]
        protected IWebElement menuGeneralInfo { get; set; }

        [FindsBy(How = How.XPath, Using = @"id(""boxNewTabs"")/div[2]/div[1]/a[9]")]
        protected IWebElement menuAdditionalInformation { get; set; }
        
        [FindsBy(How = How.XPath, Using = @"id(""cellFields"")/div[1]/table[2]/tbody[1]/tr[1]/td[1]/div[2]/textarea[1]")]
        protected IWebElement txtAdditionalRemarks { get; set; }

        [FindsBy(How = How.ClassName, Using = @"uploader-button")]
        protected IWebElement btnChooseFiles { get; set; }
        
        [FindsBy(How = How.Id, Using = @"btnSave")]
        protected IWebElement btnSave { get; set; }
                                  
        [FindsBy(How = How.XPath, Using = @"id(""cellFields"")/div[1]/table[2]/tbody[1]/tr[2]/td[1]/div[2]/div[2]")]
        protected IWebElement divUploadedFiles { get; set; }
        
        #endregion

        string TitleOpenFileDialog = "Open";

        public QuoteEditorPage()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            if (culture.ToString() == "ru-RU") TitleOpenFileDialog = "Открытие";
        }

        #region Invoke() and IsDisplayed()
        public override void Invoke()
        {
            
            Driver.Url = @"https://appulatebeta.com/icc/quoteeditor.aspx";
        }

        public override bool IsDisplayed()
        {
            return Driver.Url.Contains(@"icc/quoteeditor");
        }
        #endregion

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("menuGeneralInfo", menuGeneralInfo);
            VerifyElementVisible("menuAdditionalInformation", menuAdditionalInformation);
            VerifyElementVisible("txtAdditionalRemarks", txtAdditionalRemarks);
            VerifyElementVisible("btnChooseFiles", btnChooseFiles);
        }

        public void OpenAdditionalInformationPage()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("document.getElementsByClassName('menubody')[0].setAttribute('style', 'left: -156px; display: block')");
            menuAdditionalInformation.Click();
        }

        /// <summary>
        /// Upload image on server 
        /// </summary>
        /// <param name="imagePath">Path to the file</param>
        /// <param name="millisecondsUploadTimeout">Amount of time will wait for upload file before failing a request</param>
        public void UploadImageFile(string imagePath, int millisecondsUploadTimeout)
        {
            btnChooseFiles.WaitUntilVisible(2000);
            
            int oldCountUploadedFiles = divUploadedFiles.FindElements(By.ClassName("file-name")).Count;

            btnChooseFiles.Click();
                   
            AutoItX3 autoIt = new AutoItX3();
            
            if (autoIt.WinWait(TitleOpenFileDialog) == 0)
                throw new NullReferenceException("OpenFileDialog can not be found.");
            autoIt.ControlFocus(TitleOpenFileDialog, "", "Edit1");
            autoIt.Sleep(500);
            autoIt.ControlSetText(TitleOpenFileDialog, "", "Edit1", imagePath);
            autoIt.ControlClick(TitleOpenFileDialog, "", "Button1");
            int newCountUploadedFiles = 0;

            for (int i = 0; (i < millisecondsUploadTimeout) && (newCountUploadedFiles <= oldCountUploadedFiles); i += 500)
            {
                 System.Threading.Thread.Sleep(500);
                 newCountUploadedFiles = divUploadedFiles.FindElements(By.ClassName("file-name")).Count;
            }

            string info = "Upload: " + DateTime.Now.ToString() + " - " + Path.GetFileName(imagePath) + "\n";

            if (oldCountUploadedFiles == newCountUploadedFiles) throw new ErrorUploadImagesException("Error loading: "+ info);

            txtAdditionalRemarks.SendKeys(info);
            btnSave.Click();
        }
        
    }

    public class ErrorUploadImagesException: Exception
    {
        public ErrorUploadImagesException(){}

        public ErrorUploadImagesException(string message)
        : base(message){}

        public ErrorUploadImagesException(string message, Exception inner)
        : base(message, inner){}
    }
}



//=================================================================

//string UploadScript = @"$(function(){UploaderControl({'controls':{'btnUpload':'#btn-" + idControl +
//    "','pnlUploadData':'#upload-data-" + idControl + "','pnlFinishedUpload':'#upload-finished-" + idControl +
//    "','firingControl':null,'control':'#" + idControl + "'},'buttonText':'Choose File11s...','uploadHandler':'/insured/formattachedfiles.ashx','fileDescription':'Images','allowedExtensions':'bmp,png,jpg,gif','multiUpload':true,'maxFiles':999,'fileSizeLimit':4194304,'maxCumulativeSize':2147483647,'uploadLimit':0,'senderParamId':'senderId','senderVal':'" +
//    idControl + "','fileNameParamId':'fileName','fileObjName':'Filedata','firingControlEvent':'submit','autostart':true,'swf':'/scripts/controls/plupload/plupload.swf'});});";

//js.ExecuteScript(UploadScript);

//btnChooseFiles.SendKeys(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\SWD.jpg");


//driver.findElement(By, id(uploadButton)).sendKeys(pathToFile)

//=================================================================


