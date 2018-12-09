using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace ATRisk
{
    [TestClass]
    public class SwordActiveRiskSearchFunctionalitySteps
    {
        ChromeDriver driver;

        [TestMethod]
        public void TestMethod1()
        {
            GivenINavigateToTheHomePage();
            WhenIEnterTheSearchCriteriaAndClickSubmit();
        }

        public List<string> GetSearchCriteria()
        {
            List<string> searchCriteria = new List<string>();

            searchCriteria.Add("Risk Management");
            searchCriteria.Add("Leverage");
            searchCriteria.Add("Compatibility");

            return searchCriteria;
        }
        [Given(@"I navigate to SwordActiveRisk '<website>'")]
        public void GivenINavigateToTheHomePage()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl("http://www.sword-activerisk.com/");
        }

        [When(@"I enter the search criteria '(.*)' and click Submit button")]
        public void WhenIEnterTheSearchCriteriaAndClickSubmit()
        {
            List<string> searchCriteria = GetSearchCriteria();

            foreach (string search in searchCriteria)
            {
                driver.Navigate().GoToUrl("http://www.sword-activerisk.com/");

                //get search box id
                var searchBox = driver.FindElement(By.Id("s"));

                searchBox.SendKeys(search);
                driver.FindElement(By.Id("searchsubmit")).Click();

                WhenIWait();

                ThenIShouldSeeTheSearchResultsAndConfirmResultingLinksWork();
            }
        }

        [When(@"I wait")]
        public void WhenIWait()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [Then(@"I should see the search results and confirm resulting links work")]
        public void ThenIShouldSeeTheSearchResultsAndConfirmResultingLinksWork()
        {
                //Get all the hyper links
                IList<IWebElement> elements = driver.FindElements(By.TagName("a"));

                //for each hyperlink click 
                foreach (IWebElement element in elements)
                {
                    if (!string.IsNullOrEmpty(element.Text))
                    {
                        string value = element.GetAttribute("href");

                        try
                        {
                            // For each link check response status code is OK.
                            WebRequest request = WebRequest.Create(value);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                //link is working
                            }
                            else
                            {
                                //link is not working
                            }
                        }
                        catch (Exception)
                        {
                            //do not do anything in the event of exception just continue with the next hyperlink
                        }
                    }
                }
           
        }

    }
}

