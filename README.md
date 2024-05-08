# Simple C# XML Validator

Method DetermineXml(string xml) has been implemented in the class SimpleXmlValidator. It will return true or false based on the rules of XML as defined below:

## Definition of a valid XML string
- Element names can be composed of any string that does not consist of the following special characters
  - ' ' (blank space)
  - &
  - /
  - <
  - \>
- The following characters are invalid in any context
  - &
- Each starting element must have a corresponding ending element
- Elements must be well nested, meaning that the element that starts first must end last
- Elements may be self-closing by ending with '/>'
  - Empty spaces are permitted between the element name and the '/>'
- There can be no characters before or after the root element

# Testing

Test cases have been added to the testCases string list defined in Program.cs

# Checklist:

- [x] My code follows the style guidelines of this project
- [x] I have performed a self-review of my code
- [x] I have commented my code, particularly in hard-to-understand areas
- [x] I have made corresponding changes to the documentation
- [x] My changes generate no new warnings
- [x] I have added tests that prove my fix is effective or that my feature works
- [x] New and existing unit tests pass locally with my changes