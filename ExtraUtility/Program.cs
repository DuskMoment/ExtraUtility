// See https://aka.ms/new-console-template for more information
using ExtraUtility;

Console.WriteLine("Hello, World!");
Console.WriteLine("This is a gamer momnet");

AdjGraph graph;

graph = new AdjGraph(4, true);

//graph.addVertex();


//graph.addEdge(1, 2, 1);
//graph.deleteVertex(0);
//graph.deleteVertex(2);
//graph.addEdge(1,3,1);

//graph.display();

//Console.WriteLine("Adding a tone of verteces");
//graph.addVertex();
//graph.addVertex();
//graph.addVertex();
//graph.addVertex();
//graph.addVertex();

//graph.display();

Console.WriteLine("clearing graph");

graph.clearGraph();
graph.display();

Console.WriteLine("tesing DFS");

graph.addVertex();
graph.addVertex();
graph.addVertex();
graph.addVertex();
graph.addVertex();

graph.addEdge(0, 1, 1);
graph.addEdge(0,2,1);
graph.addEdge(0, 3, 1);
graph.addEdge(2, 4, 1);
graph.addEdge(3, 2, 1);

List<Node> test = graph.DFS(4, 0);

foreach(var ID in test)
{
    Console.Write(ID.vertexId() + " -> ");
}

List<Node> test2 = graph.BFS(4, 0);

Console.WriteLine("tesing BFS");

foreach (var ID in test2)
{
    Console.Write(ID.vertexId() + " -> ");
}

//Console.WriteLine("deleting a vertice");

//graph.deleteVertex(1);
//graph.display();



