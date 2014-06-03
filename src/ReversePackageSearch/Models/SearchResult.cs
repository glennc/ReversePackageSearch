using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReversePackageSearch.Models
{
    public class SearchResult
    {
        private string _signature;
        private string _packageName;
        private string _fullTypeName;
        private string _returnType;

        public string PackageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }

        public string FullTypeName
        {
            get { return _fullTypeName; }
            set { _fullTypeName = value; }
        }

        public string ReturnType
        {
            get { return _returnType; }
            set { _returnType = value; }
        }

        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
    }
}