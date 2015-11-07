using System;
using System.Linq;
using System.Text;
using System.Windows;
using ModelsGenerator.Common;
using ModelsGenerator.Helpers;

namespace ModelsGenerator.ViewModels
{
    public class RequestGeneratorViewModel : BindableBase
    {
        #region Fields

        private readonly ParametersCodeGenerator _parametersCodeGenerator;
        private readonly RequestCodeGenerator _requestCodeGenerator;
        #endregion

        #region Properties
        private String _requestUrl;
        public String RequestUrl
        {
            get { return _requestUrl; }
            set
            {
                if (SetProperty(ref _requestUrl, value))
                {
                    var requestName = ExtractRequestName();
                    EntityName = String.IsNullOrEmpty(requestName) ? "QueryParamters" : requestName + "Paramters";
                    var requestSegments = ExtractRequestSegments();
                    RequestSegments = String.IsNullOrEmpty(requestSegments) ? "INSERT_REQUEST" : requestSegments;

                }
            }
        }

        private String _entityName;
        public String EntityName
        {
            get { return _entityName; }
            set { SetProperty(ref _entityName, value); }
        }

        private String _requestSegments;
        public String RequestSegments
        {
            get { return _requestSegments; }
            set { SetProperty(ref _requestSegments, value); }
        }

        private bool _capitalizeFirstCharacter;
        public bool CapitalizeFirstCharacter
        {
            get { return _capitalizeFirstCharacter; }
            set { SetProperty(ref _capitalizeFirstCharacter, value); }
        }

        private String _queryParametersResult;
        public String QueryParametersResult
        {
            get { return _queryParametersResult; }
            set { SetProperty(ref _queryParametersResult, value); }
        }

        private String _requestCodeResult;
        public String RequestCodeResult
        {
            get { return _requestCodeResult; }
            set { SetProperty(ref _requestCodeResult, value); }
        }

        private String _status;
        public String Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private bool _detectType;
        public bool DetectType
        {
            get { return _detectType; }
            set { SetProperty(ref _detectType, value); }
        }

        #endregion

        #region Initialization

        public RequestGeneratorViewModel(ParametersCodeGenerator parametersCodeGenerator, RequestCodeGenerator requestCodeGenerator)
        {
            _parametersCodeGenerator = parametersCodeGenerator;
            _requestCodeGenerator = requestCodeGenerator;
            GenerateCommand = new ExtendedCommand(Generate);
            CopyParametersCommand = new ExtendedCommand(CopyParameters);
            DetectType = true;
        }
        #endregion

        #region Commands

        public ExtendedCommand GenerateCommand { get; set; }
        public ExtendedCommand CopyParametersCommand { get; set; }
        #endregion

        #region Methods

        public void Generate()
        {
            if (String.IsNullOrEmpty(RequestUrl))
            {
                Status = "Unable to parse Request";
                return;
            }
            //Generate Parameters Code
            Status = "";
            String query = GetQueryPart();
            _parametersCodeGenerator.DetectType = DetectType;
            QueryParametersResult = _parametersCodeGenerator.GenerateParametersCode(query, EntityName);
            Status = _parametersCodeGenerator.Status;
        }

        private String ExtractRequestName()
        {
            try
            {
                String name = new Uri(RequestUrl).Segments.Last();
                name = name.Trim('\\','/');
                if (Char.IsLower(name[0])) name = Char.ToUpperInvariant(name[0]) + name.Substring(1);
                return name;
            }
            catch (Exception)
            {
                return null;
            }

        }


        private string ExtractRequestSegments()
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                var segments = new Uri(RequestUrl).Segments;
                if (segments[0] != "/") builder.Append(segments[0]);
                for (int i = 1; i < segments.Length; i++)
                {
                    builder.Append(segments[i]);
                }
                return builder.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }


        private String GetQueryPart()
        {
            String querystring = String.Empty;

            int iqs = RequestUrl.IndexOf('?');
            if (iqs == -1)
            {
                return String.Empty;
            }
            if (iqs >= 0)
            {
                querystring = (iqs < RequestUrl.Length - 1) ? RequestUrl.Substring(iqs + 1) : String.Empty;
            }
            return querystring;
        }

        public void CopyParameters()
        {
            Clipboard.SetText(QueryParametersResult);
        }

        #endregion
    }
}
