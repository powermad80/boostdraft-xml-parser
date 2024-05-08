namespace SimpleXMLValidatorLibrary
{
    public class SimpleXmlValidator
    {

        
        public static bool DetermineXml(string xml)
        {
            Stack<string> tagStack = new Stack<string>();
            char currentChar;
            string currentTag;
            int tagEndIndex;

            // characters that are invalid anywhere in the xml string
            List<char> globalInvalidCharacters = new List<char>() { '&' };

            // if the string is empty, it's invalid
            if (xml.Length == 0)
            {
                return false;
            }

            // check if the beginning and end of the xml string is well-formed
            if (!doesXmlStringBeginAndEndProperly(xml))
            {
                return false;
            }

            for (int i = 0; i < xml.Length; i++)
            {
                currentChar = xml[i];

                // check if the current character is invalid
                if (globalInvalidCharacters.Contains(currentChar))
                {
                    return false;
                }

                // if we encounter a '<', we know we're at the start of a tag
                if (currentChar == '<')
                {

                    // get the index of the end of the tag
                    tagEndIndex = xml.IndexOf('>', i);

                    // get the text of the tag
                    currentTag = xml.Substring(i + 1, tagEndIndex - i - 1);

                    // if the tag is empty, it's invalid
                    if (currentTag.Length == 0)
                    {
                        return false;
                    }

                    // if the first character of the tag is '/', it's a closing tag
                    if (currentTag.First() == '/')
                    {
                        // Process a closing tag
                        if (!processClosingTag(currentTag, tagStack))
                        {
                            return false;
                        }
                        
                        // if the stack is empty, the root element has been closed; so check if we're at end of string
                        if (tagStack.Count == 0)
                        {
                            // if we aren't, it's invalid
                            if (tagEndIndex != xml.Length - 1)
                            {
                                return false;
                            }
                            // if we are, it's valid
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        // process an opening tag
                        if (!processOpeningTag(currentTag, tagStack))
                        {
                            return false;
                        }
                    }

                    // we've processed the whole tag, so set the index to the end of the tag
                    i = tagEndIndex;
                }
            }

            // if all characters have been processed and the stack has not been emptied, the root element has not been closed, so it's invalid
            return false;
        }

        private static bool processOpeningTag(string currentTag, Stack<string> tagStack)
        {

            // check if the tag is self-closing
            if (currentTag.Last() == '/')
            {
                // trailing spaces are valid on a self-closing tag. Get the tag without the trailing '/' and trim any spaces from only the end
                string trimmedTag = currentTag.Substring(0, currentTag.Length - 1).TrimEnd();

                // check for invalid characters
                if (doesTagContainInvalidCharacters(trimmedTag))
                {
                    return false;
                }

                // self-closing tags don't need to be added to the stack
                return true;
            }

            // check for invalid characters
            if (doesTagContainInvalidCharacters(currentTag))
            {
                return false;
            }

            // add tag to the stack
            tagStack.Push(currentTag);

            return true;
        }

        private static bool processClosingTag(string currentTag, Stack<string> tagStack)
        {
            // get the tag text without the leading '/'
            string endTag = currentTag.Substring(1);

            if (doesTagContainInvalidCharacters(endTag))
            {
                return false;
            }

            // if there's nothing in the stack, or the top of the stack doesn't match the tag, it's invalid
            if (tagStack.Count == 0 || tagStack.Peek() != endTag)
            {
                return false;
            }

            // remove matched tag from the stack
            tagStack.Pop();

            return true;

        }

        // a valid XML string must begin with '<' and end with '>'
        // this is a fast check to see if the string is even worth processing
        private static bool doesXmlStringBeginAndEndProperly(string xml)
        {
            if (xml.First() != '<' || xml.Last() != '>')
            {
                return false;
            }

            return true;
        }

        // Check if the tag contains invalid characters
        private static bool doesTagContainInvalidCharacters(string tag)
        {
            // characters that are invalid within tags
            List<char> invalidTagCharacters = new List<char>() { '<', '>', '/', ' ' };

            foreach (char c in tag)
            {
                if (invalidTagCharacters.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }   
    }
}