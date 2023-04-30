using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using BDD_Tests_OtterProductions.Shared;
using System.Collections.ObjectModel;
using BDD_Tests_OtterProductions.PageObjects;

namespace BDD_Tests_OtterProductions.PageObjects
{
    public class AdminRolePageObject : PageObject
    {

        public AdminRolePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Admin Dashboard";
        }

        public IWebElement ViewUsersButton => _webDriver.FindElement(By.Id("viewUsersButton"));
        public IWebElement ExplanationMessage => _webDriver.FindElement(By.Id("explanationStatement"));

        public IWebElement ErrorMessage => _webDriver.FindElement(By.ClassName("text-danger"));


        public IWebElement Title => _webDriver.FindElement(By.Id("dashboardHeader"));
        public IWebElement NavBarHelloLink => _webDriver.FindElement(By.CssSelector("a[href=\"/Identity/Account/Manage\"]"));

        public IWebElement EmailInput => _webDriver.FindElement(By.Name("Input.Email"));
        public IWebElement PasswordInput => _webDriver.FindElement(By.Name("Input.Password"));

        public IWebElement SubmitButton => _webDriver.FindElement(By.Id("buttonLogin"));


        public string NavbarWelcomeText()
        {
            return NavBarHelloLink.Text;
        }

        public void EnterEmail(string email)
        {
            EmailInput.Clear();
            EmailInput.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
        }

        public void Login()
        {
            SubmitButton.Click();
        }

        //public void NavigateToRegister()
        //{
        //    GoToRegister.Click();
        //}


    }
}