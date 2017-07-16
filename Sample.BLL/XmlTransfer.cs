using Sample.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Sample.BLL
{
    class XmlTransfer
    {
        string _type = "c#";

        public string Type {
            get { return _type; }
        }

        public XmlTransfer(string type) {
            _type = type;
        }


        public string XmlToObject(string text) {            
            try {
                StringBuilder sb = new StringBuilder();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(text);
                XmlElement documentElement = xmlDocument.DocumentElement;
                List<NodeClass> list = findRootNodeClass(xmlDocument);
                foreach (NodeClass current in list) {
                    if (current.IsClass) {
                        string str = current.createClassString(_type);
                        sb.Append(str + "\r\n");
                    }
                }
                return sb.ToString();
            } catch (Exception ex){
                throw ex;
            }
        }

        private void findAllNodeList(XmlNode node, ref List<XmlNode> nodeList) {
            if (!node.Name.Equals("#comment") && !node.Name.Equals("#text")) {
                nodeList.Add(node);
                foreach (XmlNode node2 in node.ChildNodes) {
                    this.findAllNodeList(node2, ref nodeList);
                }
            }
        }

        private List<NodeClass> findRootNodeClass(XmlDocument doc) {
            List<XmlNode> list = new List<XmlNode>();
            List<XmlNode> list2 = new List<XmlNode>();
            findAllNodeList(doc.DocumentElement, ref list2);
            List<NodeClass> list3 = new List<NodeClass>();
            for (int i = 0; i < list2.Count; i++) {
                XmlNode nodeI = list2[i];
                IEnumerable<NodeClass> enumerable = from x in list3
                                                    where x.Name.Equals(nodeI.Name)
                                                    select x;
                NodeClass nodeClass = null;
                if (enumerable != null && enumerable.Count<NodeClass>() > 0) {
                    nodeClass = (from x in list3
                                 where x.Name.Equals(nodeI.Name)
                                 select x).ElementAt(0);
                }
                if (nodeClass == null) {
                    nodeClass = new NodeClass {
                        Name = nodeI.Name
                    };
                    list3.Add(nodeClass);
                }
                if (nodeI.Attributes != null) {
                    foreach (XmlAttribute xmlAttribute in nodeI.Attributes) {
                        if (!nodeClass.NodeAttrList.Contains(xmlAttribute.Name)) {
                            nodeClass.NodeAttrList.Add(xmlAttribute.Name);
                        }
                    }
                }
                if (!nodeClass.NodeAttrList.Contains("valueText")) {
                    nodeClass.NodeAttrList.Add("valueText");
                }
                for (int j = list2.Count - 1; j > i; j--) {
                    XmlNode xmlNode = list2[j];
                    if (nodeI.Name.Equals(xmlNode.Name)) {
                        if (xmlNode.Attributes != null) {
                            foreach (XmlAttribute xmlAttribute in xmlNode.Attributes) {
                                if (!nodeClass.NodeAttrList.Contains(xmlAttribute.Name)) {
                                    nodeClass.NodeAttrList.Add(xmlAttribute.Name);
                                }
                            }
                        }
                    }
                }
            }
            foreach (NodeClass current in list3) {
                XmlNodeList elementsByTagName = doc.GetElementsByTagName(current.Name);
                if (elementsByTagName != null) {
                    foreach (XmlNode xmlNode2 in elementsByTagName) {
                        IEnumerator enumerator3 = xmlNode2.ChildNodes.GetEnumerator();
                        try {
                            XmlNode _nodeChildTag;
                            while (enumerator3.MoveNext()) {
                                _nodeChildTag = (XmlNode)enumerator3.Current;
                                IEnumerable<NodeClass> enumerable = from x in list3
                                                                    where x.Name.Equals(_nodeChildTag.Name)
                                                                    select x;
                                if (enumerable.Count<NodeClass>() > 0) {
                                    IEnumerable<NodeClass> source = from x in current.ChildNodes
                                                                    where x.Name.Equals(_nodeChildTag.Name)
                                                                    select x;
                                    if (source.Count<NodeClass>() == 0) {
                                        current.ChildNodes.Add(enumerable.ElementAt(0));
                                    }
                                }
                            }
                        } finally {
                            IDisposable disposable = enumerator3 as IDisposable;
                            if (disposable != null) {
                                disposable.Dispose();
                            }
                        }
                    }
                }
            }
            return list3;
        }
    }

    public class NodeClass
    {
        private string name = string.Empty;

        private List<string> _NodeAttrList = new List<string>();

        private List<NodeClass> _ChildNodes = new List<NodeClass>();

        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }

        public List<string> NodeAttrList {
            get {
                return this._NodeAttrList;
            }
            set {
                this._NodeAttrList = value;
            }
        }

        public List<NodeClass> ChildNodes {
            get {
                return this._ChildNodes;
            }
            set {
                this._ChildNodes = value;
            }
        }

        public bool IsClass {
            get {
                return this._NodeAttrList.Count >= 2 || this._ChildNodes.Count != 0;
            }
        }

        public string createClassString(string type) {
            string text = string.Empty;
            if (this._NodeAttrList.Count < 2 && this._ChildNodes.Count == 0) {
                text = this.createProperty(this.name, "string", type);
            } else {
                text = this.createClass(this.name);
                string text2 = string.Empty;
                if (this._NodeAttrList.Count > 1) {
                    foreach (string current in this._NodeAttrList) {
                        if (current == "valueText") {
                            continue;
                        }
                        text2 += this.createProperty(current, "string", type);
                    }
                }
                if (this._ChildNodes.Count > 0) {
                    foreach (NodeClass current2 in this._ChildNodes) {
                        if (current2._NodeAttrList.Count < 2 && current2._ChildNodes.Count == 0) {
                            text2 += current2.createClassString(type);
                        } else {
                            //text2 += this.createProperty(current2.name, "List<" + current2.name + ">");
                            text2 += this.createProperty(current2.name, current2.name, type);
                        }
                    }
                }
                text = text.Replace("paramxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxxxx", text2);
            }
            return text.Replace("\r\n\r\n}", "\r\n}"); ;
        }

        private string createProperty(string propertyName, string propertyType, string type) {
            if (propertyName.ToLower().IndexOf("id") > -1) {
                propertyType = "int";
            }
            StringBuilder stringBuilder = new StringBuilder();
            if (type == "c#") {
                stringBuilder.Append(string.Concat(new string[]
                {
                "\r\n\tpublic ",propertyType," ",propertyName
                }));
                stringBuilder.Append(" { get; set; }\r\n");
            } else if (type == "java") {
                if (propertyType == "string") {
                    propertyType = "String";
                }
                stringBuilder.Append(string.Concat(new string[]
                {
                "\r\n\tpublic ",propertyType," ",propertyName,";\r\n"
                }));
                stringBuilder.Append("\tpublic " + propertyType + " get" + FirstLetterUp(propertyName) + "(){\r\n");
                stringBuilder.Append("\t\treturn this." + propertyName + ";\r\n");
                stringBuilder.Append("\t}\r\n");
                stringBuilder.Append("\tpublic " + propertyType + " set" + FirstLetterUp(propertyName) + "("+propertyType+" "+propertyName+"){\r\n");
                stringBuilder.Append("\t\tthis." + propertyName + "=" + propertyName + ";\r\n");
                stringBuilder.Append("\t}\r\n");
            }
            return stringBuilder.ToString();
        }

        private string createClass(string className) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("public class " + className + "\r\n{");
            stringBuilder.Append("paramxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxxxx");
            stringBuilder.Append("\r\n}");
            return stringBuilder.ToString();
        }

        private string FirstLetterUp(string text) {
            string slt = "";
            char[] chars = text.ToArray();
            for (int i = 0; i < chars.Length; i++) {
                if (i == 0) {
                    slt += chars[i].ToString().ToUpper();
                } else {
                    slt += chars[i];
                }
            }
                return slt;
        }
    }
}
