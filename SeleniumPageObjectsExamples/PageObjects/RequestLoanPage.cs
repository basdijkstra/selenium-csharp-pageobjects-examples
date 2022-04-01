using OpenQA.Selenium;

namespace SeleniumPageObjectsExamples.PageObjects
{
    public class RequestLoanPage : BasePage
    {
        private IWebDriver driver;

        private By textfieldLoanAmount = By.Id("amount");
        private By textfieldDownPayment = By.Id("downPayment");
        private By dropdownFromAccountId = By.Id("fromAccountId");
        private By buttonSubmitLoanRequest = By.XPath("//input[@value='Apply Now']");

        public RequestLoanPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public void SubmitLoanRequest
            (string loanAmount, string downPayment, string fromAccountId)
        {
            SendKeys(textfieldLoanAmount, loanAmount);
            SendKeys(textfieldDownPayment, downPayment);
            Select(dropdownFromAccountId, fromAccountId);
            Click(buttonSubmitLoanRequest);
        }
    }
}
