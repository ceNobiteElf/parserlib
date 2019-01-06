# ParserLib

ParserLib is a C# library containing implementations of parsers for various text-based data interchange formats. The project started due to frustration with a certain programming environment that will not be named's substandard JSON library.

## ParserLib.Json
Currently ParserLib fully supports JSON parsing from strings and files and is standards compliant. There are common deviations that are supported, such as the use of single quotation marks (`'`'), however date parsing is wholly unsupported. 

Note that object serialization and deserialization is still very rudimentary and not fully supported.

### Usage
JSON values are stored in wrapper objects which all inherit from `JsonElement`. These wrappers are:

* `JsonArray`
* `JsonObject`
* `JsonBool`
* `JsonNumber`
* `JsonString`

These wrappers contain a number of convenience members as well as access to the underlying value. Explicit and implicit casts are available for ease of use.

The `JsonParser` class can be used to parse JSON, and will output the result as a `JsonEelement`, however generic typed functions are available if you are aware of what type the root is. Additional options can be passed to the parser to tell it how to behave in certain situations. For example:

```c#
string jsonString = "{'Name':'Tester'}";
var parserOptions = new JsonReaderOptions { DuplicateKeyBehaviour = DuplicateKeyBehaviour.Ignore };

var jsonElement = JsonParser.ParseFromString(jsonString, parserOptions);

Console.WriteLine(jsonElement["Name"]);
// OUTPUT: Tester
```

JSON objects and arrays can be stringified or written to file through the `JsonWriter` class, only the root level element needs to be provided along with any options. For example:

```c#
var options = new JsonWriterOptions { PrettyPrint = true, TabWidth = '2' };

string result = JsonWriter.WriteToString(json, options);
```

