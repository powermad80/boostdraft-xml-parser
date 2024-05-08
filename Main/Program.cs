using SimpleXMLValidatorLibrary;

class Program
{
    static void Main(string[] args)
    {
#if DEBUG
        // You can use here to test, feel free to modify/add the test cases here.
        // You can also use other ways to test if you want.

        List<(string testCase, bool expectedResult)> testCases = new()
        {
            ("<Design><Code>hello world</Code></Design>",  true),//normal case
            ("<Design><Code>hello world</Code></Design><People>", false),//no closing tag for "People" 
            ("<People><Design><Code>hello world</People></Code></Design>", false),// "/Code" should come before "/People" 
            ("<People age=”1”>hello world</People>", false),//there is no closing tag for "People age=”1”" and no opening tag for "/People"
            ("<people><dave></dave></people>7", false),//no characters after root tag is closed
            ("a<people><dave></dave></people>", false),//no characters before root tag is opened
            ("<people><dave></dave></people><wombat</wombat>", false),//xml string must end when the root tag is closed
            ("<peo/ple></peo/ple>", false),//tags cannot contain '/'
            ("</>", false),//character-less tags are invalid
            ("<></>", false),//character-less tags are invalid
            ("<w></w>", true),//single-character tags are valid
            ("<ww <></ww <>", false),// '<' and '>' are not valid characters within a tag
            ("<this><is><a><story><of><a><girl>", false),//opening a lot of tags and not closing them is invalid
            ("<root><closer/></root>", true),//self-closing tags are valid
            ("<root><closer /></root>", true)//self-closing tags with a trailing space are valid

        };
        int failedCount = 0;
        foreach ((string input, bool expected) in testCases)
        {
            bool result = SimpleXmlValidator.DetermineXml(input);
            string resultStr = result ? "Valid" : "Invalid";

            string mark;
            if (result == expected)
            {
                mark = "OK ";
            }
            else
            {
                mark = "NG ";
                failedCount++;
            }
            Console.WriteLine($"{mark} {input}: {resultStr}");
        }
        Console.WriteLine($"Result: {testCases.Count - failedCount}/{testCases.Count}");
#else
        string input = args.FirstOrDefault("");
        bool result = SimpleXmlValidator.DetermineXml(input);
        Console.WriteLine(result ? "Valid" : "Invalid");
#endif
    }
}