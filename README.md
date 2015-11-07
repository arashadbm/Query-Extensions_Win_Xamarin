# Query Extensions

Use query extensions to deal with query paramters as strongly typed properties,
 this should help reduce development time and also errors that may happen from manually constructing the query string.
The library uses attributes and reflection to generate query string.

Later I'll add an example here also a CodeGenerator which generate these classes for your from url.

#Model Generator

This WPF project generates Strongly Typed properties from query url, All you need to to do is to copy the url and press generate.

It will try to detect types, you can disable this feature by unchecking 'Detect Type' checkbox.

![Model generator screenshot](https://raw.githubusercontent.com/AhmedRashad/Query-Extensions_Win_Xamarin/master/Images/ModelGenerator.JPG "Model generator WPF application")

To use this project, you will need visual studio (Mine 2015 community).
