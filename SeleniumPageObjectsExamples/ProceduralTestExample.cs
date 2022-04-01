using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumPageObjectsExamples
{
    [TestFixture]
    public class ProceduralTestExample
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void RequestLoan_WithinBoundaries_ShouldBeAccepted()
        {
            // Start application and log in
            driver.Navigate().GoToUrl("http://localhost:8080/parabank");
            SendKeys(By.Name("username"), "john");
            SendKeys(By.Name("password"), "demo");
            Click(By.XPath("//input[@value='Log In']"));

            // Navigate to Request Loan form
            Click(By.LinkText("Request Loan"));

            // Complete Request Loan form
            SendKeys(By.Id("amount"), "1000");
            SendKeys(By.Id("downPayment"), "100");
            Select(By.Id("fromAccountId"), "12345");
            Click(By.XPath("//input[@value='Apply Now']"));

            // Check loan application result
            string result = GetElementText(By.Id("loanStatus"));

            Assert.That(result, Is.EqualTo("Approved"));
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