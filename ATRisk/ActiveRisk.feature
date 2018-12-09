Feature:SwordActiveRisk Search functionality
		 As a Website user 
		 I want to be able to search for few terms
		 So that I can confirm the search functionality works correctly and the links on the search results are functioning properly

@TC_1
Scenario Outline: Search functionality
Given I navigate to SwordActiveRisk '<website>'
When I enter the search criteria '<values>' and click Submit button
And I wait 
Then I should see the search results and confirm resulting links work

 Examples:
| website                          | values          |
| http://www.sword-activerisk.com/ | Risk management |
| http://www.sword-activerisk.com/ | Leverage        |
| http://www.sword-activerisk.com/ | Compatibility   |
