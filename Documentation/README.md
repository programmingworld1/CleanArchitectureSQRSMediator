1: Nullable reference types
Issue: Non-nullable property must contain a non-null value when exiting constructor. 
Consider adding the 'required' modifier or delcaring the property as nullable. 
This warning shows when you have Nullable Reference Types enables during static analysis.

Why: Nullable reference types are a group of features that minimize the likelihood that your code causes the runtime to throw System.NullReferenceException. 
This is giving warnings precisely to increasy your confidence in your code. Without it, it's easier to get null references errors down the road.

Example-issue: 
public class Human {
    public string Name { get; set; }
}

Explanation: string is not the same as string?, if you use "string" you are basically saying everyone can assume that Name is never null.
Because imagine you have a line that prints the name: r.Name.Length, it will throw an exception during run-time. 
Compile-time there is no check if an object during creation has a value assigned to Name.

Possible solutions:

- required: 
public class Human {
    public required string Name { get; set; }
}
now when we try to create a Human like: var human = new Human(), it will show an error during compile-time, thus it will not build. You should add a value to "Name" either in the constructor or via Object initializer.
var human = new Human("name")
var human = new Human() {
    Name = "name"
}

- init:
public string Genre { get; init; }
Init does NOT solve the nullable issue. It solves a different issue. 
It controls when the property is set, not wheter it is set. The issue it solves:
    - Can be changed anytime
    - Object can silently enter an invalid state
With init you can only set the property via constructor or via object initializer. 
The reason this is handy because you dont want anyone to change the state at anytime, for example for DTO objects because objects can become invalid later. also handy for configuration classes.

- public string Name { get; set; } = null!;
only silences the compiler but that is even worse.. now you basically remove the warning that tells you to watch out.


- Record:

Question: why does c# not force an error when string Name is used and not assigned during object creation?
Because c# has allowed for tens of years that reference types can be null. And it will break existing apps (millions) when this is not allowed anymore.

Question: What if you map objects with "required", mappers use reflection so it doesnt guarantee during compile-time that it shows an error. How to deal with this?

in C# version 8 they introduced nullable string "string?", but string and string? are not enforced during compile-time via errors, its only giving a warnings. For string? it will only tell to do a null check before, for example, you print it. for string, it will show the warning in the property as already mentioned.







