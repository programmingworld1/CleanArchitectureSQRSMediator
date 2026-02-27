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

- public string Name { get; set; } = null!;
only silences the compiler but that is even worse.. now you basically remove the warning that tells you to watch out.

Note: so why not just 
public class Person
{
    public string Name { get; }

    public Person(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}

Well, this you use for domain objects because you want it to have a correct state.

But for DTOs, incorrect state is allowed, because validation still needs to happen.
And there can be binding errors when you get a json from rest api for example, it can still work but much boiler plate.
Plus api versioning, it can break, if you add a new property to the constructor, the callers need to add the property to the request or else the code will not work (see, there is no age.. so how will it map):
public CreatePersonDto(string name, int age)
{ "name": "John" }

So this means you have to use required, so that you always will assign a value, and then you can use fluent validation to validate really if it is null (with required you can still assign null, so its not validation)

Note: But with required you can still assign a null value, so whats up wih that?: de compiler should not think a null value assigned is wrong because backward compatibility.

Note: EF core does not do anything with required, its the same as normal string, so it doesnt interpret it differently. But if you have a null value in the DB, the db will still assign it to the property when you get the data even if it has required in it, because required is static-analysis. 
and json binding and automapper can still bind a null value to a required property because there is no static analysis.









2: init / record

- init:
public string Genre { get; init; }
Init does NOT solve the nullable issue. It solves a different issue. 
It controls when the property is set, not wheter it is set. The issue it solves:
    - Can be changed anytime
    - Object can silently enter an invalid state
With init you can only set the property via constructor or via object initializer. 
The reason this is handy because you dont want anyone to change the state at anytime, for example for DTO objects because objects can become invalid later. also handy for configuration classes.

So why not use?
public string Genre { get; }
If the property has no setter, the property can only be set inside the object's constructor. 
If the property has an init setting, the property can be set by another piece of code in an object initializer block.

- Record:
The biggest difference between a record and a class is comparing instances of these objects.

public record Person(string Name);
var p1 = new Person("Ali");
var p2 = new Person("Ali");
Console.WriteLine(p1 == p2); // ✅ true

public class Person
{
    public string Name { get; }

    public Person(string name)
    {
        Name = name;
    }
}
var p1 = new Person("Ali");
var p2 = new Person("Ali");

Console.WriteLine(p1 == p2); // ❌ false

Side-note: records are not per definition immutable, if you define it likes this (they are)
public record Person(string Name);
but if you define it like this (they are not)
record Person
{
    public string Name { get; set; }
}







Question: why does c# not force an error when string Name is used and not assigned during object creation?
Because c# has allowed for tens of years that reference types can be null. And it will break existing apps (millions) when this is not allowed anymore.

Question: What if you map objects with "required", mappers use reflection so it doesnt guarantee during compile-time that it shows an error. How to deal with this?

in C# version 8 they introduced nullable string "string?", but string and string? are not enforced during compile-time via errors, its only giving a warnings. For string? it will only tell to do a null check before, for example, you print it. for string, it will show the warning in the property as already mentioned.







