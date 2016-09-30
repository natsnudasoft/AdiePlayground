# Observer

The Observer pattern is a design pattern in which an object (called the subject, or an observable)
maintains a list of dependant objects (called observers), and notifies them of any changes to its
state.

![Observer pattern](ObserverPattern.png)

This example shows the 'classic' Observer pattern; however in .NET, this pattern is usually
implemented using event handlers. Using libraries such as
[ReactiveX](http://reactivex.io/intro.html), the Observer pattern can be extended and can give you a
lot of power over regular event handling in .NET.