// See https://aka.ms/new-console-template for more information
using ExtraUtility;

Console.WriteLine("Hello, World!");
Console.WriteLine("This is a gamer momnet");

AdjGraph graph;

graph = new AdjGraph(4, true);

//graph.addVertex();


graph.addEdge(1, 2, 1);
graph.deleteVertex(0);
graph.deleteVertex(2);
graph.addEdge(1,3,1);

graph.display();

Console.WriteLine("Adding a tone of verteces");
graph.addVertex();
graph.addVertex();
graph.addVertex();
graph.addVertex();
graph.addVertex();

graph.display();


//Console.WriteLine("deleting a vertice");

//graph.deleteVertex(1);
//graph.display();



