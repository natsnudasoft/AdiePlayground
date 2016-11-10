# Template Method

The Template Method pattern is a design pattern that defines the skeleton of an algorithm; letting
subclasses redefine certain steps of the algorithm without changing the algorithm's structure. 

![Template Method pattern](TemplateMethodPattern.png)

In the Template Method pattern, parts of an algorithm that don't change are implemented once in an
abstract base class, and parts that are customisable are given either a default implementation that
*may* be overridden in a concrete derived class, or an empty abstract implementation that *must* be
overridden in a concrete derived class.