using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.CSharp;
using ModelsGenerator.Models;

namespace ModelsGenerator.Helpers
{
    public class ParametersCodeGenerator
    {
        public string Status { get; set; }
        public bool DetectType { get; set; }

        public string GenerateParametersCode(string queryString, string entityName)
        {
            StringBuilder parametersBuilder = new StringBuilder();
            Status = "";
            try
            {
                //Append usings
                parametersBuilder.Append("using QueryExtensions;\n\n");

                //Append class declration
                parametersBuilder.Append(string.Format("public class {0}\n{{\n", entityName));
                // Parse the query string variables into a NameValueCollection.
                NameValueCollection pairCollection = HttpUtility.ParseQueryString(queryString);
                var allKeys = pairCollection.AllKeys;
                foreach (var key in allKeys)
                {
                    var values = pairCollection.GetValues(key);
                    if (key.EndsWith("[]"))
                    {
                        AppendArray(key, values, parametersBuilder);
                    }
                    else if (values == null || values.Length == 0)
                    {
                        AppenedPrimitive(key, null, parametersBuilder);
                    }
                    else if (values.Length == 1)
                    {
                        bool isArrayWithSingleKey = CheckIsArrayWithSingleKey(values[0]);
                        if (isArrayWithSingleKey)
                            AppendArray(key, values, parametersBuilder);
                        else AppenedPrimitive(key, values[0], parametersBuilder);
                    }
                    else
                    {
                        AppendArray(key, values, parametersBuilder);
                    }
                }
                parametersBuilder.Append("}");
            }
            catch (Exception)
            {
                parametersBuilder.Clear();
            }

            var queryParametersResult = parametersBuilder.ToString();
            if (string.IsNullOrEmpty(queryParametersResult))
            {
                Status = "Unable to parse Request";
            }
            else
            {
                Status = "Use Clip board or select code and paste in visual studio";
            }

            return queryParametersResult;
        }

        private bool CheckIsArrayWithSingleKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var elements = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length <= 1) return false;
            return true;
        }

        private void AppenedPrimitive(string key, string value, StringBuilder parametersBuilder)
        {
            string propertyName = ConvertToPropertyName(key);
            Type propertyType = ConvertToPropertyType(value);
            AddComment(key, value, propertyType, false, parametersBuilder);
            parametersBuilder.AppendLine(string.Format("[QueryParameter(\"{0}\")]", key));
            parametersBuilder.AppendLine(string.Format("public {0} {1} {{ get; set; }}", GetTypeName(propertyType), propertyName));
            parametersBuilder.AppendLine();
        }

        private void AddComment(string key, string value, Type propertyType, bool isArray, StringBuilder parametersBuilder)
        {
            if (propertyType == typeof(DateTime))
                parametersBuilder.AppendLine(@"//TODO:Add format and culture(default Invariant) to Date Time the backend is expecting");

        }

        private void AppendArray(string key, string[] values, StringBuilder parametersBuilder)
        {
            string attributeName = ConvertToListAttributeName(key);
            bool dontEscapeKey = AddDontEscapeKey(attributeName);
            string propertyName = ConvertToListName(key);
            var elementValue = values == null || values.Length == 0 ? string.Empty : values[0];
            Type propertyType = ConvertToPropertyType(elementValue);
            string arrayFormat = GetArrayFormat(key, values);

            AddComment(key, elementValue, propertyType, false, parametersBuilder);
            parametersBuilder.AppendLine(
                string.Format("[QueryArray(\"{0}\", ArrayFormat= ArrayFormat.{1}{2})]",
                attributeName,
                arrayFormat,
                dontEscapeKey ? ", DontEscapeKey = true" : "")
                );
            parametersBuilder.AppendLine(string.Format("public List<{0}> {1} {{ get; set; }}", GetTypeName(propertyType), propertyName));
            parametersBuilder.AppendLine();
        }

        public string GetArrayFormat(string key, string[] values)
        {
            if (values == null) throw new NullReferenceException();
            if (values.Length == 1) return ArrayFormat.SingleKey.ToString();
            if (key.EndsWith("[]")) return ArrayFormat.DuplicateKeyWithBrackets.ToString();
            return ArrayFormat.DuplicateKey.ToString();
        }

        private bool AddDontEscapeKey(string attributeName)
        {
            return (Uri.EscapeDataString(attributeName) != attributeName);
        }

        private string ConvertToListName(string key)
        {

            if (string.IsNullOrEmpty(key)) return string.Empty;
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(key, "");
        }

        private string ConvertToListAttributeName(string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            if (key.EndsWith("[]")) return key.Substring(0, key.Length - 2);
            return key;
        }

        private Type ConvertToPropertyType(string value)
        {
            if (!DetectType) return typeof(string);
            var expectedTypes = new List<Type> { typeof(DateTime), typeof(int), typeof(long), typeof(double), typeof(bool) };
            foreach (var type in expectedTypes)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(typeof(string)))
                {
                    try
                    {
                        // You'll have to think about localization here
                        object newValue = converter.ConvertFromInvariantString(value);
                        if (newValue != null)
                        {
                            return type;
                        }
                    }
                    catch
                    {
                        // Can't convert given string to this type
                        continue;
                    }

                }
            }
            return typeof(string);
        }

        private string GetTypeName(Type type)
        {
            string typeName;
            using (var provider = new CSharpCodeProvider())
            {
                var typeRef = new CodeTypeReference(type);
                typeName = provider.GetTypeOutput(typeRef);
            }
            return typeName;
        }

        private string ConvertToPropertyName(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return key;
            if (char.IsLower(key[0]))
                return char.ToUpperInvariant(key[0]) + key.Substring(1);
            return key;
        }
    }
}
