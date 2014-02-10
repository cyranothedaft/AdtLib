using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using adt.lib.Graphs;
using adt.lib.Graphs.Algorithms;
using NUnit.Framework;



namespace adt.lib.test.Graphs {
   /// <summary>
   /// Runs tests on a 1-vertex 1-edge graph with this structure:  x -> x
   /// </summary>
   public class GraphTester_TrivialSelfLoop1 : GraphTypeTester {
      protected override Graph<string> GetGraph() {
         var graph = new Graph<string>();
         _x = graph.AddVertex("x");
         _edge = graph.AddEdge(_x, _x);
         return graph;
      }


      private GraphVertex<string> _x;
      private GraphEdge<string> _edge;


      #region Graph tests
      [Test]
      public override void Vertexes_Get() {
         var vertexes = TestGraph.Vertexes;

         Assert.IsNotNull(vertexes);
         var array = vertexes.ToArray();
         Assert.AreEqual(1, array.Length);
         Assert.AreSame(_x, array[0]);
      }


      [Test]
      public override void Edges_Get() {
         var edges = TestGraph.Edges;

         Assert.IsNotNull(edges);
         Assert.AreEqual(1, edges.Count());
         var edge = edges.First();
         Assert.AreSame(_edge, edge);
         Assert.AreSame(_x, edge.From);
         Assert.AreSame(_x, edge.To);
      }


      [Test]
      public override void FindVertex() {
         TestFindVertex("x", _x);
      }


      [Test]
      public override void Neighbors_Edge() {
         TestEdgesIn_ByInstance(_x, _edge);
         TestEdgesOut_ByInstance(_x, _edge);
      }


      [Test]
      public override void Neighbors_Vertex() {
         TestVertexesIn(_x, _x);
         TestVertexesOut(_x, _x);
      }

      #endregion Graph tests


      #region Algorithm tests
      [Test]
      public override void DepthFirstSearch_Trace() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         Assert.AreEqual("(x x)", search.Trace);
      }


      [Test]
      public override void DepthFirstSearch_EdgeTypes() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         TestEdges(search.EdgesOfType(DfsEdgeType.Tree));
         TestEdges(search.EdgesOfType(DfsEdgeType.Back), new[] { "x", "x" });
         TestEdges(search.EdgesOfType(DfsEdgeType.Forward));
         TestEdges(search.EdgesOfType(DfsEdgeType.Cross));
      }


      [Test]
      public override void DepthFirstSearch_Cycles() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         var cycles = search.Cycles.ToArray();
         Assert.AreEqual(1, cycles.Length);

         TestEdges(cycles[0].Edges,
                   new[] { "x", "x" });
      }
      #endregion Algorithm tests
   }
}
