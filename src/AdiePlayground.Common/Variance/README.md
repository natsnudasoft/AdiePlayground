# Variance

In object-oriented programming we can use sub-typing to allow one type to inherit from another.
For example an `Elephant` type may be a subtype of an `Animal` type, which allows us to use
an expression of `Elephant` wherever an expression of `Animal` is expected.

```csharp
void WriteAnimalTypeName(Animal animal)
{
Console.WriteLine(animal.GetType().Name);
}

var elephant = new Elephant();
// Will output "Elephant"
WriteAnimalTypeName(elephant);
```

Variance refers to how sub-typing behaves with more complex types such as `IEnumerable<Elephant>`
or `Action<Elephant>`. Depending on the type of variance the relationship of the sub-typing may be
preserved, reversed or ignored.

Consider the following sub-typing relationships, where for example, a value of type 
`Apple` may be
assigned to a variable of type `Fruit`:

```
Apple   =>  Fruit
Banana  =>  Fruit
Lemon   =>  Fruit
Lime    =>  Fruit
Orange  =>  Fruit
```
### Covariance (`out`)
In a covariant mapping the relationship of the sub-typing is *preserved*; that is, if `X => Y` is
true, then `Class<X> => Class<Y>` is also true. Using C# generic types we can express this
relationship with the `out` keyword.

.NET Framework examples: `Func<out T>`, `IEnumerable<out T>`.

Using the above sub-typing relationships this means we can convert from
`Func<Orange> => Func<Fruit>`, or `IEnumerable<Apple> => IEnumerable<Fruit>`.

This works because we are only taking values `out` of the expression, which will return something
*specific*, so we can treat that value as something more *general*. It makes sense that a sequence
of apples can be given when a sequence of fruit is expected.

### Contravariance (`in`)
In a contravariant mapping the relationship of the sub-typing is *reversed*; that is, if `X => Y`
is true, then `Class<X> <= Class<Y>` is also true. Notice the direction of the relationship is
reversed. Using C# generics we can express this relationship with the `in` keyword.

.NET Framework examples `Action<in T>`, `IComparer<in T>`.

Using the above sub-typing relationships, this means we can convert from
`Action<Fruit> => Action<Lime>`, or `IComparer<Fruit> => IComparer<Banana>`.

This works because we are expecting something to go `in` the expression that is of something more
*general*, so we can give it something more *specific* because the more *specific* type will also
implement all of the methods of the *general* type. Again, it makes sense that something that can
compare two fruits can also compare two bananas.

### Invariance
In an invariant mapping the relationship of the sub-typing is *ignored*; this means that if
`X => Y` is true, then neither `Class<X> => Class<Y>` or `Class<X> <= Class<Y>` is true.

Using the above sub-typing relationships, this means we are unable to convert from
`IInvariant<Lemon> => IInvariant<Fruit>`, or `IInvariant<Fruit> => IInvariant<Lemon>`.

&nbsp;

In C# only generic interfaces and delegates may be covariant or contravariant in their generic type
parameters. It is also possible to mix covariance and contravariance, as can be seen in
`Func<in T, out TResult>`, `Func<in T1, in T2, out TResult>` etc.

Using the above sub-typing relationships, we would be able to convert from
`Func<Fruit, Apple> => Func<Lime, Fruit>`.