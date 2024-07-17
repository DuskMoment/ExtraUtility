// See https://aka.ms/new-console-template for more information
using ExtraUtility.Graphs;
using System.Diagnostics;


void testFunction(List<string> test)
{
    if (test.Count != 0)
    {
        Console.WriteLine("FOUND PATH");
    }
    else
    {
        Console.WriteLine("DID NOT FIND PATH");
    }

}

Console.WriteLine("Hello, World!");
Console.WriteLine("This is a gamer momnet");

AdjGraph graph;

graph = new AdjGraph(4, true);

RelatoinshipGraph<string> graph2 = new RelatoinshipGraph<string>(true);

graph2.addVertex("A");
graph2.addVertex("B");
graph2.addVertex("C");
graph2.addVertex("D");

//connect graph

//graph2.addEdge("A", "C", 11);
graph2.addEdge("C", "B", 10);

//graph2.addEdge("A", "D", 12);



graph2.display();

List<string> test = graph2.DFS("B", "A");

testFunction(test);

//graph2.addVertex("A");
//graph2.addVertex("B");
//graph2.addVertex("C");
//graph2.addVertex("D");

//graph2.addEdge("A", "B", 1);

//graph2.addEdge("A", "C", 1);
//graph2.addEdge("C", "B", 1);

//graph2.display();

//Console.WriteLine("deleteing edge");
//graph2.deleteEdge("C", "B");
//graph2.deleteEdge("A", "C");

//graph2.addEdge("C", "A", 1);

//graph2.display();

//Console.WriteLine("deleteing vertex");
//graph2.deleteVertex("A");

//graph2.display();

//Console.WriteLine("DFS search");

//graph2.addVertex("A");
//graph2.addVertex("B");
//graph2.addVertex("C");
//graph2.addVertex("D");
//graph2.addVertex("E");

//graph2.addEdge("A", "B", 1);
//graph2.addEdge("A", "C", 1);
//graph2.addEdge("A", "D", 1);
//graph2.addEdge("C", "E", 1);
//graph2.addEdge("D", "C", 1);

//List<string> test = graph2.DFS("A", "E");

//foreach (var ID in test)
//{
//    Console.Write(ID + " -> ");
//}

//graph2.display();

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

//Console.WriteLine("clearing graph");

//graph.clearGraph();
//graph.display();

//Console.WriteLine("tesing DFS");

//graph.addVertex();
//graph.addVertex();
//graph.addVertex();
//graph.addVertex();
//graph.addVertex();

//graph.addEdge(0, 1, 1);
//graph.addEdge(0,2,1);
//graph.addEdge(0, 3, 1);
//graph.addEdge(2, 4, 1);
//graph.addEdge(3, 2, 1);

//List<Node> test = graph.DFS(4, 0);

//foreach(var ID in test)
//{
//    Console.Write(ID.vertexId() + " -> ");
//}

//List<Node> test2 = graph.BFS(4, 0);

//Console.WriteLine("tesing BFS");

//foreach (var ID in test2)
//{
//    Console.Write(ID.vertexId() + " -> ");
//}

//Console.WriteLine("deleting a vertice");

//graph.deleteVertex(1);
//graph.display();



