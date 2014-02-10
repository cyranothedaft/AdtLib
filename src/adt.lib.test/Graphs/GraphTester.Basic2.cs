using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using adt.lib.Graphs;
using adt.lib.Graphs.Algorithms;
using NUnit.Framework;



namespace adt.lib.test.Graphs {
   /// <summary>
   /// Runs tests on a 2-vertex 1-edge graph with this structure:  v1 -> v2
   /// </summary>
   public class GraphTester_Basic2 : GraphTypeTester {
      protected override Graph<string> GetGraph() {
         var graph = new Graph<string>();
         _v1 = graph.AddVertex("v1");
         _v2 = graph.AddVertex("v2");
         _edge = graph.AddEdge(_v1, _v2);
         return graph;
      }


      private GraphVertex<string> _v1, _v2;
      private GraphEdge<string> _edge;


      #region Graph tests
      [Test]
      public override void Vertexes_Get() {
         var vertexes = TestGraph.Vertexes;

         Assert.IsNotNull(vertexes);
         var array = vertexes.ToArray();
         Assert.AreEqual(2, array.Length);
         Assert.AreSame(_v1, array[0]);
         Assert.AreSame(_v2, array[1]);
      }


      [Test]
      public override void Edges_Get() {
         var edges = TestGraph.Edges;

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
      //   GraphVertex<string> v1 = graph.AddVertex("v1"),
      //                   v2 = graph.AddVertex("v2");
      //   v1.AddEdgeTo(v2);

      //   Assert.AreEqual(2, graph.Vertexes.Count());
      //   Assert.AreEqual(1, graph.Edges.Count());
      //}


      [Test]
      public override void FindVertex() {
         TestFindVertex("v1", _v1);
         TestFindVertex("v2", _v2);
      }


      [Test]
      public override void Neighbors_Edge() {
         TestEdgesIn_ByInstance(_v1);
         TestEdgesOut_ByInstance(_v1, _edge);

         TestEdgesIn_ByInstance(_v2, _edge);
         TestEdgesOut_ByInstance(_v2);
      }


      [Test]
      public override void Neighbors_Vertex() {
         TestVertexesIn(_v1);
         TestVertexesOut(_v1, _v2);

         TestVertexesIn(_v2, _v1);
         TestVertexesOut(_v2);
      }

      #endregion Graph tests


      #region Algorithm tests
      [Test]
      public override void DepthFirstSearch_Trace() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         Assert.AreEqual("(v1 (v2 v2) v1)", search.Trace);
      }


      [Test]
      public override void DepthFirstSearch_EdgeTypes() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         TestEdges(search.EdgesOfType(DfsEdgeType.Tree), new[] { "v1", "v2" });
         TestEdges(search.EdgesOfType(DfsEdgeType.Back));
         TestEdges(search.EdgesOfType(DfsEdgeType.Forward));
         TestEdges(search.EdgesOfType(DfsEdgeType.Cross));
      }


      [Test]
      public override void DepthFirstSearch_Cycles() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         Assert.IsFalse(search.Cycles.Any());
      }
      #endregion Algorithm tests
   }
}
