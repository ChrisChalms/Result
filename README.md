I've create the ```IResult<T>``` option type, which is based on the ```Option``` (or ```Maybe```) concept from functional programming languages like F#.

```IResult<T>``` comes in three flavours:
- ```ISuccessResult<T>``` represents a successful operation. It has a single non-null property, ```T Value```, which contains is the desired object 
- ```INoneResult``` is a "none" operation. It doesn't have any properties, nor is it typed.
- ```IFailureResult``` is an ```INoneResult``` with the addition of a non-null ```Exception``` property that is populated with the object is created.

The actual implementations of the various ```IResult```s are abstracted away, so the only way to create one is by using the methods in the partial classes ```Result.Return``` and ```Result.Wrap```.

I've also added a few handy extension methods that a lot of other option types have, such as Filter, Fold, Bind, and Map.

## TODO:
- Create a better README