using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TiaFileViewer
{
    // Class that reads in a tia file and saves the entries to corresponding lists.
    // The lists are sorted by index - each nodetype has the same index in every of these lists
    class TiaFile
    {
        private string TiaFileName;

        private List<string> nodeTypes;
        private List<int> nodeTypesCount;
        private List<List<string>> namesOrId;
        private List<List<int>> propertiesCount;

        public List<string> NodeTypes { get { return nodeTypes; } }
        public List<int> NodeTypesCount { get { return nodeTypesCount; } }
        public List<List<string>> NamesOrId { get { return namesOrId; } }
        public List<List<int>> PropertiesCount { get { return propertiesCount; } }
        

        public TiaFile(string tiaFileName)
        {
            TiaFileName = tiaFileName;
            nodeTypes = new List<string>();
            nodeTypesCount = new List<int>();
            namesOrId = new List<List<string>>();
            propertiesCount = new List<List<int>>();
        }

        // Analyzes the Tia File given in constructor
        // Returns false if an error occured, true otherwise
        public bool Analyze()
        {
            if (!File.Exists(TiaFileName))
                return false;
            
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(TiaFileName);
                ReadXmlFile(document);
            }
            catch
            {
                return false;
            }

            return true;
        }
        

        private void ReadXmlFile(XmlDocument document)
        {
            XmlNode node = document.SelectSingleNode("/tiaselectiontool/business/graph/nodes/node");

            do
            {
                // Get the needed values
                string nodeType = node.Attributes[0].Value;
                int propertiesCount = node.SelectSingleNode("properties").ChildNodes.Count;
                string nameOrId = GetNameOrId(node);
                int index = NodeTypes.IndexOf(nodeType);

                // Add to values to lists. Index is -1 if corresponding node type has not already been found
                if (index == -1)
                    AddNewToLists(nodeType, propertiesCount, nameOrId);
                else
                    ChangeExistingLists(index, propertiesCount, nameOrId);

                node = node.NextSibling;
            } while (node != null);

        }

        private void AddNewToLists(string nodeType, int propertiesCount, string nameOrId)
        {
            NodeTypes.Add(nodeType);
            NodeTypesCount.Add(1);

            // Create a new List for the current number of properties of the found node type and add it to the corresponding list
            List<int> propertiesCountByType = new List<int>();
            propertiesCountByType.Add(propertiesCount);
            PropertiesCount.Add(propertiesCountByType);

            // Create a new List for the current name od id of the found node type and add it to the corresponding list
            List<string> namesOrIdByType = new List<string>();
            namesOrIdByType.Add(nameOrId);
            NamesOrId.Add(namesOrIdByType);
        }

        private void ChangeExistingLists(int indexOfType, int propertiesCount, string namesOrId)
        {
            NodeTypesCount[indexOfType] += 1;
            PropertiesCount[indexOfType].Add(propertiesCount);
            NamesOrId[indexOfType].Add(namesOrId);
        }
        
        // Returns the value of the Name property of the given node if existing, otherwise returns the value of the id value
        private string GetNameOrId(XmlNode node)
        {
            string name = "";
            string id = "";

            XmlNode propertyNode = node.SelectSingleNode("properties/property");

            do
            {
                XmlNodeList nodes = propertyNode.ChildNodes;
                string key = nodes[0].InnerText;
                string value = nodes[1].InnerText;
                
                name = key == "Name" ? value : name;    // Get name property if existing
                id = key == "Id" ? value : id;  // Get id property if existing

                propertyNode = propertyNode.NextSibling;
            } while (propertyNode != null);

            // Return value of name property if existing, otherwise return value of id
            return name != "" ? name : id;
        }
    }
}
