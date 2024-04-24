# Usage
Reference a class file in the library by adding 
~~~csharp
using VRregVast.StandardLibrary.{ClassName}
~~~
to the start of a script, or reference methods directly using 
~~~csharp
VRregVast.StandardLibrary.{ClassName}.{Method_Name}()
~~~

# Extension
Add new methods to a relevant class file that already exists in the Standard Library folder or create a new class file for it.
For example, the method New_Scene() belongs to the class SceneManagement 

Class files should be structured as such:
~~~csharp
namespace VRregVast.StandardLibrary {
  public static class {ClassName} {
    public static class {Method_One}() {}
    public static class {Method_Two}() {}
  }
}
~~~
