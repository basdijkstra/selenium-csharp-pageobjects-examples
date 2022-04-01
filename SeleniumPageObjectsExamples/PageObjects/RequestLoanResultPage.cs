using OpenQA.Selenium;

namespace SeleniumPageObjectsExamples.PageObjects
{
    public class RequestLoanResultPage : BasePage
    {
        private IWebDriver driver;

        private By textlabelLoanApplicationResult = By.Id("loanStatus");

        public RequestLoanResultPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public string GetLoanRequestStatus()
        {
            return GetElementText(textlabelLoanApplicationResult);
        }
    }
}
