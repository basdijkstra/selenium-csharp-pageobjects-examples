using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumPageObjectsExamples.Models;
using SeleniumPageObjectsExamples.PageObjects;

namespace SeleniumPageObjectsExamples
{
    [TestFixture]
    public class PageObjectTestExample
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            this.driver = new ChromeDriver();
            this.driver.Manage().Window.Maximize();

            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void RequestLoan_WithinBoundaries_ShouldBeAccepted()
        {
            Customer customer = new Customer();

            // Start application and log in
            new LoginPage(this.driver)
                .LoginAs(customer.Username, customer.Password);

            // Navigate to Request Loan form
            new AccountsOverviewPage(driver)
                .SelectMenuItem("Request Loan");

            // Complete Request Loan form
            new RequestLoanPage(driver)
                .SubmitLoanRequest("1000", "100", "12345");

            // Check loan application result
            string loanApplicationResult = new RequestLoanResultPage(driver)
                .GetLoanRequestStatus();

            Assert.That(loanApplicationResult, Is.EqualTo("Approved"));
        }

        [TearDown]
        public void StopBrowser()
        {
            driver.Quit();
        }

        private void SendKeys(By locator, string textToSend)
        {
            try
            {
                IWebElement myElement = wait.Until<IWebElement>(driver =>
                {
                    IWebElement tempElement = driver.FindElement(locator);
                    return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
                }
                );
                myElement.Clear();
                myElement.SendKeys(textToSend);
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Exception in SendKeys(): element located by {locator} not visible and enabled within 10 seconds.");
            }
        }

        private void Click(By locator)
        {
            try
            {
                IWebElement myElement = wait.Until<IWebElement>(driver =>
                {
                    IWebElement tempElement = driver.FindElement(locator);
                    return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
                }
                );
                myElement.Click();
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Exception in Click(): element located by {locator} not visible and enabled within 10 seconds.");
            }
        }

        private void Select(By locator, string valueToSelect)
        {
            try
            {
                IWebElement myElement = wait.Until<IWebElement>(driver =>
                {
                    IWebElement tempElement = driver.FindElement(locator);
                    return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
                }
                );
                SelectElement dropdown = new SelectElement(myElement);
                dropdown.SelectByText(valueToSelect);
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Exception in Select(): element located by {locator} not visible and enabled within 10 seconds.");
            }
        }

        private string GetElementText(By locator)
        {
            try
            {
                IWebElement myElement = wait.Until<IWebElement>(driver =>
                {
                    IWebElement tempElement = driver.FindElement(locator);
                    return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
                }
                );
                return myElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Exception in GetElementText(): element located by {locator} not visible and enabled within 10 seconds.");
                return string.Empty;
            }
        }
    }
}