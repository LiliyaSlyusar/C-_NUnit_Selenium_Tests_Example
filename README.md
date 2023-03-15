### C#_NUnit_Selenium_Tests_Example
This is a sample project for the E2E automation using the NUnit + Selenium + HtmlElement bundle

Main Test Project is under the CS_NUnitSeleniumTestsExample folder and contains two Demo Test Fixtures(Suites).
Project built in multi-layered (onion) way and consists of:
Core part(domain) - located in the Core folder and contains test domain logic and related extensions.

Component part(infrastructure) - located in the Components folder and contains applciation specific logic, implementation of the Page Object Factory and actions.
To this part we can belond additional services like Email sender or Logger Service or Rest API service,etc.

Testing part(presentation) - actual tests, located in the CS_NUnitSeleniumTestsExample folder with supporting methods and test setup/clean up metohds.
