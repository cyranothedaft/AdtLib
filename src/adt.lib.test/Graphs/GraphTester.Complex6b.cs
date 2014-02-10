using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using adt.lib.Graphs;
using adt.lib.Graphs.Algorithms;
using NUnit.Framework;



namespace adt.lib.test.Graphs {
   /// <summary>
   /// Runs tests on an 6-vertex 8-edge graph that contains === all four types of edges: Tree, Back, Forward, and Cross.
   /// This example comes from the book Introduction to Algorithms, Second Edition, by T. H. Cormen, et al., Figure B.2(a) on p. 1081.
   /// </summary>
   public class GraphTester_Complex6b : GraphTypeTester {
      protected override Graph<string> GetGraph() {
         var graph = new Graph<string>();
         _1 = graph.AddVertex("1");
         _2 = graph.AddVertex("2");
         _3 = graph.AddVertex("3");
         _4 = graph.AddVertex("4");
         _5 = graph.AddVertex("5");
         _6 = graph.AddVertex("6");
         graph.AddEdge(_1, _2);
         graph.AddEdge(_2, _2);
         graph.AddEdge(_2, _4);
         graph.AddEdge(_2, _5);
         graph.AddEdge(_4, _1);
         graph.AddEdge(_4, _5);
         graph.AddEdge(_5, _4);
         graph.AddEdge(_6, _3);
         return graph;
      }


      private GraphVertex<string> _1, _2, _3, _4, _5, _6;


      #region Graph tests
      [Test]
      public override void Vertexes_Get() {
         var vertexes = TestGraph.Vertexes;
         Assert.IsNotNull(vertexes);

         var array = vertexes.ToArray();
         Assert.AreEqual(6, array.Length);
         Assert.AreSame(_1, array[0]);
         Assert.AreSame(_2, array[1]);
         Assert.AreSame(_3, array[2]);
         Assert.AreSame(_4, array[3]);
         Assert.AreSame(_5, array[4]);
         Assert.AreSame(_6, array[5]);
      }


      [Test]
      public override void Edges_Get() {
         TestEdges(TestGraph.Edges,
                   new[] { "1", "2" },
                   new[] { "2", "2" },
                   new[] { "2", "4" },
                   new[] { "2", "5" },
                   new[] { "4", "1" },
                   new[] { "4", "5" },
                   new[] { "5", "4" },
                   new[] { "6", "3" });
      }


      [Test]
      public override void FindVertex() {
         TestFindVertex("1", _1);
         TestFindVertex("2", _2);
         TestFindVertex("3", _3);
         TestFindVertex("4", _4);
         TestFindVertex("5", _5);
         TestFindVertex("6", _6);
      }


      [Test]
      public override void Neighbors_Edge() {
         TestEdgesIn_ByVertexValue (_1, "4");
         TestEdgesOut_ByVertexValue(_1, "2");

         TestEdgesIn_ByVertexValue (_2, "1", "2");
         TestEdgesOut_ByVertexValue(_2, "2", "4", "5");

         TestEdgesIn_ByVertexValue (_3, "6");
         TestEdgesOut_ByVertexValue(_3);

         TestEdgesIn_ByVertexValue (_4, "2", "5");
         TestEdgesOut_ByVertexValue(_4, "1", "5");

         TestEdgesIn_ByVertexValue (_5, "2", "4");
         TestEdgesOut_ByVertexValue(_5, "4");

         TestEdgesIn_ByVertexValue (_6);
         TestEdgesOut_ByVertexValue(_6, "3");
      }


      [Test]
      public override void Neighbors_Vertex() {
         TestVertexesIn (_1, _4);
         TestVertexesOut(_1, _2);

         TestVertexesIn (_2, _1, _2);
         TestVertexesOut(_2, _2, _4, _5);

         TestVertexesIn (_3, _6);
         TestVertexesOut(_3);

         TestVertexesIn (_4, _2, _5);
         TestVertexesOut(_4, _1, _5);

         TestVertexesIn (_5, _2, _4);
         TestVertexesOut(_5, _4);

         TestVertexesIn (_6);
         TestVertexesOut(_6, _3);
      }

      #endregion Graph tests


      #region Algorithm tests
      [Test]
      public override void DepthFirstSearch_Trace() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         Assert.AreEqual("(1 (2 (4 (5 5) 4) 2) 1) (3 3) (6 6)", search.Trace);
      }


      [Test]
      public override void DepthFirstSearch_EdgeTypes() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         TestEdges(search.EdgesOfType(DfsEdgeType.Tree),
                   new[] { "1", "2" },
                   new[] { "2", "4" },
                   new[] { "4", "5" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Back),
                   new[] { "2", "2" },
                   new[] { "4", "1" },
                   new[] { "5", "4" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Forward),
                   new[] { "2", "5" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Cross),
                   new[] { "6", "3" });
      }


      [Test]
      public override void DepthFirstSearch_Cycles() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         var cycles = search.Cycles.ToArray();
         Assert.AreEqual(3, cycles.Length);

         TestEdges(cycles[0].Edges,
                   new[] { "2", "2" });
         TestEdges(cycles[1].Edges,
                   new[] { "4", "1" },
                   new[] { "1", "2" },
                   new[] { "2", "4" });
         TestEdges(cycles[2].Edges,
                   new[] { "4", "5" },
                   new[] { "5", "4" });
      }
      #endregion Algorithm tests
   }
}
