# Query Extensions

Use query extensions to deal with query paramters as strongly typed properties,
 this should help reduce development time and also errors that may happen from manually constructing the query string.
The library uses attributes and reflection to generate query string.

###Features
- Strongly Typed properties instead of manual manipulation.
- Support for three different ArrayFormats inside query.
- Escaping keys and values for you.
- Overriding Name of properties by using attributes, example
  ```
   [QueryParameter("api_key")]
   public string ApiKey { get; set; }
  ```
- You can use ModelGenerator WPF project to generate these properties for you out of the box!
  Refer to Model Generator section below.

###PCL Target
.Net 4.5 | Windows 8 | WPSliverLight 8 | WP 8.1 | Xamarin.Android | Xamarin.iOS | Xamarin.iOS(Classic).


###Install
Currently the project doesn't have Nuget package but I will create it later when I do unit tests on the project, but you can:

1. Pull the source code and build QueryExtensions PCL (along with the license).
2. Copy paste the classes of QueryExtensions PCL inside your solution (along with the license).

###Sample
To see the library in action, pull the source code and check WindowsPhoneRTSample which has a page which calls GetRecentPhotosRequest to show recent photos of Flicker.


###Usage
For Example in windows phone RT if you need to call Flicker Api, method GetRecentPhotos
Url:https://api.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=_YourKey_&per_page=40&page=1&extras=geo%2Cdescription&format=json&nojsoncallback=1

You can create model for the query paramters:
```
 public class RecentQueryParamters
    {
        [QueryParameter("method")]
        public string Method { get; set; }

        [QueryParameter("api_key")]
        public string ApiKey { get; set; }
        
        [QueryParameter("page")]
        public int Page { get; set; }
        
        [QueryParameter("per_page")]
        public int PerPage { get; set; }

        [QueryArray("extras", ArrayFormat = ArrayFormat.SingleKey)]
        public List<string> Extras { get; set; }
        ...
    }
```
Simply you can construct the query like this.

```
var extras = new List<string> { "geo", "description" };
var parameters = new RecentQueryParamters()
                {
                    Method = "flickr.photos.getRecent",
                    ApiKey = "_YourKey_",
                    Page=1,
                    PerPage=40,
                    Extras=extras
                };
                
 string baseUrl= "https://api.flickr.com/services/rest/";  
 string fullUrl = baseUrl.AppendQueryString(parameters);
 
 var httpClient = new HttpClient();
 var getResponse = await httpClient.GetAsync(fullUrl);
```

- Primitives like long, bool,... and strings can be decorated by optional QueryParameter attribute to specify name of paramter, if the attribute is omitted, the property name will be used.

- In case of list of values in query paramter, the optional QueryArray attribute can be used to specify ArrayFormat.

####Available Array Formats:

```
 public enum ArrayFormat
    {
        /// <summary>
        /// Example output: "cars[]=Saab&cars[]=Audi"
        /// </summary>
        DuplicateKeyWithBrackets,
        /// <summary>
        /// Example output: "cars=Saab&cars=Audi"
        /// </summary>
        DuplicateKey,
        /// <summary>
        /// Example output "cars=Saab,Audi"
        /// </summary>
        SingleKey

    }
```

###TODO
1. Unit Test project.
2. Nuget package.

###Issues
Please report any issue you find or contact if you have enhancements or ideas.

#Model Generator

This WPF project generates Strongly Typed properties from query url, All you need to to do is to copy the url and press generate.

It will try to detect types, you can disable this feature by unchecking 'Detect Type' checkbox.

![Model generator screenshot](https://raw.githubusercontent.com/AhmedRashad/Query-Extensions_Win_Xamarin/master/Images/ModelGenerator.JPG "Model generator WPF application")

To use this project, you will need visual studio (Mine 2015 community).

###TODO
Unit Testing.
Convert Model Generator project to web hosted solution to make it easy for anyone to generate classes.

###License
GNU v3, check license file under main repositry.
