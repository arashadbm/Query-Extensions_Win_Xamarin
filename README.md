# Query Extensions

Use query extensions to deal with query paramters as strongly typed properties,
 this should help reduce development time and also errors that may happen from manually constructing the query string.
The library uses attributes and reflection to generate query string.


###Install
Currently the project doesn't have Nuget package but I will create it later when I do unit tests on the project, but you can:

1. Pull the source code and build QueryExtensions PCL (along with the license).
2. Copy paste the classes of QueryExtensions PCL inside your solution (along with the license).


###Usage


###TODO
1. Unit Test project.
2. Nuget package.

Please report any issue you find or contact if you have enhancements or ideas.

#Model Generator

This WPF project generates Strongly Typed properties from query url, All you need to to do is to copy the url and press generate.

It will try to detect types, you can disable this feature by unchecking 'Detect Type' checkbox.

![Model generator screenshot](https://raw.githubusercontent.com/AhmedRashad/Query-Extensions_Win_Xamarin/master/Images/ModelGenerator.JPG "Model generator WPF application")

To use this project, you will need visual studio (Mine 2015 community).
