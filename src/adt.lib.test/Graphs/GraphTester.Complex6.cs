using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using adt.lib.Graphs;
using adt.lib.Graphs.Algorithms;
using NUnit.Framework;



namespace adt.lib.test.Graphs {
   /// <summary>
   /// Runs tests on an 6-vertex 8-edge graph that contains all four types of edges: Tree, Back, Forward, and Cross.
   /// This example comes from the book Introduction to Algorithms, Second Edition, by T. H. Cormen, et al., Figure 22.4 on p. 542.
   /// </summary>
   public class GraphTester_Complex6 : GraphTypeTester {
      protected override Graph<string> GetGraph() {
         var graph = new Graph<string>();
         _u = graph.AddVertex("u");
         _v = graph.AddVertex("v");
         _w = graph.AddVertex("w");
         _x = graph.AddVertex("x");
         _y = graph.AddVertex("y");
         _z = graph.AddVertex("z");
         graph.AddEdge(_u, _v);
         graph.AddEdge(_u, _x);
         graph.AddEdge(_v, _y);
         graph.AddEdge(_w, _y);
         graph.AddEdge(_w, _z);
         graph.AddEdge(_x, _v);
         graph.AddEdge(_y, _x);
         graph.AddEdge(_z, _z);
         return graph;
      }


      private GraphVertex<string> _u, _v, _w, _x, _y, _z;


      #region Graph tests
      [Test]
      public override void Vertexes_Get() {
         var vertexes = TestGraph.Vertexes;
         Assert.IsNotNull(vertexes);

         var array = vertexes.ToArray();
         Assert.AreEqual(6, array.Length);
         Assert.AreSame(_u, array[0]);
         Assert.AreSame(_v, array[1]);
         Assert.AreSame(_w, array[2]);
         Assert.AreSame(_x, array[3]);
         Assert.AreSame(_y, array[4]);
         Assert.AreSame(_z, array[5]);
      }


      [Test]
      public override void Edges_Get() {
         TestEdges(TestGraph.Edges,
                   new[] { "u", "v" },
                   new[] { "u", "x" },
                   new[] { "v", "y" },
                   new[] { "w", "y" },
                   new[] { "w", "z" },
                   new[] { "x", "v" },
                   new[] { "y", "x" },
                   new[] { "z", "z" });
      }


      [Test]
      public override void FindVertex() {
         TestFindVertex("u", _u);
         TestFindVertex("v", _v);
         TestFindVertex("w", _w);
         TestFindVertex("x", _x);
         TestFindVertex("y", _y);
         TestFindVertex("z", _z);
      }


      [Test]
      public override void Neighbors_Edge() {
         TestEdgesIn_ByVertexValue (_u);
         TestEdgesOut_ByVertexValue(_u, "v", "x");

         TestEdgesIn_ByVertexValue (_v, "u", "x");
         TestEdgesOut_ByVertexValue(_v, "y");

         TestEdgesIn_ByVertexValue (_w);
         TestEdgesOut_ByVertexValue(_w, "y", "z");

         TestEdgesIn_ByVertexValue (_x, "u", "y");
         TestEdgesOut_ByVertexValue(_x, "v");

         TestEdgesIn_ByVertexValue (_y, "v", "w");
         TestEdgesOut_ByVertexValue(_y, "x");

         TestEdgesIn_ByVertexValue (_z, "w", "z");
         TestEdgesOut_ByVertexValue(_z, "z");
      }


      [Test]
      public override void Neighbors_Vertex() {
         TestVertexesIn (_u);
         TestVertexesOut(_u, _v, _x);

         TestVertexesIn (_v, _u, _x);
         TestVertexesOut(_v, _y);

         TestVertexesIn (_w);
         TestVertexesOut(_w, _y, _z);

         TestVertexesIn (_x, _u, _y);
         TestVertexesOut(_x, _v);

         TestVertexesIn (_y, _v, _w);
         TestVertexesOut(_y, _x);

         TestVertexesIn (_z, _w, _z);
         TestVertexesOut(_z, _z);
      }

      #endregion Graph tests


      #region Algorithm tests
      [Test]
      public override void DepthFirstSearch_Trace() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         Assert.AreEqual("(u (v (y (x x) y) v) u) (w (z z) w)", search.Trace);
      }


      [Test]
      public override void DepthFirstSearch_EdgeTypes() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         TestEdges(search.EdgesOfType(DfsEdgeType.Tree),
                   new[] { "u", "v" },
                   new[] { "v", "y" },
                   new[] { "y", "x" },
                   new[] { "w", "z" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Back),
                   new[] { "x", "v" },
                   new[] { "z", "z" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Forward),
                   new[] { "u", "x" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Cross),
                   new[] { "w", "y" });
      }


      [Test]
      public override void DepthFirstSearch_Cycles() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         var cycles = search.Cycles.ToArray();
         Assert.AreEqual(2, cycles.Length);

         TestEdges(cycles[0].Edges,
                   new[] { "v", "y" },
                   new[] { "y", "x" },
                   new[] { "x", "v" });
         TestEdges(cycles[1].Edges,
                   new[] { "z", "z" });
      }
      #endregion Algorithm tests
   }
}
