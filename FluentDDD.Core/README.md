# FluentDDD Core

This API provides implementations to help the developer to follow the domain driven design.

It expose ways to implement custom `Entity` and `ValueObject`. 

## Value Objects

The **value objects**, as described by **Eric Evans** in _Domain Driven Design_ book, are objects that represents its values.
A Value Object is differentiable from another Value Object by comparing thirs attributes. 
The Value Object should aways be immutable, cause if its attributes change, the representation it is from another Value Object. 

**_Exemple:_** An Value Object Color has the attributes red, green and blue, if we change one of the attributes, the Value Object color changes to another Value Object.

Because it is immutable, it is useful provide a way to the Value Object to copy itself.

### Customizing Value Objects

To create a value object following our API, you just need to extend the class `ValueObject` and overrides some of its methods:

```c#
public sealed class Cpf : ValueObject<Cpf>
{

    private readonly string _code;

    public Cpf(string code)
    {
        _code = code;
    }

    protected override bool Equalsore(ValueObject other)
    {
        return _code == cpf._code;
    }

    protected override int GetHashCodeCore()
    {
        return GetType().GetHashCode() * new Random(_code.GetHashCode()).Next();
    }

    public override string ToString()
    {
        return _code;
    }
}

```

> We recommend to avoid inherit from Value Objects, using aways the `sealed` modificator, so they cannot have child that overrides his methods.

The internal attribute of the `ValueObject` is **always** `readonly`, and don't need to have Properties to Get or Set. They should be passed by the constructor, and have other yays to expose their attributes. Like in cpf case, a `Formatted()` and `Unformatted()` methods.

You will need to override two methods: `EqualsCore()` and `GetHashCore()` with the rule you need to assegure the consistence of your value object.

#### The EqualsCore method

The equality of an Value Object is aways the state of its attributes, so you just need to check if their are the same:

```c#
    protected override bool Equals(ValueObject other)
    {
        return _code == cpf._code;
    }
```

#### The GetHashCodeCore method

We provide a way to the value object serves its own hash code:

```c#
    protected override int GetHashCodeCore()
    {
        return GetType().GetHashCode() * new Random(_code.GetHashCode()).Next();
    }
```

> In our case, we use a `Random` to create a hash, but serving always the same HashCode for a same `_code` attribute.

## Entity

Different from the `ValueObject`, a `Entity` is **always** comparable by its `Identity`.

An `Entity` is an object that has an continuous life cycle, so its can have different states. It means, two entities instances can coexist with different attributes, but be the same if the identity be the same.

**_Example:_** An Person can be considerate an Entity. It can change its name attribute when married, but continues being the same person as before.

The `Identity` attribute, by other hand, can't be changed, because if we change an Identity of an Entity. We creating or changing to new one. So, in our API, a Identity is always an `ValueObject`.

### Customizing Entities

To creates an personalized Entity you will just need to extend our `Entity`, passing in the generics param its Identity:

```c#
private class Person : Entity<Cpf>
{
    public Entidade(Cpf identity, string name) : base(identity)
    {
        Name = name;
    }

    public string Name { get; set; }

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return base.ToString() + $"Person [Name = {Name}]";
    }
}

```

As the Person Entity have the Cpf Value Object as its Identity, we need to pass it in the constructor. In this case we also pass an name attribute:

```c#
    public Entidade(Cpf identity, string name) : base(identity)
    {
        Name = name;
    }
```

## Others implementations

We also provides a way to customize formatters for an `ValueObjects` string based type, using the class `Formatter` and use it in a `ValueObject` implementing the `IFormatter` interface.

### The Formatter

The `Formatter` class implements operations to format/unformat an `string` in a specific pattern. It have to be extended by the actual formatter:

```c#
    public class CpfFormatter : Formatter
    {
        public CpfFormatter()
            : base(
                new Regex(@"^(\d{3})[.](\d{3})[.](\d{3})-(\d{2})$", RegexOptions.Compiled | RegexOptions.IgnoreCase),
                new Regex(@"^(\d{3})(\d{3})(\d{3})(\d{2})$", RegexOptions.Compiled | RegexOptions.IgnoreCase),
                "$1.$2.$3-$4",
                "$1$2$3$4"
            )
        {
        }
    }
```

> You will just need to pass the formatted pattern, the unformatted pattern, the string formatted pattern for replacement and the string unformatted pattern for replacement in the constructor.

Then the formatter will have the follower methods:

- Format() : Formats the string to the formatted pattern.
- Unformat() : Unformats the string to the unformatted pattern.
- IsFormatted() : Checks if the string passed is considerate formatted for this formatter
- IsUnformatted() : Checks if the string passed is considerate unformatted for this formatter

> **Note**: To use `Format()` and `Unformat()` methods, the string have to be `IsFormatted()` or `IsUnformatted()`. Any format different from that will throw an error.

### The IFormattable

The `ValueObject` that can have his attribute formatted should implement the interface `IFormattable`.

It provides the signature contract to expose the methods `Formatted()` and `Unformatted()`:

```c#
    public sealed class Cpf : ValueObject<Cpf>, IFormattable<string>
    {
        private readonly string _code;

        private readonly Formatter _formatter;

        public Cpf(string code)
        {
            _formatter = new CpfFormatter();
            _code = _formatter.Unformat(code);
        }

        public string Formatted()
        {
            return _formatter.Format(_code);
        }

        public string Unformatted()
        {
            return _formatter.Unformat(_code);
        }
    }
```
