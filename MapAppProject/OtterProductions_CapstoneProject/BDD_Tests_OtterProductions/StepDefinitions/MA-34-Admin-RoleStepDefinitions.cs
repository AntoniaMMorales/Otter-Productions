using BDD_Tests_OtterProductions.Drivers;
using BDD_Tests_OtterProductions.PageObjects;
using BDD_Tests_OtterProductions.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;
using System;
using System.Diagnostics;

namespace BDD_Tests_OtterProductions.StepDefinitions
{

    public class TestAdmin
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    [Binding]
    public class AdminRoleStepDefinitions
    {

        private readonly AdminRolePageObject _adminPage;
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;

        private IConfigurationRoot Configuration { get; }
        public AdminRoleStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _adminPage = new AdminRolePageObject(browserDriver.Current);
            _loginPage = new LoginPageObject(browserDriver.Current);
            _scenarioContext = context;


            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<AdminRoleStepDefinitions>();
            Configuration = builder.Build();
        }

        [Given(@"I am the admin")]
        public void GivenIAmTheAdmin()
        {
            TestUser admin = new TestUser
            {
                UserName = "admin",
                FirstName = "The",
                LastName = "Admin",
                Email = "admin@example.com",
                Password = Configuration["SeedAdminPW"]
            };
            if (admin.Password == null)
            {
                throw new Exception("Did you forget to set the admin password in user-secrets?");
            }
            Debug.WriteLine("Password = " + admin.Password);
            _scenarioContext["CurrentUser"] = admin;
        }


        //[Given(@"I am logged in as an admin")]
        //public void GivenIAmLoggedInAsAnAdmin()
        //{
        //    TestAdmin admin = new TestAdmin
        //    {
        //        UserName = "admin@email.com",
        //        FirstName = "The",
        //        LastName = "Admin",
        //        Email = "admin@email.com",
        //        Password = "OPAdmin4612023" //Configuration["SeedAdminPW"]
        //    };
        //    if (admin.Password == null)
        //    {
        //        throw new Exception("Did you forget to set the admin password in user-secrets?");
        //    }
        //    Debug.WriteLine("Password = " + admin.Password);
        //    _scenarioContext["CurrentAdmin"] = admin;

        //    // Go to the login page
        //    _adminPage.GoTo();
        //    //Thread.Sleep(3000);
        //    // Now (attempt to) log them in.  Assumes previous steps defined the user
        //    TestAdmin u = (TestAdmin)_scenarioContext["CurrentAdmin"];
        //    _adminPage.EnterEmail(u.Email);
        //    _adminPage.EnterPassword(u.Password);
        //    _adminPage.Login();

        //}

        //[When(@"I am on the ""([^""]*)"" page")]
        //public void WhenIAmOnThePage(string pageName)
        //{
        //    _adminPage.GoTo(pageName);
        //}

        [Then(@"The page presents a View Users button")]
        public void ThenThePagePresentsAViewUsersButton()
        {

            _adminPage.ViewUsersButton.Should().NotBeNull();
            _adminPage.ViewUsersButton.Displayed.Should().BeTrue();

            Assert.That(_adminPage.ViewUsersButton.GetAttribute("id"), Does.Contain("viewUsersButton"));
        }

        [Then(@"The page contains an explanation message")]
        public void ThenThePageContainsAnExplanationMessage()
        {
            _adminPage.ExplanationMessage.Should().NotBeNull();
            _adminPage.ExplanationMessage.Displayed.Should().BeTrue();

            Assert.That(_adminPage.ExplanationMessage.GetAttribute("class"), Does.Contain("explanationStatement"));

        }

        [Then(@"The page title contains ""([^""]*)""")]
        public void ThenThePageContainsATitle(string p0)
        {
            //Thread.Sleep(10000);
            _adminPage.Title.Should().NotBeNull();
            _adminPage.Title.Displayed.Should().BeTrue();

            Assert.That(_adminPage.Title.GetAttribute("id"), Does.Contain("dashboardHeader"));

        }

        [Then(@"The page contains an error message")]
        public void ThenThePageContainsAnErrorMessage()
        {
            //Thread.Sleep(10000);
            _adminPage.ErrorMessage.Should().NotBeNull();
            _adminPage.ErrorMessage.Displayed.Should().BeTrue();

            Assert.That(_adminPage.ErrorMessage.GetAttribute("class"), Does.Contain("text-danger"));

        }

        [Then(@"I am redirected to the '([^']*)' page")]
        public void ThenIAmRedirectedToThePage(string pageName)
        {
            // how do we identify which page we're on?  We're saving that in Common, so use that data
            // any page object will do
            _loginPage.GetURL().Should().Be(Common.UrlFor(pageName));
        }


        //[Given(@"I am a registered user")]
        //public void GivenIAmARegisteredUser()
        //{

        //    TestUser user = new TestUser
        //    {
        //        UserName = "johndoe@email.com",
        //        FirstName = "John",
        //        LastName = "Doe",
        //        Email = "johndoe@email.com",
        //        Password = Configuration["userTestingPassword"]
        //    };
        //    if (user.Password == null)
        //    {
        //        throw new Exception("Did you forget to set the user password in user-secrets?");
        //    }
        //    Debug.WriteLine("Password = " + user.Password);
        //    _scenarioContext["CurrentUser"] = user;
        //}

        [When(@"I navigate to the ""([^""]*)"" page")]
        public void WhenINavigateToThePage(string pageName)
        {
            _adminPage.GoTo(pageName);
        }

        [Given(@"I login"), When(@"I login")]
        public void WhenILogin()
        {
            // Go to the login page
            _loginPage.GoTo();
            //Thread.Sleep(3000);
            // Now (attempt to) log them in.  Assumes previous steps defined the user
            TestUser u = (TestUser)_scenarioContext["CurrentUser"];
            _loginPage.EnterEmail(u.Email);
            _loginPage.EnterPassword(u.Password);
            _loginPage.Login();
        }

        [Then(@"I can see a message in the navbar that includes my email")]
        public void ThenICanSeeAMessageInTheNavbarThatIncludesMyEmail()
        {
            // This is after a redirection to the homepage so we need to use that page
            TestUser u = (TestUser)_scenarioContext["CurrentUser"];
            _adminPage.NavbarWelcomeText().Should().ContainEquivalentOf(u.Email, AtLeast.Once());
        }

    }
}
