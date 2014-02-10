using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using adt.lib.Graphs;
using adt.lib.Graphs.Algorithms;
using NUnit.Framework;



namespace adt.lib.test {
   public abstract class GraphTypeTester {
      public abstract Graph<string> ConstructGraph();
   }



   /// <summary>
   /// Runs tests on a 2-vertex 1-edge graph with this structure:  v1 -> v2
   /// </summary>
   [TestFixture]
   public class GraphTester_Basic2 : GraphTypeTester {
      public override Graph<string> ConstructGraph() {
         _graph = new Graph<string>();
         _v1 = _graph.AddVertex("v1");
         _v2 = _graph.AddVertex("v2");
         _edge = _graph.AddEdge(_v1, _v2);
         return _graph;
      }


      private Graph<string> _graph; 
      private GraphNode<string> _v1, _v2;
      private GraphEdge<string> _edge;


      [Test]
      public void Vertexes_Get() {
         ConstructGraph();
         var vertexes = _graph.Vertexes.Cast<GraphNode<string>>();

         Assert.IsNotNull(vertexes);
         var array = vertexes.OrderBy(v => v.Value).ToArray();
         Assert.AreEqual(2, array.Length);
         Assert.AreSame(_v1, array[0]);
         Assert.AreSame(_v2, array[1]);
      }


      [Test]
      public void Edges_Get() {
         ConstructGraph();
         var edges = _graph.Edges;

         Assert.IsNotNull(edges);
         Assert.AreEqual(1, edges.Count());
         var edge = edges.First();
         Assert.AreSame(_edge, edge);
         Assert.AreSame(_v1, edge.From);
         Assert.AreSame(_v2, edge.To);
      }


      //[Test]
      //public void Construct_Relative_Basic() {
      //   Graph<string> graph = new Graph<string>();
      //   GraphNode<string> v1 = graph.AddVertex("v1"),
      //                   v2 = graph.AddVertex("v2");
      //   v1.AddEdgeTo(v2);

      //   Assert.AreEqual(2, graph.Vertexes.Count());
      //   Assert.AreEqual(1, graph.Edges.Count());
      //}


      [Test]
      public void FindVertex() {
         ConstructGraph();

         var foundV1 = _graph.FindVertex("v1", string.Equals);
         Assert.IsNotNull(foundV1);
         Assert.AreEqual("v1", foundV1.Value);

         var foundV2 = _graph.FindVertex("v2", string.Equals);
         Assert.IsNotNull(foundV2);
         Assert.AreEqual("v2", foundV2.Value);
      }


      [Test]
      public void Neighbors_Edge() {
         ConstructGraph();

         var v1EdgesIn = _v1.EdgesIn;
         Assert.IsNotNull(v1EdgesIn);
         Assert.IsFalse(v1EdgesIn.Any());

         var v1EdgesOut = _v1.EdgesOut;
         Assert.IsNotNull(v1EdgesOut);
         Assert.AreEqual(1, v1EdgesOut.Count());
         Assert.AreSame(_edge, v1EdgesOut.First());
         Assert.AreSame(_edge.From, v1EdgesOut.First().From);
         Assert.AreSame(_edge.To, v1EdgesOut.First().To);

         var v2EdgesIn = _v2.EdgesIn;
         Assert.IsNotNull(v2EdgesIn);
         Assert.AreEqual(1, v2EdgesIn.Count());
         Assert.AreSame(_edge, v2EdgesIn.First());
         Assert.AreSame(_edge.From, v2EdgesIn.First().From);
         Assert.AreSame(_edge.To, v2EdgesIn.First().To);

         var v2EdgesOut = _v2.EdgesOut;
         Assert.IsNotNull(v2EdgesOut);
         Assert.IsFalse(v2EdgesOut.Any());
      }


      [Test]
      public void Neighbors_Vertex() {
         ConstructGraph();

         var nodesToV1 = _v1.VertexesIn;
         Assert.IsNotNull(nodesToV1);
         Assert.IsFalse(nodesToV1.Any());

         var nodesFromV1 = _v1.VertexesOut;
         Assert.IsNotNull(nodesToV1);
         Assert.AreEqual(1, nodesFromV1.Count());
         Assert.AreSame(_v2, nodesFromV1.First());

         var nodesToV2 = _v2.VertexesIn;
         Assert.IsNotNull(nodesToV2);
         Assert.AreEqual(1, nodesToV2.Count());
         Assert.AreSame(_v1, nodesToV2.First());

         var nodesFromV2 = _v2.VertexesOut;
         Assert.IsNotNull(nodesFromV2);
         Assert.IsFalse(nodesFromV2.Any());
      }


      [Test]
      public void DepthFirstSearch_Trace() {
         ConstructGraph();

         DepthFirstSearch<string> search = new DepthFirstSearch<string>(_graph);
         search.PerformSearch();

         Assert.AreEqual("(v1(v2))", search.Trace);
      }
   }
}
