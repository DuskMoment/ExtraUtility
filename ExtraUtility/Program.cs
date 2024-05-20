// See https://aka.ms/new-console-template for more information
using ExtraUtility;

Console.WriteLine("Hello, World!");
Console.WriteLine("This is a gamer momnet");

AdjGraph graph;

graph = new AdjGraph(4, true);

graph.addEdge(1, 3, 1);
graph.addEdge(1, 2, 1);

graph.display();

graph.deleteEdge(1,3);

graph.display();

