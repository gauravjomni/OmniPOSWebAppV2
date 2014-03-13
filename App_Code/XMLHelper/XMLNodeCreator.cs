using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for XMLNodeCreator
/// </summary>
public class XMLNodeCreator
{
	public XMLNodeCreator()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //returns a xml node for the given element and its value under a xml doc
    public static XmlNode xmlNodeForElement(string element, string value, XmlDocument inDoc)
    {
        XmlNode aNode = inDoc.CreateElement(element);
        aNode.InnerText = value;

        return aNode;
    }

}