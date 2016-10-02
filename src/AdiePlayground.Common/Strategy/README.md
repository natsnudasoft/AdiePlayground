# Strategy

The Strategy pattern is a design pattern that encapsulates each algorithm within a family of
algorithms and allows them to be interchangeable at runtime; this means the algorithms can vary
independently of the clients using them.

![Strategy pattern](StrategyPattern.png)


The strategy pattern is used to define *how* something should be done, and is used by another object
to provide specific algorithms at runtime. In this example a sort strategy provides a family of
different sorting algorithms which could be selected at runtime based on criteria such as the size
of the collection to be sorted etc. Further sort strategies can easily be added independent of any
other clients.