using OpenQA.Selenium;

namespace SeleniumPageObjectsExamples.PageObjects
{
    public class LoginPage : BasePage
    {
        private const string LoginPageUrl = "http://localhost:8080/parabank";

        private By textfieldUsername = By.Name("username");
        private By textfieldPassword = By.Name("password");
        private By buttonDoLogin = By.XPath("//input[@value='Log In']");

        public LoginPage(IWebDriver driver) : base(driver)
        {
            driver.Navigate().GoToUrl(LoginPageUrl);
        }

        public void LoginAs(string username, string password)
        {
            SendKeys(textfieldUsername, username);
            SendKeys(textfieldPassword, password);
            Click(buttonDoLogin);
        }
    }
}
